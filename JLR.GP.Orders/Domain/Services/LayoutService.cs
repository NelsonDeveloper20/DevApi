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
    public class LayoutService: ILayoutGrupo
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public LayoutService(
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
         
        //::::::::::::::::::::::::::::::::
        /*public async Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo()
        {
            var _user = this._context.Tbl_DetalleOpGrupo.ToList();
            return _user;
        }*/
        public async Task<GeneralResponse<dynamic>> ListarDetalleOpGrupo(string NumeroCotizacionGrupo)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetLayoutGrupov2", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacionGrupo", NumeroCotizacionGrupo));
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
        public async Task<GeneralResponse<dynamic>> ListarComponentesLayout(string NumeroCotizacionGrupo)
        {
            try
            {
                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetLayoutGrupoComponentes", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacionGrupo", NumeroCotizacionGrupo));
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

    }
}
