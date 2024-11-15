﻿using Api_Dc.Domain.Contracts;
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
using System.Drawing;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class MonitoreoService : IMonitoreo
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public MonitoreoService(
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
          
        public async Task<GeneralResponse<dynamic>> ListarExplocion(string grupoCotizacion, string fechaInicio, string fechaFin) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin ", fechaFin));
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

        public async Task<GeneralResponse<dynamic>> ListarReporteExplocion(string grupoCotizacion, string fechaInicio, string fechaFin) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_listarReporteExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin ", fechaFin));
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


        public async Task<GeneralResponse<dynamic>> ListarMantenimientoExplocion(string grupoCotizacion) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_ListarMantenimientoExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion)); 
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
        public async Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id) //MOSTRAR EN POPUP DETALLE
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_listarComponentesProductoPorGrupo", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Grupo", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@Id", id)); 
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

        public async Task<IEnumerable<Tbl_Componentes>> ListarComponentesPorCodigosProducto(string codigosProducto, string grupo) //MOSTRAR COMPONENTES EN COMBOS DEL DETALLE DEL GRUPO
        {

            try
            {
                if (grupo.Contains("-0"))
                {

                    var query = await this._context.Tbl_Componentes.ToListAsync();
                    return query;
                }
                else
                {
                    var query = await this._context.Tbl_Componentes
                              .Where(o => codigosProducto.Contains(o.CodigoProducto))
                              .ToListAsync();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request)
        {
            try
            {
                var newEstacion = new Tbl_ProduccionEstacion()
                { 
                     

         IdEstacion =_request.IdEstacion,
         NumeroCotizacion =_request.NumeroCotizacion,
         CotizacionGrupo =_request.CotizacionGrupo,
         //CodigoUsuario =_request.CodigoUsuario,
         /*FechaInicio =_request.FechaInicio,
         FechaFin =_request.FechaFin,*/
         FechaCreacion =DateTime.Now,
         IdUsuarioCreacion =_request.IdUsuarioCreacion,
         Estado =1
    };
                this._context.Tbl_ProduccionEstacion.Add(newEstacion);
                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = newEstacion.Id,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0

                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> GuardarExplocion(List<ExplocionComponentesRequest> request)
        {
            try
            {
                foreach(var item in request)
                {
                    var nuevaFila = new Tbl_Explocion()
                    {
                        NumeroCotizacion = item.NumeroCotizacion,
                        CotizacionGrupo = item.Grupo,
                        Nombre_Producto = item.NombreProducto,
                        Codigo_Producto = item.CodigoProducto,
                        Descrip_Componente = item.Componente,
                        Cod_Componente = item.Codigo,
                        Descripcion = item.Nombre,
                        Color = item.Color,
                        Unidad = item.UnidadMedida,
                        Cantidad = item.CantidadUtilizada,
                        Merma = item.Merma,
                        Origen = "Explocion",
                        //Adicional = item.Agregado,
                        IdUsuarioCrea =Convert.ToInt32(item.Usuario),
                        FechaCreacion = DateTime.Now
                    };
                    this._context.Tbl_Explocion.Add(nuevaFila);
                }
                // Actualizar el estado de grupo dentro de la transacción
                var grupo = await this._context.Tbl_DetalleOpGrupo
                                            .Where(g => g.CotizacionGrupo == request[0].Grupo)
                                            .FirstOrDefaultAsync();
                if (grupo != null)
                {
                    grupo.IdEstado = 6; // Estado TERMINADO
                    this._context.Tbl_DetalleOpGrupo.Update(grupo);
                }
                else
                {
                    throw new Exception("No se encontró el grupo especificado.");
                }

                await this._context.SaveChangesAsync();


                var jsonresponse = new
            {
                Respuesta = "OK",
                idOrden = 1,

            };

            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }catch (Exception ex)
            {
                this._logger.LogError($"Insertar   explocion mantenimiento Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0

                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> GuardarExplocionMantenimiento(List<ExplocionComponentesMantRequest> request)
        {
            if (request == null || !request.Any())
            {
                var errorResponse = new
                {
                    Respuesta = "La solicitud no puede estar vacía",
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.BadRequest, errorResponse);
            }

            try
            {
                var firstRequest = request.FirstOrDefault(r => r.id != "0");
                if (firstRequest == null)
                {
                    var errorResponse = new
                    {
                        Respuesta = "No se encontró un ID válido en la solicitud",
                        idOrden = 0
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.BadRequest, errorResponse);
                }

                var fechaExplocion = _context.Tbl_Explocion
                    .Where(e => e.Id == Convert.ToInt32(firstRequest.id))
                    .Select(e => e.FechaCreacion)
                    .FirstOrDefault();

                foreach (var item in request)
                {
                    if (item.id == "0")
                    {
                        var nuevaFila = new Tbl_Explocion
                        {
                            NumeroCotizacion = item.numeroCotizacion,
                            CotizacionGrupo = item.cotizacionGrupo,
                            Nombre_Producto = item.nombre_Producto,
                            Codigo_Producto = item.codigo_Producto,
                            Descrip_Componente = item.componente,
                            Cod_Componente = item.cod_Componente,
                            Descripcion = item.descripcion,
                            Color = item.color,
                            Unidad = item.unidad,
                            Cantidad = item.cantidad,
                            Merma = item.merma,
                            Origen = "Explocion",
                            IdUsuarioCrea = Convert.ToInt32(item.idUsuarioCrea),
                            FechaCreacion = fechaExplocion
                        };
                        _context.Tbl_Explocion.Add(nuevaFila);
                    }
                    else
                    {
                        var _explocion = await _context.Tbl_Explocion.FindAsync(Convert.ToInt32(item.id));
                        if (_explocion != null)
                        {
                            _explocion.Descrip_Componente = item.componente;
                            _explocion.Cod_Componente = item.cod_Componente;
                            _explocion.Descripcion = item.descripcion;
                            _explocion.Color = item.color;
                            _explocion.Unidad = item.unidad;
                            _explocion.Cantidad = item.cantidad;
                            _explocion.Merma = item.merma;
                            _explocion.IdUsuarioModifica = Convert.ToInt32(item.idUsuarioCrea);
                            _explocion.FechaModifica = DateTime.Now;
                            _context.Tbl_Explocion.Update(_explocion);
                        }
                    }
                }

                await _context.SaveChangesAsync();

                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idOrden = 1
                };
                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        public async Task<GeneralResponse<Object>> CargaExcelExplocion(List<ExplocionComCargaRequest> request)
        {
            try
            {
                // Validación de datos
                List<object> listErrores = new List<object>();

                foreach (var item in request)
                {

                    // Validar si el grupo ya fue explotado
                    var grupoExplotado = await this._context.Tbl_Explocion
                        .AnyAsync(e => e.CotizacionGrupo == item.Grupo);

                    if (grupoExplotado)
                    {
                        var error = new
                        {
                            Cotizacion = item.Cotizacion,
                            Grupo = item.Grupo,
                            Producto = item.Codigo_Producto,
                            Mensaje = "El grupo ya fue explotado"
                        };
                        listErrores.Add(error);
                    }
                    // Validar si el producto existe en la cotización y grupo
                    var existeProducto = await this._context.TBL_DetalleOrdenProduccion
                        .AnyAsync(dp => dp.NumeroCotizacion == item.Cotizacion
                                    && dp.CotizacionGrupo == item.Grupo
                                    && dp.CodigoProducto == item.Codigo_Producto);


                    if (!existeProducto)
                    {
                        var error = new
                        {
                            Cotizacion = item.Cotizacion,
                            Grupo = item.Grupo,
                            Producto = item.Codigo_Producto,
                            Mensaje = "El producto no existe en la cotización y grupo cargado"
                        };
                        listErrores.Add(error);
                    }
                }

                // Si hay errores, retornar respuesta con lista de errores
                if (listErrores.Count > 0)
                {
                    var jsonResponse = new
                    {
                        Respuesta = "ERROR",
                        idOrden = 0,
                        listaError = listErrores
                    };

                    return new GeneralResponse<Object>(HttpStatusCode.OK, jsonResponse);
                }

                // Procesar los datos si no hay errores
                foreach (var item in request)
                {
                    var nuevaFila = new Tbl_Explocion
                    {
                        NumeroCotizacion = item.Cotizacion,
                        CotizacionGrupo = item.Grupo,
                        Nombre_Producto = item.Nombre_Producto,
                        Codigo_Producto = item.Codigo_Producto,
                        Descrip_Componente = item.Componente,
                        Cod_Componente = item.Cod_Componente,
                        Descripcion = item.Descripcion,
                        Color = item.Color,
                        Unidad = item.Unidad,
                        Cantidad = item.Cantidad,
                        Merma = item.Merma,
                        Origen = "Carga",
                        IdUsuarioCrea = Convert.ToInt32(item.Usuario),
                        FechaCreacion = DateTime.Now,
                    };

                    _context.Tbl_Explocion.Add(nuevaFila);
                    var grupo = await this._context.Tbl_DetalleOpGrupo
                                          .Where(g => g.CotizacionGrupo == item.Grupo)
                                          .FirstOrDefaultAsync();
                    if (grupo != null)
                    {
                        grupo.IdEstado = 6; // Estado TERMINADO
                        this._context.Tbl_DetalleOpGrupo.Update(grupo);
                    }
                    else
                    {
                        throw new Exception("No se encontró el grupo especificado.");
                    }

                }

                await _context.SaveChangesAsync();

                // Retornar respuesta de éxito
                var successResponse = new
                {
                    Respuesta = "OK",
                    idOrden = 0
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, successResponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error al insertar orden de producción: {JsonConvert.SerializeObject(ex)}");

                var errorResponse = new
                {
                    Respuesta = "Error al procesar la carga de datos.",
                    Detalle = ex.Message // Aquí puedes agregar más detalles según necesites
                };

                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, errorResponse);
            }
        }


    }
}
