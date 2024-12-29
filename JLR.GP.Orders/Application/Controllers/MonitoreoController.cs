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
using System.Text.RegularExpressions;
using Api_Dc.Application.Models.Request;

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
        [HttpGet("ListarMonitoreoSapSalidaEntrada")]
        public async Task<ActionResult> ListarMonitoreoSapSalidaEntrada(string grupoCotizacion, string fechaInicio, string fechaFin) //OK
        {
            var response = await this._usuarioService.ListarMonitoreoSapSalidaEntrada(grupoCotizacion, fechaInicio, fechaFin);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ListarReporteExplocion")]
        public async Task<ActionResult> ListarReporteExplocion(string grupoCotizacion, string fechaInicio, string fechaFin) //OK
        {
            var response = await this._usuarioService.ListarReporteExplocion(grupoCotizacion, fechaInicio, fechaFin);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpGet("ListarMantenimientoExplocion")]
        public async Task<ActionResult> ListarMantenimientoExplocion(string grupoCotizacion) //OK
        {
            var response = await this._usuarioService.ListarMantenimientoExplocion(grupoCotizacion);
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

        [HttpGet("ListarComponentesPorCodigosProducto")]
        public async Task<ActionResult<IEnumerable<Tbl_Componentes>>> GetAll(string codigosProducto,string grupo)
        {
            try
            {
                var proyectos = await _usuarioService.ListarComponentesPorCodigosProducto(codigosProducto, grupo);
                var response = this._mapper.Map<IEnumerable<Tbl_Componentes>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Orden Produccion : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
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

                var filePath = Path.Combine(rutaArchivo, "Plantilla_Carga.xlsm");
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

                return File(fileBytes, contentType, "Plantilla_Carga.xlsm");
            }
            catch (Exception ex)
            {
                // Logging the exception can be useful for debugging purposes.
                // Logger.LogError(ex, "Error al descargar el archivo");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al descargar el archivo.");
            }
        }


        [HttpPost("ExplocionarComponente")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarExplocion([FromBody] List<ExplocionComponentesRequest> request)
        {
            try
            {
                var response = await this._usuarioService.GuardarExplocion(request);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }


        [HttpPost("ExplocionarMantenimiento")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarExplocionMantenimiento([FromBody] List<ExplocionComponentesMantRequest> request)
        {
            try
            {
                var response = await this._usuarioService.GuardarExplocionMantenimiento(request);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error modificar explocion : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpPost("ExplocionarCompCargaExcel")]
        public async Task<ActionResult<GeneralResponse<Object>>> CargaExcelExplocion([FromBody] ExplocionComCargaRequest request)
        {
            try
            {
                var response = await this._usuarioService.CargaExcelExplocion(request);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpPost("GuardarSalidaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarSalidaSap([FromBody] SalidaSapRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { mensaje = "La solicitud no puede ser nula." });
                }

                var response = await this._usuarioService.GuardarCodigoSalida(request.NumeroCotizacion, request.GrupoCotizacion, request.CodigoSalida);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error al guardar salida SAP: {JsonConvert.SerializeObject(ex)}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Ocurrió un error al procesar la solicitud." });
            }
        }
        [HttpGet("EnviarEntradaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> EnviarEntradaSap(string cotizacion, string grupo , string idusuario) //OK
        {
            var response = await this._usuarioService.EnviarEntradaSap(cotizacion, grupo, idusuario);
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
