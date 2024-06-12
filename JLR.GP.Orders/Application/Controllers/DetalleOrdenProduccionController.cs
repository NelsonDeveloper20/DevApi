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
using Api_Dc.Application.Models.Response;
using System.Dynamic;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/DetalleOrdenProduccion")]
    [ApiController]
    public class DetalleOrdenProduccionController : ControllerBase
    {
        private readonly IDetalleOrdenProduccionService _iservice;
        private readonly ILogger<DetalleOrdenProduccionController> _logger;
        private readonly IMapper _mapper;

        public DetalleOrdenProduccionController(
            IDetalleOrdenProduccionService iservice,
            ILogger<DetalleOrdenProduccionController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }
          
       [HttpGet]
        public async Task<ActionResult<IEnumerable<TBL_DetalleOrdenProduccion>>> GetAll()
        {
            try
            {
                 var proyectos = await _iservice.GetAllAsync();
                 var response = this._mapper.Map<IEnumerable<TBL_DetalleOrdenProduccion>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Orden Produccion : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        } 
        [HttpPost] 
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarDatos(string tipo)
        {
            try
            {
                // Obtener los datos del formulario desde Request.Form
                IFormCollection formData = Request.Form;

                // Extraer y deserializar el JSON
                string formularioJson = formData["Formulario"];

                // Deserializar los datos
                var formularioData = JsonConvert.DeserializeObject<ExpandoObject>(formularioJson);

                // Inicializar la lista de escuadra
                List<object> escuadraList = new List<object>(); // Aquí la lista se inicializa fuera del bloque if-else

                // Verificar si la clave "Escuadra" existe en el formulario
                if (formData.ContainsKey("Escuadra"))
                {
                    // Si la clave existe, extraer y deserializar los datos de la escuadra
                    string escuadraJson = formData["Escuadra"];
                    escuadraList = JsonConvert.DeserializeObject<List<object>>(escuadraJson) ?? new List<object>();
                }
                // Llamar al método del servicio
                var response = await _iservice.AgregarProducto(formularioData, escuadraList, tipo);
                // var response = await _iservice.AgregarProducto(_producto, _escuadra);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict(ex);
            }
        } 
    }
}
