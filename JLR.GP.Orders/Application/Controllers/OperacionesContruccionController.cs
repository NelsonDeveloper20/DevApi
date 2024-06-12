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
    [Route("api/OperacionesContruccion")]
    [ApiController]
    public class OperacionesContruccionController : ControllerBase
    {
        private readonly IOperacionesContruccion _usuarioService;
        private readonly ILogger<OperacionesContruccionController> _logger;
        private readonly IMapper _mapper;

        public OperacionesContruccionController(
            IOperacionesContruccion usuarioService,
            ILogger<OperacionesContruccionController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ListarOperacionesConstruccion(string fecha)
        {            
            var response = await this._usuarioService.ListarOperacionesConstruccion(fecha);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("Estacion")]
        public async Task<ActionResult> ValidarEstacion(string paso, string dato)
        {
            var response = await this._usuarioService.ValidarEstacion(paso,dato);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ProductoEstacionPorEstacion")]
        public async Task<ActionResult> ListarProductoXEstacionGrupo(string grupoCotizacion, string estacion)
        {
            var response = await this._usuarioService.ListarProductoXEstacionGrupo(grupoCotizacion, estacion);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("AvanceEstacion")]
        public async Task<ActionResult> ListarAvanceEstacion(string grupoCotizacion)
        {
            var response = await this._usuarioService.ListarAvanceEstacion(grupoCotizacion);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }

        [HttpPost] 
        public async Task<ActionResult<GeneralResponse<Object>>> InsertarEstacionProducto([FromBody] EstacionProductoRequest _request)
        {
            try
            { 
                var response = await this._usuarioService.InsertarEstacionProducto(_request);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Producto estacion : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

    }
}
