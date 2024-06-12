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
using Api_Dc.Domain.Request;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Perfil")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _PerfilService;
        private readonly ILogger<PerfilController> _logger;
        private readonly IMapper _mapper;

        public PerfilController(
            IPerfilService PerfilService,
            ILogger<PerfilController> logger,
            IMapper mapper
        )
        {
            this._PerfilService = PerfilService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        //[ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<Tbl_Rol>>> GetPerfil()
        {
            var usuarios = await this._PerfilService.ListarPerfil();
            var response = this._mapper.Map<IEnumerable<Tbl_Rol>>(usuarios); 
            return Ok(response);
        }

        [HttpGet("ModulosPorRol")]
        public async Task<ActionResult> ListarModulosPorRol(int idRol)
        {
            var response = await this._PerfilService.ListarModulosPorRol( idRol);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ModulosPorUsuario")]
        public async Task<ActionResult> ListarModulosPorUsuario(int idUsuario)
        {
            var response = await this._PerfilService.ListarModulosPorUsuario(idUsuario);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("AgregarModuloRol")]
        public async Task<ActionResult<GeneralResponse<Object>>>AgregarModuloRol(int idRol, [FromBody] List<AgregarModuloRolRequest> modulos)
        {
            try
            {
                var response = await this._PerfilService.AgregarModeloRol(idRol, modulos);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpPost] 
        public async Task<ActionResult<GeneralResponse<Object>>> AgregraPerfil(int id, string nombre,string descripcion)
        {
            try
            { 
                var response = await this._PerfilService.AgregarPerfil(id,nombre,descripcion);
                return response;
                
            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<GeneralResponse<Object>>> EliminarPerfil(int id)
        {
            try
            {
                var response = await this._PerfilService.EliminarPerfil(id);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

    }
}
