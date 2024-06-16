using Api_Dc.Application.Models.Request;
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
    public class SupervisionOpService : ISupervision
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public SupervisionOpService(
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
         
       
        public async Task<GeneralResponse<dynamic>> ListarOp(string numCotizacion)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarOpSupervisor", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumCotizacion", numCotizacion));
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
        public async Task<GeneralResponse<Object>> Aprobacion(SuperAprobacionRequest item)

        {
            try
            { 
                var newRow = new Tbl_SupervisorAprobacion()
                {
                    CotizacionGrupo = item.CotizacionGrupo,

                    TurnoInicial = item.TurnoInicial,
                    TurnoCambio = item.TurnoCambio,
                    FechProdInicial = DateTime.Parse(item.FechProdInicial.ToString()),
                    FechaProdCambio = DateTime.Parse(item.FechaProdCambio.ToString()),
                    FechaCreacion = DateTime.Now,
                    IdUsuario = item.IdUsuario,
                    IdUsuarioSolicita = item.IdUsuarioSolicita,
                    Estado=item.Estado,
                    NumeroCotizacion=item.NumeroCotizacion

                };
                this._context.Tbl_SupervisorAprobacion.Add(newRow);
                var grupo = this._context.Tbl_DetalleOpGrupo.Find(item.id);
                if (item.Estado == "Aprobado")
                {
                    grupo.IdEstado = 2;//PENDIENTE EN VENTA
                }
                else
                {
                    grupo.IdEstado = 7;//Rechazado por supervisor
                }
                grupo.IdUsuarioModifica = item.IdUsuario;
                grupo.FechaModifica = DateTime.Now;
                this._context.Tbl_DetalleOpGrupo.Update(grupo);

                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idModulo = 1,
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
        //ee
    }
}

