using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class DetalleOpGrupoService: IDetalleOpGrupo
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public DetalleOpGrupoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger
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
        public async Task<GeneralResponse<object>> ModificarTurnoFechaGrupo(string grupo, string turno, string fecha)
        {
            using var transaction = await this._context.Database.BeginTransactionAsync();
            try
            {
                // Validar y convertir la fecha
                if (!DateTime.TryParse(fecha, out var fechaProduccion))
                {
                    return new GeneralResponse<object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Formato de fecha inválido", idModulo = 0 }
                    );
                }

                // Modificar el grupo en Tbl_DetalleOpGrupo
                var grupoExistente = await this._context.Tbl_DetalleOpGrupo
                    .FirstOrDefaultAsync(g => g.CotizacionGrupo == grupo);

                if (grupoExistente == null)
                {
                    return new GeneralResponse<object>(
                        HttpStatusCode.NotFound,
                        new { Respuesta = "El grupo no existe", idModulo = 0 }
                    );
                }

                grupoExistente.Turno = turno;
                grupoExistente.FechaProduccion = fechaProduccion;

                this._context.Tbl_DetalleOpGrupo.Update(grupoExistente);

                // Modificar todos los productos relacionados en TBL_DetalleOrdenProduccion
                var productosRelacionados = this._context.TBL_DetalleOrdenProduccion
                    .Where(p => p.CotizacionGrupo == grupo)
                    .ToList();

                if (productosRelacionados.Any())
                {
                    foreach (var producto in productosRelacionados)
                    {
                        producto.Turno = turno;
                        producto.FechaProduccion = fechaProduccion;
                    }

                    this._context.TBL_DetalleOrdenProduccion.UpdateRange(productosRelacionados);
                }

                // Guardar todos los cambios
                await this._context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                return new GeneralResponse<object>(
                    HttpStatusCode.OK,
                    new { Respuesta = "Operación realizada correctamente", idModulo = 1 }
                );
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                await transaction.RollbackAsync();

                // Loggear el error
                this._logger.LogError($"Error en ModificarTurnoFechaGrupo: {ex}");

                return new GeneralResponse<object>(
                    HttpStatusCode.InternalServerError,
                    new { Respuesta = ex.Message, idModulo = 0 }
                );
            }
        }


        //::::::::::::::::::::::::::::::::
        /*public async Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo()
        {
            var _user = this._context.Tbl_DetalleOpGrupo.ToList();
            return _user;
        }*/
        public async Task<GeneralResponse<dynamic>> ListarDetalleOpGrupo(OPGrupoRequest Request)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetOrdenProduccionDetalleGrupoVenta", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Vendedor", Request.Vendedor));
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion ", Request.NumeroCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@Cliente", Request.Cliente));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", Request.FechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin", Request.FechaFin));
                        cmd.Parameters.Add(new SqlParameter("@CodigoSisgeco", Request.CodigoSisgeco));
                        cmd.Parameters.Add(new SqlParameter("@RucCliente", Request.RucCliente));
                        cmd.Parameters.Add(new SqlParameter("@IdProyecto", Request.IdProyecto));
                        cmd.Parameters.Add(new SqlParameter("@TipoCliente", Request.TipoCliente));
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<Object>> EnviarEstado(string destino, List<GruposIDRequest> request)

        {
            try
            {
                var idModulo = 1;
                foreach (var item in request)
                {
                    var _Grupo = this._context.Tbl_DetalleOpGrupo.Find(item.Id);
                    switch(destino)
                    {
                        case "Venta":
                            _Grupo.IdEstado = 2;//Pendiente Venta
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Operaciones":
                            _Grupo.IdEstado = 3; //Pendiente Operaciones
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Construccion":
                            _Grupo.IdEstado = 4;//Pendiente Construccion
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Construccion Terminada":
                            _Grupo.IdEstado = 5;//Construccion Terminada
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Terminado":
                            _Grupo.IdEstado = 6;//Terminado
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                    }                    
                }
                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idModulo = idModulo,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idModulo = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListarFiltros()
        {
            try
            {
                var vendedores = await _context.Tbl_OrdenProduccion
            .Select(d => d.NombreVendedor)
            .Distinct()
            .ToListAsync();


                var cotizaciones = await _context.Tbl_DetalleOpGrupo
                    .Select(d => d.NumeroCotizacion)
                    .Distinct()
                    .ToListAsync();

                var proyectos = await _context.Tbl_Proyecto.ToListAsync();

                var tiposClientes = await _context.Tbl_TipoCliente.ToListAsync();

                var rucs = await _context.Tbl_OrdenProduccion
                    .Select(o => o.RucCliente)
                    .Distinct()
                    .ToListAsync();
                var grupos = await _context.Tbl_DetalleOpGrupo
          .Select(d => d.CotizacionGrupo)
          .Distinct()
          .ToListAsync();
                var result = new
                {
                    vendedores,
                    cotizaciones,
                    proyectos,
                    tiposClientes,
                    rucs,
                    grupos

                };

                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListatarProductosDetallePorGrupo(string grupo)
        {
            try
            {
                //var _productos=this._context.TBL_DetalleOrdenProduccion.Where(p=> p.CotizacionGrupo==grupo).ToList();
                var _productos = await (from dop in _context.TBL_DetalleOrdenProduccion
                                        where dop.CotizacionGrupo == grupo
                                        orderby dop.Linea
                                        select new 
                                        {
                                            Id = dop.Id,
                                            NumeroCotizacion = dop.NumeroCotizacion,
                                            CodigoSisgeco = dop.CodigoSisgeco,
                                            CodigoProducto = dop.CodigoProducto,
                                            Linea = dop.Linea,
                                            NombreProducto = dop.NombreProducto,
                                            UnidadMedida = dop.UnidadMedida,
                                            Cantidad = dop.Cantidad,
                                            Alto = dop.Alto,
                                            Ancho = dop.Ancho,
                                            IndiceAgrupacion = dop.IndiceAgrupacion,
                                            IndexDetalle = dop.IndexDetalle,
                                            Pase = "",
                                            Existe = "",
                                            Familia = dop.Familia,
                                            SubFamilia = dop.SubFamilia,
                                            Precio = dop.Precio,
                                            PrecioInc = dop.PrecioInc,
                                            Igv = dop.Igv,
                                            Lote = dop.Lote,
                                            FechaProduccion = dop.FechaProduccion != null ? dop.FechaProduccion.Value.ToString("dd/MM/yyyy H:mm:ss") : null,
                                            
                                            FechaEntrega = dop.FechaEntrega != null ? dop.FechaEntrega.Value.ToString("dd/MM/yyyy H:mm:ss") : null,


                                            Nota = dop.Nota,
                                            Color = dop.Color,
                                            IdTbl_Ambiente = dop.IdTbl_Ambiente,
                                            Ambiente = dop.Ambiente,
                                            Turno = dop.Turno,
                                            SoporteCentral = dop.SoporteCentral,
                                            TipoSoporteCentral = dop.TipoSoporteCentral,
                                            Caida = dop.Caida,
                                            Accionamiento = dop.Accionamiento,
                                            CodigoTubo = dop.CodigoTubo,
                                            NombreTubo = dop.NombreTubo,
                                            Mando = dop.Mando,
                                            TipoMecanismo = dop.TipoMecanismo,
                                            ModeloMecanismo = dop.ModeloMecanismo,
                                            TipoCadena = dop.TipoCadena,
                                            CodigoCadena = dop.CodigoCadena,
                                            Cadena = dop.Cadena,
                                            TipoRiel = dop.TipoRiel,
                                            TipoInstalacion = dop.TipoInstalacion,
                                            CodigoRiel = dop.CodigoRiel,
                                            Riel = dop.Riel,
                                            TipoCassete = dop.TipoCassete,
                                            Lamina = dop.Lamina,
                                            Apertura = dop.Apertura,
                                            ViaRecogida = dop.ViaRecogida,
                                            TipoSuperior = dop.TipoSuperior,
                                            CodigoBaston = dop.CodigoBaston,
                                            Baston = dop.Baston,
                                            NumeroVias = dop.NumeroVias,
                                            TipoCadenaInferior = dop.TipoCadenaInferior,
                                            MandoCordon = dop.MandoCordon,
                                            MandoBaston = dop.MandoBaston,
                                            CodigoBastonVarrilla = dop.CodigoBastonVarrilla,
                                            BastonVarrilla = dop.BastonVarrilla,
                                            Cabezal = dop.Cabezal,
                                            CodigoCordon = dop.CodigoCordon,
                                            Cordon = dop.Cordon,
                                            CodigoCordonTipo2 = dop.CodigoCordonTipo2,
                                            CordonTipo2 = dop.CordonTipo2,
                                            Cruzeta = dop.Cruzeta,
                                            Dispositivo = dop.Dispositivo,
                                            CodigoControl = dop.CodigoControl,
                                            Control = dop.Control,
                                            CodigoSwitch = dop.CodigoSwitch,
                                            Switch = dop.Switch,
                                            LlevaBaston = dop.LlevaBaston,
                                            MandoAdaptador = dop.MandoAdaptador,
                                            CodigoMotor = dop.CodigoMotor,
                                            Motor = dop.Motor,
                                            CodigoTela = dop.CodigoTela,
                                            Tela = dop.Tela,
                                            Cenefa = dop.Cenefa,
                                            NumeroMotores = dop.NumeroMotores,
                                            Serie = dop.Serie,
                                            AlturaCadena = dop.AlturaCadena,
                                            AlturaCordon = dop.AlturaCordon,
                                            MarcaMotor = dop.MarcaMotor,
                                            IdUsuarioCrea = dop.IdUsuarioCrea,
                                            IdUsuarioModifica = dop.IdUsuarioModifica,
                                            FechaCreacion = dop.FechaCreacion,
                                            FechaModifica = dop.FechaModifica,
                                            IdEstado = dop.IdEstado,
                                            CotizacionGrupo = dop.CotizacionGrupo,
                                            Tipo = dop.Tipo,
                                            EstadoOp = "",
                                            Escuadra = dop.Escuadra,
                                            WhsCode=dop.WhsCode
                                        }).ToListAsync();

                return new GeneralResponse<object>(HttpStatusCode.OK, _productos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /*
         Lógica para Aplicar Central a Productos
En este sistema, cada cuadro representa un producto y los cuadros están organizados en filas, donde la cantidad de productos en cada fila va aumentando progresivamente. La lógica para aplicar "Central" a un producto sigue estas reglas:

Producto Único: Si hay solo un producto en el grupo, no se puede aplicar "Central". La respuesta será "Central": "NO".

Múltiples Productos:

Los productos se organizan en una lista ordenada.
El primer producto en la lista (índice 0) no puede ser central.
El segundo producto (índice 1) puede ser central solo para un producto.
El tercer producto (índice 2) puede ser central para dos productos.
Y así sucesivamente, hasta que el último producto de la lista puede ser central para todos los productos anteriores.
Actualización del Estado Central:

Primero, se obtiene la lista de productos del grupo especificado, ordenada por su identificador.
Se cuenta la cantidad total de productos.
Si hay más de un producto, se localiza el producto específico según su identificador (id).
Se verifica si la posición (índice) de este producto en la lista le permite ser central.
Si la posición es válida, se actualiza el estado del producto a "Central".
Se guarda el cambio en la base de datos y se responde con "Central": "SI".
Si la posición no es válida o el producto no se encuentra, se responde con un error o "Central": "NO".
        */

        public async Task<GeneralResponse<Object>> AplicarCentral(string cotizacionGrupo, int id, string valor)
        {
            try
            {
                var jsonresponse = new
                {
                    Respuesta = "",
                    Central = "",
                    idModulo = 0,
                    ProductoId = 0
                };

                // Validación del valor
                if (valor != "SI" && valor != "NO")
                {
                    jsonresponse = new
                    {
                        Respuesta = "Valor inválido",
                        Central = "NO",
                        idModulo = 0,
                        ProductoId = 0
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.BadRequest, jsonresponse);
                }

                if (valor == "NO")
                {
                    var producto = await this._context.TBL_DetalleOrdenProduccion.FindAsync(id);
                    if (producto != null)
                    {
                        producto.Central = "NO";
                        this._context.TBL_DetalleOrdenProduccion.Update(producto);
                        await this._context.SaveChangesAsync();
                        jsonresponse = new
                        {
                            Respuesta = "Se ha quitado el central",
                            Central = "NO",
                            idModulo = 1,
                            ProductoId = id
                        };
                    }
                    else
                    {
                        jsonresponse = new
                        {
                            Respuesta = "Producto no encontrado",
                            Central = "NO",
                            idModulo = 0,
                            ProductoId = id
                        };
                    }
                }
                else
                {
                    var productos = await this._context.TBL_DetalleOrdenProduccion
                        .Where(dop => dop.CotizacionGrupo == cotizacionGrupo)
                        .OrderBy(dop => dop.IndexDetalle)
                        .ToListAsync();

                    int cantidadProductos = productos.Count;

                    if (cantidadProductos <= 1)
                    {
                        jsonresponse = new
                        {
                            Respuesta = "El grupo debe tener mínimo 2 productos para aplicar central",
                            Central = "NO",
                            idModulo = 1,
                            ProductoId = 0
                        };
                    }
                    else
                    {
                        var producto = productos.FirstOrDefault(p => p.Id == id);
                        if (producto != null)
                        {
                            int maxCentral = cantidadProductos - 1;
                            int centralesExistentes = productos.Count(p => p.Central == "SI");

                            if (centralesExistentes >= maxCentral && valor == "SI")
                            {
                                jsonresponse = new
                                {
                                    Respuesta = "Número máximo de productos centrales alcanzado",
                                    Central = "NO",
                                    idModulo = 0,
                                    ProductoId = id
                                };
                            }
                            else
                            {
                                producto.Central = valor.ToUpper();
                                this._context.TBL_DetalleOrdenProduccion.Update(producto);
                                await this._context.SaveChangesAsync();

                                jsonresponse = new
                                {
                                    Respuesta = "OK",
                                    Central = "SI",
                                    idModulo = 1,
                                    ProductoId = id
                                };
                            }
                        }
                        else
                        {
                            jsonresponse = new
                            {
                                Respuesta = "Producto no encontrado",
                                Central = "NO",
                                idModulo = 0,
                                ProductoId = id
                            };
                        }
                    }
                }

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idModulo = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }


        public async Task<GeneralResponse<Object>> ReinicioGrupo2(int idGrupo)
        {
            try
            {
                var jsonresponse = new
                {
                    Respuesta = "OK REINICIADO",
                    ProductoId = 0
                };
                var _grupo = this._context.Tbl_DetalleOpGrupo.Find(idGrupo);
                _grupo.IdEstado = 3; //Pendiente Operaciones

                var _productEstaciones=this._context.Tbl_ProduccionEstacion.Where(p=>p.CotizacionGrupo==_grupo.CotizacionGrupo).ToList();

                foreach(var iten in _productEstaciones)
                {
                    this._context.Tbl_ProduccionEstacion.Remove(iten);
                }
                this._context.Tbl_DetalleOpGrupo.Update(_grupo);
                await this._context.SaveChangesAsync();

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idModulo = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> ReinicioGrupo(int idGrupo)
        {
            using (var transaction = await this._context.Database.BeginTransactionAsync())
            {
                try
                {
                    var jsonresponse = new
                    {
                        Respuesta = "OK REINICIADO",
                        ProductoId = 0
                    };
                    var _grupo = await this._context.Tbl_DetalleOpGrupo.FindAsync(idGrupo);
                    if (_grupo == null)
                    {
                        return new GeneralResponse<Object>(HttpStatusCode.NotFound, new { Respuesta = "Grupo no encontrado", ProductoId = 0 });
                    }
                    _grupo.IdEstado = 3; // Pendiente Operaciones
                    var _productEstaciones = this._context.Tbl_ProduccionEstacion
                        .Where(p => p.CotizacionGrupo == _grupo.CotizacionGrupo)
                        .ToList();
                    this._context.Tbl_ProduccionEstacion.RemoveRange(_productEstaciones);
                    this._context.Tbl_DetalleOpGrupo.Update(_grupo);
                    await this._context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    this._logger.LogError($"ReinicioGrupo Error: {ex.Message}", ex);
                    var jsonresponse = new
                    {
                        Respuesta = ex.Message,
                        idModulo = 0
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
                }
            }
        }



    }
}
