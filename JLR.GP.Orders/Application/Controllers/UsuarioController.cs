using AutoMapper;
using ApiPortal_DataLake.Application.Filters;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using Api_Dc.Domain.Models;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsuarioController(
            IUsuarioService usuarioService,
            ILogger<UsersController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<dynamic>>> GetUsuarios()
        {
            var usuarios = await this._usuarioService.listarusuario();
            var response = this._mapper.Map<IEnumerable<dynamic>>(usuarios); 
            return Ok(response);
        }
        [HttpPost] 
        public async Task<ActionResult<GeneralResponse<Object>>> AgregarUsuario([FromBody] AgregarUsuarioRequest _usuario)
        {
            try
            { 
                var response = await this._usuarioService.AgregarUsuario(_usuario);
                return response;
                
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Agregar usuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

    }
}
