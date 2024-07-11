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
    [Route("api/DetalleOpgrupo")]
    [ApiController]
    public class DetalleOpGrupoController : ControllerBase
    {
        private readonly IDetalleOpGrupo _usuarioService;
        private readonly ILogger<DetalleOpGrupoController> _logger;
        private readonly IMapper _mapper;

        public DetalleOpGrupoController(
            IDetalleOpGrupo usuarioService,
            ILogger<DetalleOpGrupoController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ListarDetalleOpGrupo([FromQuery] OPGrupoRequest Request)
        {            
            var response = await this._usuarioService.ListarDetalleOpGrupo(Request);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }

        [HttpGet("ListarFiltros")]
        public async Task<ActionResult> ListarFiltros()
        {
            var response = await this._usuarioService.ListarFiltros();
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ListarProductosPorGrupo")]
        public async Task<ActionResult> ListatarProductosDetallePorGrupo(string grupo)
        {
            var response = await this._usuarioService.ListatarProductosDetallePorGrupo(grupo);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("CambioEstadoOP")]
        public async Task<ActionResult<GeneralResponse<Object>>> EnviarEstado(string destino, [FromBody] List<GruposIDRequest> request)
        {
            try
            {
                var response = await this._usuarioService.EnviarEstado(destino, request);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpPost("AplicarCentral")]
        public async Task<ActionResult<GeneralResponse<Object>>> AplicarCentral(string cotizacionGrupo, int id,string valor)
        {
            try
            {
                var response = await this._usuarioService.AplicarCentral(cotizacionGrupo,id, valor);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

        [HttpPost("ReiniciarGrupo")]
        public async Task<ActionResult<GeneralResponse<Object>>> Reiniciar(int id)
        {
            try
            {
                var response = await this._usuarioService.ReinicioGrupo(id);
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
