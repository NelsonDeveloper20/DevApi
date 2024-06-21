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
        public async Task<GeneralResponse<object>> ValidarEstacion(string paso, string dato)
        {
            try
            {
                var jsonresponse = new { Existe = "", Data = (object)null };
                string existe = "NO";
                switch (paso)
                {
                    case "USUARIO":
                        var _usuario = await this._context.Tbl_Usuario.Where(u => u.CodigoUsuario.Trim().ToUpper() == dato.Trim().ToUpper()).ToListAsync();
                        if (_usuario.Count > 0)
                        {
                            existe = "SI";
                        }
                        jsonresponse = new { Existe = existe, Data = (object)_usuario };
                        break;
                    case "MAQUINA":
                        var _estacion = await this._context.Tbl_Estacion.Where(e => e.CodigoEstacion.Trim().ToUpper() == dato.ToUpper()).ToListAsync();
                        if (_estacion.Count > 0)
                        {
                            existe = "SI";
                        } 
                        jsonresponse = new { Existe = existe, Data = (object)_estacion };
                        break;
                    case "COTIZACION":
                        var _grupo = await this._context.Tbl_DetalleOpGrupo.Where(u => u.CotizacionGrupo.Trim().ToUpper() == dato.Trim().ToUpper()).ToListAsync();
                        if (_grupo.Count > 0)
                        {
                            existe = "SI";
                        } 
                        jsonresponse = new { Existe = existe, Data = (object)_grupo };
                        break;
                    default:
                        break;
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
                                  PE.CodigoUsuario,
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
         CodigoUsuario =_request.CodigoUsuario,
         /*FechaInicio =_request.FechaInicio,
         FechaFin =_request.FechaFin,*/
         FechaCreacion =DateTime.Now,
         IdUsuarioCreacion =_request.IdUsuarioCreacion,
         Estado = estado//Pendiente Construccion
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
