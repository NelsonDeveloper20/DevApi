using AutoMapper;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Request;
using ApiPortal_DataLake.Domain.Response;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http.Headers;
using ApiPortal_DataLake.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly DBContext _context;
        private string CnDc_Blinds; 

        public AuthController(
            IConfiguration configuration,
            IUsuarioService usuarioService,
            IAuthService authService,
            ILogger<AuthController> logger,
            DBContext context,
            IMapper mapper
        )
        {
            this._configuration = configuration;
            this._usuarioService = usuarioService;
            this._authService = authService;
            this._logger = logger;
            this._mapper = mapper;
            this._context = context;
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configurations = builder.Build(); 
            CnDc_Blinds = configurations["ConnectionString:DefaultConnection"];
        }
        public class LineaModel
        {
            public int DocEntry { get; set; }
            public int LineNum { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; } 
            public int ItemsGroupCode { get; set; }
            public string ItemsGroupName { get; set; }

            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal LineTotal { get; set; }
            public string CodFamilia { get; set; }
            public string Familia { get; set; }
            public string CodSubfamilia { get; set; }
            public string Subfamilia { get; set; }
        }
        [HttpGet("insertar-lineas")]
        public async Task<IActionResult> InsertarLineasDesdeApi()
        {
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwibmJmIjoxNzQ2ODUzODYxLCJleHAiOjE3NDY4NTc0NjEsImlhdCI6MTc0Njg1Mzg2MX0.ztsqTEoa0dhtjkGb6HkbyiX97pzth5BjQwn5obvw_dw"; // tu token real

            var ordenes = await _context.Tbl_OrdenProduccion.ToListAsync();

            using (var connection = new SqlConnection(CnDc_Blinds))
            {
                await connection.OpenAsync();

                foreach (var orden in ordenes)
                {
                    int docEntry = Convert.ToInt32(orden.NumeroCotizacion.ToString());
                    string url = $"http://191.98.160.56:8081/api/Orders/{docEntry}";

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        try
                        {
                            var response = await client.GetAsync(url);
                            if (!response.IsSuccessStatusCode) continue;

                            var jsonResponse = await response.Content.ReadAsStringAsync();

                            using var doc = JsonDocument.Parse(jsonResponse);
                            var lineasJson = doc.RootElement.GetProperty("Lineas").GetRawText();
                            var lineas = JsonSerializer.Deserialize<List<LineaModel>>(lineasJson);

                            foreach (var linea in lineas)
                            {
                                var cmd = new SqlCommand(@"
                            INSERT INTO TB_DATASAP
                            (DocEntry, LineNum, ItemCode, ItemName, ItemsGroupCode, ItemsGroupName, Quantity, Price, LineTotal, CodFamilia, Familia, CodSubfamilia, Subfamilia)
                            VALUES
                            (@DocEntry, @LineNum, @ItemCode, @ItemName, @ItemsGroupCode, @ItemsGroupName, @Quantity, @Price, @LineTotal, @CodFamilia, @Familia, @CodSubfamilia, @Subfamilia)
                        ", connection);

                                cmd.Parameters.AddWithValue("@DocEntry", linea.DocEntry);
                                cmd.Parameters.AddWithValue("@LineNum", linea.LineNum);
                                cmd.Parameters.AddWithValue("@ItemCode", linea.ItemCode ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@ItemName", linea.ItemName ?? (object)DBNull.Value);
                                //cmd.Parameters.AddWithValue("@ItemsGroupCode", linea.ItemsGroupCode ?? (object)DBNull.Value); 
                                cmd.Parameters.AddWithValue("@ItemsGroupCode", linea.ItemsGroupCode);  // No es necesario hacer null check

                                cmd.Parameters.AddWithValue("@ItemsGroupName", linea.ItemsGroupName ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Quantity", linea.Quantity);
                                cmd.Parameters.AddWithValue("@Price", linea.Price);
                                cmd.Parameters.AddWithValue("@LineTotal", linea.LineTotal);
                                cmd.Parameters.AddWithValue("@CodFamilia", linea.CodFamilia ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Familia", linea.Familia ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@CodSubfamilia", linea.CodSubfamilia ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@Subfamilia", linea.Subfamilia ?? (object)DBNull.Value);

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error procesando DocEntry {orden.NumeroCotizacion}");
                        }
                    }
                }
            }

            return Ok("Todos los datos de SAP fueron insertados correctamente.");
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok("AuthController.Get");
        }

        [HttpPost]
        [Route("token")]
        public async Task<ActionResult<GeneralResponse<JwtResponse>>> GetToken()
        {
            this._logger.LogInformation($"Controller GetToken started");
            try
            {
                StringValues azureToken;
                bool hasAzureToken = HttpContext.Request.Headers.TryGetValue(HeaderRequestConstant.AzureTokenAD, out azureToken);
                if (hasAzureToken)
                {
                    var response = await this._authService.GetToken(azureToken);

                    return response;
                }

                this._logger.LogError($"GetToken : No se envó el token en la cabecera");
                return Conflict();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error GetToken : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<GeneralResponse<JwtResponse>>> GetRefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var response = await this._authService.GetRefreshToken(refreshTokenRequest);

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error GetToken : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
    }
}
