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

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/listas")]
    [ApiController]
    public class ListasController : ControllerBase
    {
        private readonly IListasService _proyectoService;
        private readonly ILogger<ListasController> _logger;
        private readonly IMapper _mapper;

        public ListasController(
            IListasService proyectoService,
            ILogger<ListasController> logger,
            IMapper mapper
        )
        {
            this._proyectoService = proyectoService;
            this._logger = logger;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListasResponse>>> GetAll(string table)
        {
            try
            {
                
                var proyectos = await this._proyectoService.GetListar(table);
                var response = this._mapper.Map<IEnumerable<ListasResponse>>(proyectos);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }
        [HttpPost] 
        public async Task<ActionResult<GeneralResponse<int>>> GuardarDatos()
        {
            try
            {
                
                var ids = Request.Form["ids"]; 
                var nombre = Request.Form["nombre"];
                var accion = Request.Form["accion"];
                var tabla = Request.Form["tabla"];
                var valor = Request.Form["valor"];
                var iteams = new AgregarListasRequest
                {
                ids=ids,
                nombre=nombre,
                accion=accion,
                tabla=tabla,
                valor=valor
                };


                var response = await this._proyectoService.Agregar(iteams);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

    }
}
