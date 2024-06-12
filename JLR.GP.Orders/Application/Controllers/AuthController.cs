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

        public AuthController(
            IConfiguration configuration,
            IUsuarioService usuarioService,
            IAuthService authService,
            ILogger<AuthController> logger,
            IMapper mapper
        )
        {
            this._configuration = configuration;
            this._usuarioService = usuarioService;
            this._authService = authService;
            this._logger = logger;
            this._mapper = mapper;
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
