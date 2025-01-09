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
    [Route("api/Layout")]
    [ApiController]
    public class LayoutController : ControllerBase
    {
        private readonly ILayoutGrupo _usuarioService;
        private readonly ILogger<LayoutController> _logger;
        private readonly IMapper _mapper;

        public LayoutController(
            ILayoutGrupo usuarioService,
            ILogger<LayoutController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> ListarDetalleOpGrupo(string numeroCotizacionGrupo)
        {            
           
            try
            {
                var response = await this._usuarioService.ListarDetalleOpGrupo(numeroCotizacionGrupo); 
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar layout : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }
        }

        [HttpGet("Componentes")]
        public async Task<ActionResult> ListarComponentesGrupo(string numeroCotizacionGrupo)
        {
            var response = await this._usuarioService.ListarComponentesLayout(numeroCotizacionGrupo);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }

    }
}
