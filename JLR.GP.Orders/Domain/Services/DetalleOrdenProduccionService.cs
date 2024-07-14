using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

namespace ApiPortal_DataLake.Domain.Services
{
    public class DetalleOrdenProduccionService: IDetalleOrdenProduccionService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private string CnDc_Blinds;


        private readonly ILogger<DetalleOrdenProduccionService> _logger;
        
        public DetalleOrdenProduccionService(
            IConfiguration configuration,
            DBContext context,
            ILogger<DetalleOrdenProduccionService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;


            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configurations = builder.Build();
            CnDc_Blinds = configurations["ConnectionString:DefaultConnection"];
        }

        public async Task<IEnumerable<TBL_DetalleOrdenProduccion>> GetAllAsync()
        {

            try
            {

                var query = await this._context.TBL_DetalleOrdenProduccion.ToListAsync();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #region VALIACION DE PRODUCTOS POR TURNO Y FECH PRODUCCION
        public async Task<GeneralResponse<object>> ValidarRegistroProducto(string turno, string fechaProduccion, string codigoProducto, string accionamiento)
        {
            try
            {
                var jsonresponse = new
                {
                    Msj = "",
                    Resultado = "OK"
                };

                DateTime _fechaProduccion = DateTime.Parse(fechaProduccion);
                string _CodProducto = codigoProducto;

                var listCargaProd = await this._context.Tbl_CargaProduccion
                    .Where(cp => cp.CodigoProducto == _CodProducto)
                    .ToListAsync();

                if (!listCargaProd.Any())
                {
                    jsonresponse = new
                    {
                        Msj = "NO EXISTE VALIDACIÓN PARA EL PRODUCTO: " + _CodProducto,
                        Resultado = "OK"
                    };
                    return new GeneralResponse<object>(HttpStatusCode.OK, jsonresponse);
                }
                int equivalencia = 0;
                // Aplicar filtro de equivalencia solo para ciertos productos y accionamientos
                if (_CodProducto == "PRTRS" || _CodProducto == "PRTRZ")
                {
                    equivalencia = listCargaProd
                        .Where(ls => ls.Accionamiento == accionamiento && ls.CodigoProducto == _CodProducto)
                        .Select(ls => ls.Equivalencia)
                        .FirstOrDefault() ?? 0;
                }
                else
                {
                    equivalencia =  listCargaProd
                        .Where(ls => ls.CodigoProducto == _CodProducto)
                        .Select(ls => ls.Equivalencia)
                        .FirstOrDefault() ?? 0;
                }

                int productos = await CalculateProductos(turno, _fechaProduccion, _CodProducto, accionamiento, equivalencia);
                int productosMasUltimo = productos + equivalencia;
                if (productos >= 30)
                {
                    jsonresponse = new
                    {
                        Msj = $"Ya está superando la cantidad de producción en el turno {turno} para la fecha de producción {fechaProduccion}",
                        Resultado = "NO"
                    };
                }else  if (productosMasUltimo >= 30){
                        jsonresponse = new
                        {
                            Msj = $"Con este producto ya está superando la cantidad de producción en el turno {turno} para la fecha de producción {fechaProduccion}",
                            Resultado = "NO"
                        };
                }else{
                    jsonresponse = new
                    {
                        Msj = "La producción está dentro de los límites permitidos.",
                        Resultado = "OK"
                    };
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Insertar Orden producción Error: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        private async Task<int> CalculateProductos(string turno, DateTime fechaProduccion, string codigoProducto, string accionamiento, int equivalencia)
        {
            int productos = 0; 

            // Asignar valores adicionales según el producto y el accionamiento
          
            DataTable result = new DataTable();
            using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
            {
                await cnm.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("SP_ValidacionCantidadXTurnoFecProd", cnm))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Turno", turno));
                    cmd.Parameters.Add(new SqlParameter("@Fecha", fechaProduccion));
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(result);
                    }
                }
            }

            if (result.Rows.Count == 0)
            {
                // No rows returned by the stored procedure
                return 0;
            }

            foreach (DataRow row in result.Rows)
            {
                int _equivalencia = Convert.ToInt32(row["Equivalencia"]);
                int cantidad = Convert.ToInt32(row["Cantidad"]);
                productos += _equivalencia * cantidad;
            }


            return productos;
        }

        #endregion
        public async Task<GeneralResponse<object>> AgregarProducto(dynamic formData, dynamic escuadra, string tipo)
        {
            try
            {
                // Extraer algunos campos específicos en variables  
                string numeroCotizacion = formData.NumeroCotizacion;
                string codigosisgeco = formData.CodigoSisgeco;
                //string SuperoMaximo = formData.Maximo;

                string cotizacionGrupo = ""; 
                int indexDetalle = 0;
                int estadoInicial = 2; 
                // Método para crear un nuevo grupo
                void CrearNuevoGrupo(string cotizacionGrupo, string tipo, DateTime? fechaProduccion = null, string turno = null)
                {
                    var filagrupo = new Tbl_DetalleOpGrupo()
                    {
                        NumeroCotizacion = numeroCotizacion,
                        CotizacionGrupo = cotizacionGrupo,
                        CodigoSisgeco = codigosisgeco,
                        Tipo = tipo,
                        FechaProduccion = fechaProduccion,
                        Turno = turno,
                        FechaCreacion = DateTime.Now,
                        IdEstado = estadoInicial,
                        IdUsuarioCrea = Convert.ToInt32(formData.IdUsuarioCrea)
                    };
                    this._context.Add(filagrupo);
                }
                if (string.IsNullOrEmpty(formData.Id))
                {
                    string SuperoMaximo = formData.Maximo;
                    // Buscar grupo existente para la cotización 
                    var grupoExistente = this._context.Tbl_DetalleOpGrupo
                        .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion);
                    if (grupoExistente != null) // EXISTE
                    {
                        if (tipo == "Componente")
                        {
                            var grupoExiste = this._context.Tbl_DetalleOpGrupo
                                .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion && g.CotizacionGrupo == numeroCotizacion + "-0");

                            if (grupoExiste != null) // EXISTE
                            {
                                cotizacionGrupo = grupoExiste.CotizacionGrupo;

                            }
                            else
                            {
                                cotizacionGrupo = $"{numeroCotizacion}-0";
                                estadoInicial = 5;//ESTADO COMO CONTRUCCION TERMINADA PARA LOS COMPONENTES
                                CrearNuevoGrupo(cotizacionGrupo, "Componente");
                            }
                        }
                        else
                        {
                            string turno = formData.Turno;
                            DateTime fechaProduccion = DateTime.Parse(formData.FechaProduccion);
                            var grupoExistente2 = this._context.Tbl_DetalleOpGrupo
                                .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion && g.Turno == turno && g.FechaProduccion == fechaProduccion);
                           
                            if (grupoExistente2 != null) // EXISTE
                            {
                                cotizacionGrupo = grupoExistente2.CotizacionGrupo;
                            }
                            else
                            {
                                var ultimoGrupo = this._context.Tbl_DetalleOpGrupo
                                    .Where(g => g.NumeroCotizacion == numeroCotizacion)
                                    .OrderByDescending(g => g.CotizacionGrupo)
                                    .FirstOrDefault(); 
                                if (ultimoGrupo != null)
                                {
                                    // Obtener el último número de grupo y agregar 1
                                    int numeracion = int.Parse(ultimoGrupo.CotizacionGrupo.Split('-')[1]) + 1;
                                    cotizacionGrupo = $"{numeroCotizacion}-{numeracion}";
                                    if (SuperoMaximo == "SI")
                                    {
                                        estadoInicial = 1;//PENDIENTE APROBACION SUPERVISOR
                                    }
                                    CrearNuevoGrupo(cotizacionGrupo, "Producto", fechaProduccion, turno);
                                }
                                else
                                {
                                    // Si no hay grupos existentes para la cotización, asignar el primer número de grupo
                                    cotizacionGrupo = $"{numeroCotizacion}-0";
                                    if (SuperoMaximo == "SI")
                                    {
                                        estadoInicial = 1;//PENDIENTE APROBACION SUPERVISOR
                                    }
                                    CrearNuevoGrupo(cotizacionGrupo, "Producto", fechaProduccion, turno);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Si no hay grupos existentes para la cotización, asignar el primer número de grupo
                        if (tipo == "Componente")
                        {
                            cotizacionGrupo = $"{numeroCotizacion}-0";
                            estadoInicial = 5;//ESTADO COMO CONTRUCCION TERMINADA PARA LOS COMPONENTES
                            CrearNuevoGrupo(cotizacionGrupo, "Componente");
                        }
                        else
                        {
                            string turno = formData.Turno;
                            DateTime fechaProduccion = DateTime.Parse(formData.FechaProduccion);

                            cotizacionGrupo = $"{numeroCotizacion}-1";

                            if (SuperoMaximo == "SI")
                            {
                                estadoInicial = 1;//PENDIENTE APROBACION SUPERVISOR
                            }
                            CrearNuevoGrupo(cotizacionGrupo, "Producto", fechaProduccion, turno);
                        }
                    }
                }
                TBL_DetalleOrdenProduccion dataFila = new TBL_DetalleOrdenProduccion();
                int Id = 0;
                // Lista de propiedades que no deseas modificar
                var propiedadesNoModificables = new List<string> { "FechaCreacion", "UsuarioCreador", "Cantidad", "Alto", "Ancho", "TuboMedida", "TelaMedida", "AlturaMedida" };


                Id = formData.Id != null && formData.Id.ToString() != "" ? Convert.ToInt32(formData.Id) : 0;
                if (Id == 0)
                {   // Convertir los datos del formData a propiedades del objeto dataFila
                    foreach (var kvp in formData)
                    {
                        var key = kvp.Key;
                        var value = kvp.Value;
                        if (key == "Id")
                        { // Manejo especial para el campo "Id"                            
                            Id = value != null && value.ToString() != "" ? Convert.ToInt32(value) : 0;
                        }else if (key == "Maximo")
                        {

                        }else
                        {
                            if (value != "")
                            {

                                // Obtener el tipo de datos de la propiedad
                                var propInfo = typeof(TBL_DetalleOrdenProduccion).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                                if (propInfo != null)
                                {
                                    // Verificar si el tipo de datos es decimal
                                    if (propInfo.PropertyType == typeof(decimal) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(decimal))
                                    {
                                        propInfo.SetValue(dataFila, Convert.ToDecimal(value.Replace(".", ",")), null);

                                    }// Verificar si el tipo de datos es DateTime
                                    else if (propInfo.PropertyType == typeof(DateTime) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(DateTime))
                                    {
                                        // Convertir el valor a DateTime
                                        var dateTimeValue = DateTime.Parse(value.ToString());
                                        // Asignar el valor convertido a la propiedad correspondiente
                                        propInfo.SetValue(dataFila, dateTimeValue, null);

                                    }// Verificar si el tipo de datos es int
                                    else if (propInfo.PropertyType == typeof(int) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(int))
                                    {
                                        // Convertir el valor a int
                                        var intvalue = int.Parse(value.ToString());
                                        // Asignar el valor convertido a la propiedad correspondiente
                                        propInfo.SetValue(dataFila, intvalue, null);
                                    }
                                    else
                                    {
                                        // Convertir el valor al tipo de datos de la propiedad y asignarlo
                                        var convertedValue = Convert.ChangeType(value.ToString(), propInfo.PropertyType);
                                        propInfo.SetValue(dataFila, convertedValue.Replace("--Seleccione--", ""), null);
                                    }
                                }
                            }
                        }
                    }
                    //agregar valor al grupo numeracion

                    dataFila.CotizacionGrupo = cotizacionGrupo.ToString();
                    dataFila.FechaCreacion = DateTime.Now;
                    dataFila.IdEstado = estadoInicial;
                    if (tipo == "Componente")
                    {
                        dataFila.Tipo = "Componente";
                        dataFila.Escuadra = "NO";
                    }
                    else
                    {

                        dataFila.Tipo = "Producto";
                        dataFila.Escuadra = "NO";
                        IEnumerable<object> escuadraEnumerable0 = escuadra as IEnumerable<object>;
                        if (escuadraEnumerable0 != null && escuadraEnumerable0.Any())
                        {
                            dataFila.Escuadra = "SI";
                        }


                        var ultimoDetalle = this._context.TBL_DetalleOrdenProduccion
                    .Where(dp => dp.CotizacionGrupo == cotizacionGrupo)
                    .OrderByDescending(dp => dp.IndexDetalle)
                    .FirstOrDefault();
                        indexDetalle = (int)(ultimoDetalle != null ? ultimoDetalle.IndexDetalle + 1 : 1);

                        dataFila.IndexDetalle = indexDetalle;

                        string indiceGroup = formData.IndiceAgrupacion;
                        var _ambiente = this._context.Tbl_Ambiente
                   .Where(a => a.Indice == Convert.ToInt32(indiceGroup) && a.NumeroCotizacion == numeroCotizacion)
                   .FirstOrDefault();
                        if (_ambiente != null)
                        {
                            DateTime fechaProduccion = DateTime.Parse(formData.FechaProduccion);
                            string turno = formData.Turno;
                            _ambiente.Stock -= 1;
                            _ambiente.FechaProduccion = fechaProduccion;
                            _ambiente.Turno = turno;
                            this._context.Tbl_Ambiente.Update(_ambiente);
                        }
                    }
                    dataFila.IdUsuarioCrea = Convert.ToInt32(formData.IdUsuarioCrea);
                     


                    this._context.TBL_DetalleOrdenProduccion.AddAsync(dataFila); ;


                }
                else
                {
                    var existingDataFila = await _context.TBL_DetalleOrdenProduccion.FindAsync(Id);

                    if (existingDataFila != null)
                    {
                        foreach (var kvp in formData)
                        {
                            var key = kvp.Key;
                            var value = kvp.Value;
                            if (propiedadesNoModificables.Contains(key))
                            {
                                continue; // Omite las propiedades que no deseas modificar
                            }

                            if (value != "")
                            {

                                // Obtener el tipo de datos de la propiedad
                                var propInfo = typeof(TBL_DetalleOrdenProduccion).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                                if (propInfo != null)
                                {
                                    // Verificar si el tipo de datos es decimal
                                    if (propInfo.PropertyType == typeof(decimal) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(decimal))
                                    {
                                        propInfo.SetValue(existingDataFila, Convert.ToDecimal(value.Replace(".", ",")), null);

                                    }// Verificar si el tipo de datos es DateTime
                                    else if (propInfo.PropertyType == typeof(DateTime) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(DateTime))
                                    {
                                        // Convertir el valor a DateTime
                                        var dateTimeValue = DateTime.Parse(value.ToString());
                                        // Asignar el valor convertido a la propiedad correspondiente
                                        propInfo.SetValue(existingDataFila, dateTimeValue, null);

                                    }// Verificar si el tipo de datos es int
                                    else if (propInfo.PropertyType == typeof(int) || Nullable.GetUnderlyingType(propInfo.PropertyType) == typeof(int))
                                    {
                                        // Convertir el valor a int
                                        var intvalue = int.Parse(value.ToString());
                                        // Asignar el valor convertido a la propiedad correspondiente
                                        propInfo.SetValue(existingDataFila, intvalue, null);
                                    }
                                    else
                                    {
                                        // Convertir el valor al tipo de datos de la propiedad y asignarlo
                                        var convertedValue = Convert.ChangeType(value.ToString(), propInfo.PropertyType);
                                        propInfo.SetValue(existingDataFila, convertedValue.Replace("--Seleccione--", ""), null);
                                    }
                                }
                            }
                            else
                            { 
                                switch (key)
                                {
                                    case "CodigoMotor":
                                        existingDataFila.CodigoMotor = "";
                                        existingDataFila.Motor = "";
                                        existingDataFila.MarcaMotor = "";
                                        break;
                                    case "Motor": 
                                        existingDataFila.Motor = "";  
                                        break;

                                }
                            }
                        }
                        // Validar si escuadra tiene datos
                        existingDataFila.Escuadra = "NO";
                        if (escuadra != null)
                        {
                            // Intentar convertir escuadra a una lista de objetos
                            var escuadraList = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(escuadra));
                            if (escuadraList != null && escuadraList.Count > 0)
                            {
                                existingDataFila.Escuadra = "SI";
                            }
                        }

                        existingDataFila.IdUsuarioModifica = Convert.ToInt32(formData.IdUsuarioCrea);
                        _context.TBL_DetalleOrdenProduccion.Update(existingDataFila);
                    }
                }

                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                if (escuadra != null)
                {
                    // Intentar convertir escuadra a una lista de objetos
                    var escuadraList = JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(escuadra));
                    if (escuadraList != null && escuadraList.Count > 0 && Id==0)
                    {
                        foreach (var _item in escuadra)
                        {
                            int _cant;
                            bool conversionExitosa = int.TryParse(_item.Cantidad.ToString(), out _cant);
                            if (!conversionExitosa)
                            {
                                _cant = 0; // Si la conversión falla, asigna 0.
                            }
                            var _escuadra = new Tbl_Escuadra()
                            {
                                CotizacionGrupo = cotizacionGrupo,
                                Codigo = _item.Codigo,
                                IdProducto = dataFila.Id,
                                Descripcion = _item.Descripcion,
                                IdUsuarioCrea = Convert.ToInt32(formData.IdUsuarioCrea),
                                Cantidad = _cant
                            };
                            this._context.Tbl_Escuadra.Add(_escuadra);
                        }
                    }
                    // Intentar convertir escuadra a una lista de objetos
                    if (Id != 0)
                    {
                        var _listEscuadra = _context.Tbl_Escuadra.Where(ee => ee.IdProducto == Id);
                        _context.Tbl_Escuadra.RemoveRange(_listEscuadra);
                        if (escuadraList != null && escuadraList.Count > 0 && Id != 0)
                        {
                            foreach (var _item in escuadra)
                            {
                                int _cant;
                                bool conversionExitosa = int.TryParse(_item.Cantidad.ToString(), out _cant);
                                if (!conversionExitosa)
                                {
                                    _cant = 0; // Si la conversión falla, asigna 0.
                                }
                                var _escuadra = new Tbl_Escuadra()
                                {
                                    CotizacionGrupo = cotizacionGrupo,
                                    Codigo = _item.Codigo,
                                    IdProducto = Id,
                                    IdUsuarioCrea = Convert.ToInt32(formData.IdUsuarioCrea),
                                    Descripcion = _item.Descripcion,
                                    Cantidad = _cant
                                };
                                _context.Tbl_Escuadra.Add(_escuadra);
                            }
                        }
                    }
                       
                }
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = Id,
                };

                return new GeneralResponse<object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        } 

    }
}
