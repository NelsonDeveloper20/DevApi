using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Storage.Blobs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class ListasSisgecoService: IListasSisgecoService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<ListasSisgecoService> _logger;

        public ListasSisgecoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<ListasSisgecoService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }  
        //INIT BANCOS 
        public async Task<IEnumerable<ListasSisgecoResponse>> GetListar(string tipo,string subfamilia)
        {
            List<ListasSisgecoResponse> list = new List<ListasSisgecoResponse>();
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            var CnSisgeco = configuration["ConnectionString:DefaultConnectionSisgeco"];
            var query = "";
            switch (tipo)
            {
                case "Tela":                    
                    if (subfamilia == "TELCS")
                    {
                        //query = @"select  top 5  * from  TBL_DetalleOrdenProduccion where Id= @id";
                        query = "SELECT * FROM Articulo WHERE codfamilia IN ('TEL')  AND des NOT LIKE '%(Liquidado)%' ";
                    }
                    else
                    {
                        query = "SELECT * FROM Articulo WHERE codfamilia IN ('TEL')  AND des NOT LIKE '%(Liquidado)%' "; 
                    }
                    break;
                case "Familia":
                    if (subfamilia == "0")
                    {
                        query = "SELECT * FROM Articulo";
                    }
                    else if (subfamilia == "PALRH")
                    {//RIEL HOTELERO
                        query = "SELECT * FROM Articulo WHERE codigo like  '"+subfamilia+"%'";
                    }
                    else if (subfamilia == "PALRF")
                    {//RIEL FLEXIBLE
                        query = "SELECT * FROM Articulo WHERE codigo like  '"+subfamilia+"%'";
                    }
                    else if (subfamilia == "PJ")
                    {
                        query = "SELECT * FROM Articulo WHERE  codigo like  'ACCPJ%' OR codigo LIKE '%ACCRH%'";
                    }
                    else if (subfamilia == "PJJJ")
                    { //PANEL JAPONES
                        query = "SELECT * FROM Articulo WHERE   codigo IN('PALRM00000002','PALRM00000003')";
                    }
                    else if (subfamilia == "ACCRH")
                    {
                        query = "SELECT * FROM Articulo WHERE codigo like  '"+subfamilia+"%'";
                    }
                    else if (subfamilia == "PJ")
                    {
                        query = "SELECT * FROM Articulo WHERE codsubfamilia= '"+subfamilia+"'";
                    }
                    else if (subfamilia == "TO")
                    {
                        query = "SELECT * FROM Articulo WHERE des like  '%manivela%'";
                    }
                    else if (subfamilia == "PALRF")
                    {
                        query = "SELECT * FROM Articulo WHERE codigo like  '"+subfamilia+"%'";
                    }
                    else
                    {
                        query = "SELECT * FROM Articulo WHERE codsubfamilia= '"+subfamilia+"'";
                    }
                    break;
                case "Varrilla":
                    if (subfamilia == "VARRILLA")
                    {//barrilla baston 
                        query = "SELECT * FROM Articulo WHERE des LIKE 'BASTON%' AND  codsubfamilia= 'PH'";
                    }
                    else if (subfamilia == "CORDON")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE des LIKE 'CORDON%' AND  codsubfamilia= 'PH'";
                    }
                    else if (subfamilia == "ACCPJ")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE   codigo like '%"+subfamilia+"%'";
                    }
                    else if (subfamilia == "ACCRH")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE   codigo like '%"+subfamilia+"%'";
                    }
                    else
                    {
                        query = "SELECT * FROM Articulo WHERE des LIKE 'CORDON%' AND  codsubfamilia= '"+subfamilia+"'";
                    }
                    break;
                case "Cordon":
                    if (subfamilia == "VARRILLA")
                    {//barrilla baston 
                        query = "SELECT * FROM Articulo WHERE des LIKE 'BASTON%' AND  codsubfamilia= 'PH'";
                    }
                    else if (subfamilia == "CORDON")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE des LIKE 'CORDON%' AND  codsubfamilia= 'PH'";
                    }
                    else if (subfamilia == "ACCPJ")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE   codigo like '%" + subfamilia + "%'";
                    }
                    else if (subfamilia == "ACCRH")
                    {//cordon : cordon
                        query = "SELECT * FROM Articulo WHERE   codigo like '%" + subfamilia + "%'";
                    }
                    else
                    {
                        query = "SELECT * FROM Articulo WHERE des LIKE 'CORDON%' AND  codsubfamilia= '" + subfamilia + "'";
                    }
                    break;
                case "Articulo":
                    query = "SELECT * FROM Articulo WHERE   codigo like '%"+subfamilia+"%'";
                    break;
                case "Motor": 
                    if (subfamilia == "PRTPJ")
                    { //panel japones
                        query = "SELECT *FROM Articulo ar WHERE  codigo IN('PRTPJ0003','ADDPJ0001')";
                    }
                    else
                    {
                        query = "SELECT *FROM Articulo ar WHERE  ar.des NOT LIKE '%(Liquidado)%' AND ar.des  LIKE '%MOTOR%' AND codfamilia = 'MOT'; ";
                    }
                    break;
                case "Nombretubo":
                    query = "SELECT * FROM Articulo WHERE   codigo like '%" + subfamilia + "%'";
                    break;
                default:

                    break;

            }

            DataTable dt = new DataTable();
            using (var connection = new SqlConnection(CnSisgeco))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(query, connection);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                }
            }
            foreach (DataRow row in dt.Rows)
            {
                if (dt.Columns.Contains("codigo") && dt.Columns.Contains("des"))
                {
                    var fila = new ListasSisgecoResponse()
                    {
                        Codigo = row["codigo"] != DBNull.Value ? row["codigo"].ToString() : null,
                        Nombre = row["des"] != DBNull.Value ? row["des"].ToString() : null
                    };
                    list.Add(fila);
                }
            }

            return  list;
        }

        //INIT FORMA PAGO


        public async Task<GeneralResponse<dynamic>> ListarComponentesSsigeco()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                var CnSisgeco = configuration["ConnectionString:DefaultConnectionSisgeco"];

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnSisgeco))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_listarComponentesSisgeco ", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
