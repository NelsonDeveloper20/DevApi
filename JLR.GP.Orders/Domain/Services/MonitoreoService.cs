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
        public async Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id) //OK OK
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

        public async Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request)
        {
            try
            {
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

    }
}
