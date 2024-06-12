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
    [Route("api/Proyecto")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly IProyecto _iservice;
        private readonly ILogger<ProyectoController> _logger;
        private readonly IMapper _mapper;

        public ProyectoController(
            IProyecto iservice,
            ILogger<ProyectoController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }
          
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tbl_Proyecto>>> GetAll()
        {
            try
            {
                 var proyectos = await _iservice.listarProyecto();
                 var response = this._mapper.Map<IEnumerable<Tbl_Proyecto>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Orden Produccion : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }
        [HttpPost]
        //[Route("{Guardar}")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarDatos(string nombreProyecto)
        {
            try
            {               
                var response = await this._iservice.GuararProyecto(nombreProyecto);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpPut]
        //[Route("{Guardar}")]
        public async Task<ActionResult<GeneralResponse<Object>>> Modificar(string id,string nombreProyecto)
        {
            try
            {
                var response = await this._iservice.ModificarProyecto(id, nombreProyecto);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpDelete]
        public async Task<ActionResult<GeneralResponse<int>>> EliminarAdjunto(string id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error eliminar adjunto : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
    }
}
