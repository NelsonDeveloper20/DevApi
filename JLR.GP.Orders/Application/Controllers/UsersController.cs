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

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(
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
        //[ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await this._usuarioService.GetAllAsync();
            var response = this._mapper.Map<IEnumerable<Usuario>>(usuarios);//UsuarioResponse>>(usuarios);
            return Ok(response);
        }

        [HttpGet("roles")]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRoles()
        {
            var roles = await this._usuarioService.GetRolesAsync();
            var response = this._mapper.Map<IEnumerable<Rol>>(roles);
            return Ok(response);
        }/*

        [HttpGet("Unidad")]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<UnidadNegocio>>> GetUnidad()
        {
            var roles = await this._usuarioService.GetUnidadNegocio();
            var response = this._mapper.Map<IEnumerable<UnidadNegocio>>(roles);
            return Ok(response);
        }

        [HttpGet("Sede")]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<Sede>>> GetSede()
        {
            try
            {
                var roles = await this._usuarioService.GetSede();
            var response = this._mapper.Map<IEnumerable<Sede>>(roles);
            return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error AgregarUsuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }*/

        [HttpPost]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<GeneralResponse<int>>> AgregarUsuario()
        {
            try
            {//[FromBody] AgregarUsuarioRequest agregarUsuarioRequest
               /* string nombre = Request.Form["nombre"];
                string correo = Request.Form["correo"];
                string usuario = Request.Form["usuario"];
                var jsonData = Request.Form["DatosFoms"];
                List<AgregarUsuarioRolesRequest> dataRoles
                = JsonConvert.DeserializeObject<List<AgregarUsuarioRolesRequest>>(jsonData);

                AgregarUsuarioRequest user = new AgregarUsuarioRequest
                {
                    Nombre= nombre,
                    Correo= correo,
                    Usuario= usuario
                };*/
               // var response = await this._usuarioService.AgregarUsuario(user, usuario, dataRoles);
                return Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error AgregarUsuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        } 
        [HttpPut]
        [Route("{usuarioId}")]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<GeneralResponse<int>>> UpdateUsuario([FromRoute] int usuarioId)
        {
            try
            {

                //, [FromBody] ActualizarUsuarioRequest actualizarUsuarioRequest
                string nombre = Request.Form["nombre"];
                string correo = Request.Form["correo"];
                string usuario = Request.Form["usuario"];
                string estado = Request.Form["estado"];
                var jsonData = Request.Form["DatosFoms"];
                List<AgregarUsuarioRolesRequest> dataRoles
                = JsonConvert.DeserializeObject<List<AgregarUsuarioRolesRequest>>(jsonData);

                ActualizarUsuarioRequest actualizarUsuarioRequest = new ActualizarUsuarioRequest
                {
                    Nombre = nombre,
                    Correo = correo,
                    Usuario = usuario,
                    Estado= estado
                };
                var response = await this._usuarioService.ActualizarUsuario(usuarioId, actualizarUsuarioRequest, usuario, dataRoles);

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error UpdateUsuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpDelete]  
        public async Task<ActionResult<GeneralResponse<int>>> DeleteUser(int Id)
        {
            try
            {
                 
                var response = await this._usuarioService.EliminarRol(Id);

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error UpdateUsuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

       /* [HttpPut]
        [Route("{usuarioId}/state")]
        [ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<GeneralResponse<int>>> ModificarEstadoUsuario([FromRoute] int usuarioId, [FromBody] ModificarEstadoUsuarioRequest modificarEstadoUsuarioRequest)
        {
            try
            {
                if (modificarEstadoUsuarioRequest == null || string.IsNullOrEmpty(modificarEstadoUsuarioRequest.Estado))
                {
                    return new GeneralResponse<int>(HttpStatusCode.BadRequest);
                }

                var response = await this._usuarioService.ModificarEstadoUsuario(usuarioId, modificarEstadoUsuarioRequest.Estado, modificarEstadoUsuarioRequest.Usuario,modificarEstadoUsuarioRequest.Nombre);

                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error UpdateUsuario : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }*/
    }
}
