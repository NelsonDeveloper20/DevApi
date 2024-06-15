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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;

namespace ApiPortal_DataLake.Domain.Services
{
    public class DetalleOrdenProduccionService: IDetalleOrdenProduccionService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;

        
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
 
        public async Task<GeneralResponse<object>> AgregarProducto(dynamic formData, dynamic escuadra,string tipo)
        {
            try
            {
                // Extraer algunos campos específicos en variables  
                string numeroCotizacion = formData.NumeroCotizacion;
                string codigosisgeco = formData.CodigoSisgeco;
                string cotizacionGrupo = "";

                // Buscar grupo existente para la cotización, turno y fecha de producción especificados
               //var grupoExistente = this._context.Tbl_DetalleOpGrupo
               //     .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion && g.Turno == turno && g.FechaProduccion == fechaProduccion);
                var grupoExistente = this._context.Tbl_DetalleOpGrupo
                    .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion);
                int estadoInicial = 2;
                if (grupoExistente != null)//EXISTE
                {

                    if (tipo == "Componente")
                    {
                        var grupoExiste = this._context.Tbl_DetalleOpGrupo
                   .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion && g.CotizacionGrupo == numeroCotizacion + "-0");

                        if (grupoExiste != null)
                        {
                            cotizacionGrupo = grupoExiste.CotizacionGrupo;
                        }
                        else
                        {
                            cotizacionGrupo = $"{numeroCotizacion}-0";
                            var filagrupo = new Tbl_DetalleOpGrupo()
                            {
                                //IdTbl_OrdenProduccion
                                NumeroCotizacion = numeroCotizacion,
                                CotizacionGrupo = cotizacionGrupo,
                                CodigoSisgeco = codigosisgeco, 
                                Tipo="Componente",
                                FechaCreacion = DateTime.Now,
                                IdEstado = estadoInicial
                            };
                            this._context.Add(filagrupo);
                        }

                    }
                    else
                    {

                        string turno = formData.Turno;
                        DateTime fechaProduccion = DateTime.Parse(formData.FechaProduccion);
                        var grupoExistente2 = this._context.Tbl_DetalleOpGrupo
                    .FirstOrDefault(g => g.NumeroCotizacion == numeroCotizacion && g.Turno == turno && g.FechaProduccion == fechaProduccion);
                    if (grupoExistente2 != null)//EXISTE
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
                            int numeracion = int.Parse(ultimoGrupo.CotizacionGrupo.Split("-")[1]) + 1;
                            cotizacionGrupo = $"{numeroCotizacion}-{numeracion}";
                            var filagrupo = new Tbl_DetalleOpGrupo()
                            {
                                //IdTbl_OrdenProduccion
                                NumeroCotizacion = numeroCotizacion,
                                CotizacionGrupo = cotizacionGrupo,
                                CodigoSisgeco = codigosisgeco,
                                FechaProduccion = fechaProduccion,
                                Turno = turno,
                                Tipo="Producto",
                                FechaCreacion = DateTime.Now,
                                IdEstado = estadoInicial
                            };
                            this._context.Add(filagrupo);
                        }
                        else
                        {
                            // Si no hay grupos existentes para la cotización, asignar el primer número de grupo
                            cotizacionGrupo = $"{numeroCotizacion}-0";
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
                        var filagrupo = new Tbl_DetalleOpGrupo()
                        {
                            //IdTbl_OrdenProduccion
                            NumeroCotizacion = numeroCotizacion,
                            CotizacionGrupo = cotizacionGrupo,
                            CodigoSisgeco = codigosisgeco,
                            Tipo = "Componente",
                            FechaCreacion = DateTime.Now,
                            IdEstado = estadoInicial
                        };
                        this._context.Add(filagrupo);
                    }
                    else
                    {
                        string turno = formData.Turno;
                        DateTime fechaProduccion = DateTime.Parse(formData.FechaProduccion);
                        cotizacionGrupo = $"{numeroCotizacion}-1";
                        var filagrupo = new Tbl_DetalleOpGrupo()
                        {
                            //IdTbl_OrdenProduccion
                            NumeroCotizacion = numeroCotizacion,
                            CotizacionGrupo = cotizacionGrupo,
                            CodigoSisgeco = codigosisgeco,
                            FechaProduccion = fechaProduccion,
                            Turno = turno,
                            Tipo = "Producto",
                            FechaCreacion = DateTime.Now,
                            IdEstado = estadoInicial
                        };
                        this._context.Add(filagrupo);
                    }
                }

                TBL_DetalleOrdenProduccion dataFila = new TBL_DetalleOrdenProduccion();
                int Id = 0;

                // Lista de propiedades que no deseas modificar
                var propiedadesNoModificables = new List<string> { "FechaCreacion", "UsuarioCreador" };

                foreach (var kvp in formData)
                {
                    var key = kvp.Key;
                    var value = kvp.Value;
                    if (key == "Id")
                    { // Manejo especial para el campo "Id"
                        if (value != null && value.ToString() != "")
                            {
                                Id = (int)value;
                            }
                            else
                            {
                                Id = 0;
                            } 
                    }
                    else
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
                                    // Reemplazar las comas por puntos para que el método de conversión funcione correctamente
                                    //  value = value.Replace(",", ".");
                                    // Convertir el valor a decimal
                                    // var decimalValue = Convert.ToDouble(value);

                                    // Asignar el valor convertido a la propiedad correspondiente
                                    //propInfo.SetValue(dataFila, decimalValue, null);
                                    decimal decimalValue;
                                    //decimal? decimalValue = null; 
                                    // Convert.ToDecimal(value.Replace(",", "."), CultureInfo.InvariantCulture);
                                    //decimal.TryParse(value.Replace(",", "."), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimalValue);
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
                                    propInfo.SetValue(dataFila, convertedValue, null);
                                }
                            }
                        }
                    }
                }
                //agregar valor al grupo numeracion
                
        //var CotizacionGrupo_propInfo = typeof(TBL_DetalleOrdenProduccion).GetProperty("CotizacionGrupo", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        var CotizacionGrupo_value = Convert.ChangeType(cotizacionGrupo.ToString(), CotizacionGrupo_propInfo.PropertyType);
        //        CotizacionGrupo_propInfo.SetValue(dataFila, CotizacionGrupo_value, null);
         
                dataFila.CotizacionGrupo = cotizacionGrupo.ToString();
                dataFila.FechaCreacion = DateTime.Now;
                dataFila.IdEstado = estadoInicial;

                if (Id == 0)
                {
                    if (tipo == "Componente")
                    { 
                        dataFila.Tipo = "Componente";
                    }
                    else
                    { 
                        dataFila.Tipo = "Producto"; 
                    }
                    
                    if (escuadra != null)
                    {
                        foreach (var _item in escuadra)
                        {
                            var _cant = _item.Cantidad ?? 0; // Si _item.Cantidad es null, asigna 0, de lo contrario, asigna el valor de _item.Cantidad
                            var _escuadra = new Tbl_Escuadra()
                            {
                                CotizacionGrupo = cotizacionGrupo,
                                Codigo = _item.Codigo,
                                Descripcion = _item.Descripcion,
                                Cantidad = _cant
                            };
                            this._context.Tbl_Escuadra.Add(_escuadra);
                        }
                        dataFila.Escuadra = "SI";
                    }
                    else
                    {
                        dataFila.Escuadra = "NO";
                    }
                    var Productos = this._context.TBL_DetalleOrdenProduccion.AddAsync(dataFila);
                }
                else
                {
                    var existingDataFila = await _context.TBL_DetalleOrdenProduccion.FindAsync(Id);

                    if (existingDataFila != null)
                    {
                        foreach (var kvp in formData)
                        {
                            var key = kvp.Key;
                            if (propiedadesNoModificables.Contains(key))
                            {
                                continue; // Omite las propiedades que no deseas modificar
                            }

                            var propInfo = typeof(TBL_DetalleOrdenProduccion).GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                            if (propInfo != null)
                            {
                                var convertedValue = Convert.ChangeType(kvp.Value.ToString(), propInfo.PropertyType);
                                propInfo.SetValue(existingDataFila, convertedValue, null);
                            }
                        }
                        

                        if (escuadra != null)
                        {
                            var _listEscuadra = _context.Tbl_Escuadra.Where(ee => ee.CotizacionGrupo == cotizacionGrupo);
                            _context.Tbl_Escuadra.RemoveRange(_listEscuadra);

                            foreach (var _item in escuadra)
                            {
                                var _cant = _item.Cantidad ?? 0; // Si _item.Cantidad es null, asigna 0, de lo contrario, asigna el valor de _item.Cantidad
                                var _escuadra = new Tbl_Escuadra()
                                {
                                    CotizacionGrupo = cotizacionGrupo,
                                    Codigo = _item.Codigo,
                                    Descripcion = _item.Descripcion,
                                    Cantidad = _cant
                                };
                                _context.Tbl_Escuadra.Add(_escuadra);
                            }
                            existingDataFila.Escuadra = "SI";
                        }
                        else
                        {
                            existingDataFila.Escuadra = "NO";
                        }
                        _context.TBL_DetalleOrdenProduccion.Update(existingDataFila);
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
