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
using Api_Dc.Domain.Contracts;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Componentes")]
    [ApiController]
    public class ComponentesController : ControllerBase
    {
        private readonly IComponentes _iservice;
        private readonly ILogger<ComponentesController> _logger;
        private readonly IMapper _mapper;

        public ComponentesController(
            IComponentes iservice,
            ILogger<ComponentesController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> ListarComponetes()
        {
            var response = await this._iservice.listarComponetes();
                return Ok(response);
          
        }
        [HttpPost]
        public async Task<ActionResult<GeneralResponse<Object>>> AgregarComponentes([FromBody] AgregarComponentesRequest componente)
        {
            try
            {
                var response = await this._iservice.AgregarComponentes(componente);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<GeneralResponse<Object>>> EliminarComponente(int id)
        {
            try
            {
                var response = await this._iservice.EliminarComponente(id);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }
        }

         

        [HttpGet("listarTelaRielTubo")]
        public async Task<ActionResult> listarTelaRielTubo(string tipo, string codigoProducto, string nombreProducto)
        {
            try { 
                var response = await this._iservice.listarTelaRielTubo(tipo, codigoProducto, nombreProducto);
                return Ok(response);
            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }

        }

        [HttpGet("listarAccesorioXProducto")]
        public async Task<ActionResult> listarAccesorioXProducto(string codigoProducto)
        {
            try
            {
                var response = await this._iservice.listarAccesorio(codigoProducto);
                return Ok(response);
            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }

        }
    }
}
