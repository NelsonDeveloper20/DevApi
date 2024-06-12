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
using Microsoft.AspNetCore.StaticFiles;

namespace ApiPortal_DataLake.Application.Controllers
{

    [Route("api/OrdenProduccion")]
    [ApiController]
    public class OrdenProduccionController : ControllerBase
    {
        private readonly IOrdenProduccion _iservice;
        private readonly ILogger<OrdenProduccionController> _logger;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;
        public OrdenProduccionController(
            IOrdenProduccion iservice,
            ILogger<OrdenProduccionController> logger,
            IMapper mapper, IWebHostEnvironment env
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tbl_OrdenProduccion>>> GetAll(string numeroCotizacion)
        {
            try
            {
                var proyectos = await _iservice.GetAllAsync(numeroCotizacion);
                var response = this._mapper.Map<IEnumerable<Tbl_OrdenProduccion>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Orden Produccion : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }

        [HttpGet("download")]
        public IActionResult DownloadFile(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El nombre del archivo no puede estar vacío.");
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                var rutaArchivo = configuration["Archivos:ruta"];

                var filePath = Path.Combine(rutaArchivo, nombre);
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound(); // Devuelve 404 si el archivo no existe
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Determinar el tipo MIME
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream"; // Tipo MIME por defecto si no se encuentra uno específico
                }

                return File(fileBytes, contentType, nombre);
            }
            catch (Exception ex)
            {
                // Logging the exception can be useful for debugging purposes.
                // Logger.LogError(ex, "Error al descargar el archivo");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al descargar el archivo.");
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
        public async Task<ActionResult<GeneralResponse<int>>> GuardarDatos(string usuario, string id, string paso, string estado, string subestado, string motivo, string? motivorechazodevolucion)
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
