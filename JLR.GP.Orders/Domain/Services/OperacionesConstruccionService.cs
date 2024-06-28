using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace ApiPortal_DataLake.Domain.Services
{
    public class OperacionesConstruccionService : IOperacionesContruccion
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public OperacionesConstruccionService(
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
         
       
        public async Task<GeneralResponse<dynamic>> ListarOperacionesConstruccion(string fecha)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarOperacionesConstruccion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Fecha", fecha));
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
        public async Task<GeneralResponse<object>> ValidarEstacion(string paso, string codigoEstacion, string idusuario, string grupo)
        {
            try
            {
                var jsonresponse = new { Existe = "", Estacion = (object)null, Grupo = (object)null, EstacionProduccion = (object)null, Mensaje = "" };
                string existe = "NO";
                int estado = 0;
                if (codigoEstacion == "100001")
                {
                    estado = 4;//Pendiente Construccion
                }
                else if(codigoEstacion=="100002")
                {
                    estado = 5;//Termino Produccion
                }
                if (paso == "COTIZACION")
                {
                    var grupoExistente = await this._context.Tbl_DetalleOpGrupo
                        .Where(u => u.CotizacionGrupo.Trim().ToUpper() == grupo.Trim().ToUpper())
                        .ToListAsync();
                    
                    if (grupoExistente.Count > 0)
                    {
                        if (grupoExistente[0].IdEstado ==3 || grupoExistente[0].IdEstado == 4)
                        {

                            existe = "SI";
                            var estacionProduccion = await this._context.Tbl_ProduccionEstacion
                                .Where(o => o.CotizacionGrupo == grupo)
                                .ToListAsync();
                            // Validar si ha pasado por todas las estaciones 
                            if (estacionProduccion.Count > 0)
                            {
                                jsonresponse = new
                                {
                                    Existe = existe,
                                    Estacion = (object)null,
                                    Grupo = (object)grupoExistente,
                                    EstacionProduccion = (object)estacionProduccion,
                                    Mensaje = "El grupo ya ha pasado por las estaciones."
                                };
                            }
                            else
                            {
                                jsonresponse = new
                                {
                                    Existe = existe,
                                    Estacion = (object)null,
                                    Grupo = (object)grupoExistente,
                                    EstacionProduccion = (object)estacionProduccion,
                                    Mensaje = "El grupo aún no ha pasado por todas las estaciones."
                                };
                            }
                        }
                        else
                        {

                            var estacionProduccion = await this._context.Tbl_ProduccionEstacion
                                .Where(o => o.CotizacionGrupo == grupo)
                                .ToListAsync();
                            if (estacionProduccion.Count >= 2)
                            {
                                jsonresponse = new { Existe = "SI", Estacion = (object)null, Grupo = (object)grupoExistente, EstacionProduccion = (object)estacionProduccion, Mensaje = "Construcción finalizada" };

                            }
                            else
                            {
                                jsonresponse = new { Existe = "NO", Estacion = (object)null, Grupo = (object)grupoExistente, EstacionProduccion = (object)null, Mensaje = "El grupo debe estar en estado Pendiente Operaciones." };
                            
                            }

                        }
                    }
                    else
                    {
                        jsonresponse = new { Existe = "NO", Estacion = (object)null, Grupo = (object)grupoExistente, EstacionProduccion = (object)null, Mensaje = "El grupo no existe." };
                    }
                }
                else if (paso == "MAQUINA")
                {
                    var estacionExistente = await this._context.Tbl_Estacion
                        .Where(e => e.CodigoEstacion.Trim().ToUpper() == codigoEstacion.ToUpper())
                        .ToListAsync();

                    if (estacionExistente.Count > 0)
                    {
                        existe = "SI";
                        if (codigoEstacion == "100001")
                        {
                            var estacionProduccion = await this._context.Tbl_ProduccionEstacion
                                .Where(o => o.CotizacionGrupo == grupo)
                                .ToListAsync();
                            var grupoSearch = await this._context.Tbl_DetalleOpGrupo
                                .Where(u => u.CotizacionGrupo.Trim().ToUpper() == grupo.Trim().ToUpper())
                                .ToListAsync();
                            var estaciones = estacionProduccion.Count;

                            if (estaciones == 0)
                            {
                                var newEstacion = new Tbl_ProduccionEstacion()
                                {
                                    IdEstacion = estacionExistente[0].Id,
                                    NumeroCotizacion = grupoSearch[0].NumeroCotizacion,
                                    CotizacionGrupo = grupoSearch[0].CotizacionGrupo,
                                    IdUsuario = Convert.ToInt32(idusuario),
                                    FechaCreacion = DateTime.Now,
                                    IdUsuarioCreacion = Convert.ToInt32(idusuario),
                                    Estado = 1
                                };
                                this._context.Tbl_ProduccionEstacion.Add(newEstacion);
                                var grupoFind = await this._context.Tbl_DetalleOpGrupo.FirstOrDefaultAsync(g => g.CotizacionGrupo == grupo);
                                if (grupoFind != null)
                                {
                                    grupoFind.IdEstado = estado;
                                    this._context.Tbl_DetalleOpGrupo.Update(grupoFind);
                                }
                                await this._context.SaveChangesAsync();
                                jsonresponse = new
                                {
                                    Existe = existe,
                                    Estacion = (object)estacionExistente,
                                    Grupo = (object)grupoSearch,
                                    EstacionProduccion = (object)newEstacion,
                                    Mensaje = "El grupo ha sido registrado en la estación 1."
                                };
                            }
                            else
                            {
                                jsonresponse = new
                                {
                                    Existe = existe,
                                    Estacion = (object)estacionExistente,
                                    Grupo = (object)grupoSearch,
                                    EstacionProduccion = (object)estacionProduccion,
                                    Mensaje = "El grupo ya está registrado en la estación 1."
                                };
                            }
                        }
                        else if (codigoEstacion == "100002")
                        {
                            var estacionProduccion1 = await this._context.Tbl_ProduccionEstacion
                                .Where(o => o.IdEstacion == 1 && o.CotizacionGrupo == grupo)
                                .ToListAsync();

                            if (estacionProduccion1.Count == 0)
                            {
                                jsonresponse = new { Existe = "DEBE EMPEZAR POR ESTACION 1", Estacion = (object)null, Grupo = (object)null, EstacionProduccion = (object)null, Mensaje = "Debe empezar por la estación 1." };
                            }
                            else
                            {
                                var estacionProduccion2 = await this._context.Tbl_ProduccionEstacion
                                    .Where(o => o.IdEstacion == 2 && o.CotizacionGrupo == grupo)
                                    .ToListAsync();
                                var grupoSearch2 = await this._context.Tbl_DetalleOpGrupo
                                    .Where(u => u.CotizacionGrupo.Trim().ToUpper() == grupo.Trim().ToUpper())
                                    .ToListAsync();

                                if (estacionProduccion2.Count == 0)
                                {
                                    var newEstacion = new Tbl_ProduccionEstacion()
                                    {
                                        IdEstacion = 2,
                                        NumeroCotizacion = grupoSearch2[0].NumeroCotizacion,
                                        CotizacionGrupo = grupoSearch2[0].CotizacionGrupo,
                                        IdUsuario = Convert.ToInt32(idusuario),
                                        FechaCreacion = DateTime.Now,
                                        IdUsuarioCreacion = Convert.ToInt32(idusuario),
                                        Estado = 1
                                    };
                                    this._context.Tbl_ProduccionEstacion.Add(newEstacion);
                                    var grupoFind = await this._context.Tbl_DetalleOpGrupo.FirstOrDefaultAsync(g => g.CotizacionGrupo == grupo);
                                    if (grupoFind != null)
                                    {
                                        grupoFind.IdEstado = estado;
                                        this._context.Tbl_DetalleOpGrupo.Update(grupoFind);
                                    }
                                    await this._context.SaveChangesAsync();
                                    jsonresponse = new
                                    {
                                        Existe = "ESTACION 2 INSERTADO",
                                        Estacion = (object)null,
                                        Grupo = (object)null,
                                        EstacionProduccion = (object)newEstacion,
                                        Mensaje = "El grupo ha sido registrado en la estación 2."
                                    };
                                }
                                else
                                {
                                    jsonresponse = new
                                    {
                                        Existe = "ESTACION 2 YA EXISTE",
                                        Estacion = (object)null,
                                        Grupo = (object)null,
                                        EstacionProduccion = (object)estacionProduccion2,
                                        Mensaje = "El grupo ya está registrado en la estación 2."
                                    };
                                }
                            }
                        }
                    }
                    else
                    {
                        jsonresponse = new { Existe = "NO", Estacion = (object)null, Grupo = (object)null, EstacionProduccion = (object)null, Mensaje = "La estación no existe." };
                    }
                }

                return new GeneralResponse<object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<dynamic>> ListarProductoXEstacionGrupo(string grupoCotizacion, string estacion)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_ListarProdParaEstacionXGrupo", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@GrupoCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@ESTACION", estacion));
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

        public async Task<GeneralResponse<dynamic>> ListarAvanceEstacion(string grupoCotizacion)
        {
            try
            {
                var result = (from PE in this._context.Tbl_ProduccionEstacion
                              join E in this._context.Tbl_Estacion on PE.IdEstacion equals E.Id into estacionGroup
                              from E in estacionGroup.DefaultIfEmpty()
                              join U in this._context.Tbl_Usuario on PE.IdUsuarioCreacion equals U.Id into usuarioGroup
                              from U in usuarioGroup.DefaultIfEmpty()
                              where PE.CotizacionGrupo == grupoCotizacion
                              select new
                              {
                                  PE.Id,
                                  Estacion = E.Nombre,
                                  E.CodigoEstacion,
                                  PE.IdUsuario,
                                  PE.TipoProceso,
                                  PE.FechaInicio,
                                  PE.FechaFin,
                                  PE.FechaCreacion,
                                  Usuario = U.Nombre + " " + U.Apellido
                              }).ToList();

                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request)
        {
            try
            {
                int estado = 0;
                if (_request.IdEstacion == 1)
                {
                    estado = 4;//Pendiente Construccion
                }
                else
                {
                    estado = 5;//Termino Produccion
                }
                var newEstacion = new Tbl_ProduccionEstacion()
                {       
                 IdEstacion =_request.IdEstacion,
                 NumeroCotizacion =_request.NumeroCotizacion,
                 CotizacionGrupo =_request.CotizacionGrupo,
                 IdUsuario = _request.IdUsuarioCreacion,// _request.IdUsuario, 
                 FechaCreacion =DateTime.Now,
                 IdUsuarioCreacion =_request.IdUsuarioCreacion,
                 Estado = estado 
                };
                this._context.Tbl_ProduccionEstacion.Add(newEstacion);
                var grupo = await this._context.Tbl_DetalleOpGrupo
            .FirstOrDefaultAsync(g => g.CotizacionGrupo == _request.CotizacionGrupo);

                if (grupo != null)
                {
                    grupo.IdEstado = estado;
                    this._context.Tbl_DetalleOpGrupo.Update(grupo);
                }

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

    }
}
