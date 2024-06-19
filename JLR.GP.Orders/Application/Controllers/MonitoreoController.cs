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
using Microsoft.AspNetCore.StaticFiles;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Monitoreo")]
    [ApiController]
    public class MonitoreoController : ControllerBase
    {
        private readonly IMonitoreo _usuarioService;
        private readonly ILogger<MonitoreoController> _logger;
        private readonly IMapper _mapper;

        public MonitoreoController(
            IMonitoreo usuarioService,
            ILogger<MonitoreoController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        } 
        [HttpGet]
        public async Task<ActionResult> ListarProductoXEstacionGrupo(string grupoCotizacion, string fechaInicio, string fechaFin) //OK
        {
            var response = await this._usuarioService.ListarExplocion(grupoCotizacion, fechaInicio, fechaFin);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ListarComponenteProducto")]
        public async Task<ActionResult> listarComponentesProductoPorGrupo(string grupoCotizacion, string id) //OK
        {
            var response = await this._usuarioService.listarComponentesProductoPorGrupo(grupoCotizacion, id);
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

        [HttpGet("downloadPlantilla")]
        public IActionResult DownloadFile()
        {
            try
            {
               
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var configuration = builder.Build();
                var rutaArchivo = configuration["Archivos:rutaPlantilla"];

                var filePath = Path.Combine(rutaArchivo, "Plantilla_Carga.xlsx");
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

                return File(fileBytes, contentType, "Plantilla_Carga.xlsx");
            }
            catch (Exception ex)
            {
                // Logging the exception can be useful for debugging purposes.
                // Logger.LogError(ex, "Error al descargar el archivo");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al descargar el archivo.");
            }
        }


    }
}
