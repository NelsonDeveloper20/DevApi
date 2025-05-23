﻿using AutoMapper;
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
using Api_Dc.Application.Models.Response;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProducto _iservice;
        private readonly ILogger<ProductoController> _logger;
        private readonly IMapper _mapper;

        public ProductoController(
            IProducto iservice,
            ILogger<ProductoController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }
          
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tbl_Escuadra>>> GetAll(string id)
        {
            try
            {
                 var proyectos = await _iservice.GetAllAsync(id);
                 var response = this._mapper.Map<IEnumerable<Tbl_Escuadra>>(proyectos);

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
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarDatos()
        {
            try
            {
                IFormCollection formulario = Request.Form;
                var Orden = Request.Form["orden"];
                var archivo = formulario.Files["archivo"];
                AgregarOrdenProduccionRequest _orden = JsonConvert.DeserializeObject<AgregarOrdenProduccionRequest>(Orden); 
                var response = await this._iservice.AgregarOrden(_orden, archivo);
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
        public async Task<ActionResult<GeneralResponse<int>>> GuardarDatos(string usuario, string id, string paso, string estado, string subestado, string motivo,string? motivorechazodevolucion)
        {
            try
            {
                var _motivorechazodevolucion = "";
                _motivorechazodevolucion = motivorechazodevolucion;
                
                return null;
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
