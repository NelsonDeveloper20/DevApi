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
    [Route("api/MantenimientoOP")]
    [ApiController]
    public class MantOPController : ControllerBase
    {
        private readonly IMantenimientoOp _usuarioService;
        private readonly ILogger<MantOPController> _logger;
        private readonly IMapper _mapper;

        public MantOPController(
            IMantenimientoOp usuarioService,
            ILogger<MantOPController> logger,
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
            var response = await this._usuarioService.ListarOp(numeroCotizacionGrupo);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<GeneralResponse<Object>>> EliminarOp(string id)
        {
            try
            {

                var response = await _usuarioService.EliminarMantenimientooOP(id);

                // var response = await _iservice.AgregarProducto(_producto, _escuadra);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Eliminar controller OP : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }


    }
}
