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
using System.Collections.Generic;
using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Ambiente")]
    [ApiController]
    public class AmbienteController : ControllerBase
    {
        private readonly IAmbiente _iservice;
        private readonly ILogger<AmbienteController> _logger;
        private readonly IMapper _mapper;

        public AmbienteController(
            IAmbiente iservice,
            ILogger<AmbienteController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }
          
        [HttpGet]
        public async Task<ActionResult> GetAll(string numeroCotizacion)
        {
            try
            {
                 var proyectos = await _iservice.listarAmbiente(numeroCotizacion);
                 var response = this._mapper.Map<IEnumerable<Tbl_Ambiente>>(proyectos);
                 return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar ambiente : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }
        [HttpPost]
        //[Route("{Guardar}")]
        public async Task<ActionResult<GeneralResponse<Object>>> Guardar(string cotizacion, string indice, string ambiente, string cantidad)
        {
            try
            { 
                var response = await this._iservice.GuararAmbiente(cotizacion, indice,ambiente,cantidad);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }
        } 
        [HttpDelete]
        public async Task<ActionResult<GeneralResponse<Object>>> EliminarAmbiente(string id)
        {
            try
            {
                var response = await this._iservice.EliminarAmbiente(id);
                return response;
           
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error  : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
             }
}
    }
}
