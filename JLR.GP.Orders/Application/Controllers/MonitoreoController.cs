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
using System.Text;
using System.Data.Common;
using System.Dynamic;
using Api_Dc.Domain.Contracts;
using ApiPortal_DataLake.Domain.Services;
using SAPbobsCOM;
using System.Runtime.InteropServices;
using Azure.Core;
using System.Threading.Tasks;


namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Monitoreo")]
    [ApiController]
    public class MonitoreoController : ControllerBase
    {
        private readonly IDIAPIService _diapiService;
        private readonly IMonitoreo _usuarioService;
        private readonly ILogger<MonitoreoController> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public MonitoreoController(
            IMonitoreo usuarioService,
            ILogger<MonitoreoController> logger,
            IMapper mapper,
            IConfiguration configuration,
        IDIAPIService diapiService
        )
        {
            _diapiService = diapiService;
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;

            this._configuration = configuration;
        }
        [HttpPost("GetDescripcionesArticulos")]
        public async Task<ActionResult> GetDescripcionArticulov2([FromBody] List<string> itemCodes)
        {
            if (itemCodes == null || !itemCodes.Any())
            {
                return BadRequest(new { message = "Debe proporcionar al menos un código de artículo" });
            }

            // Construir IN clause con los códigos
            var codigosEscapados = itemCodes
                .Select(code => $"'{code.Replace("'", "''")}'")
                .ToList();

            string inClause = string.Join(",", codigosEscapados);


            Company company = null;
            Items item = null;

            Recordset oRec = null; // <-- Agregado
            var result = new List<object>(); // <-- Agregado
            try
            {
                // Obtener conexión de forma asíncrona
                company = await Task.Run(() => _diapiService.GetConnection());

                // Crear objeto Recordset
                oRec = (Recordset)company.GetBusinessObject(BoObjectTypes.BoRecordset);

                // Armar el query dinámico 
                string query = $@"
            SELECT 
                ""ItemCode"",
                ""ItemName"",
                ""FrgnName""
            FROM ""OITM"" 
            WHERE ""ItemCode"" IN ({inClause})";
                // Ejecutar consulta
                oRec.DoQuery(query);

                // Procesar resultados
                if (oRec.RecordCount > 0)
                {
                    while (!oRec.EoF)
                    {
                        result.Add(new
                        {
                            ItemCode = oRec.Fields.Item("ItemCode").Value.ToString(),
                            ItemName = oRec.Fields.Item("ItemName").Value.ToString(),
                            FrgnName = oRec.Fields.Item("FrgnName").Value.ToString()
                        });

                        oRec.MoveNext();
                    }
                }
                 

                return Ok(result);
            }
            catch (Exception ex)
            { 

                if (company != null)
                {
                    string diError = company.GetLastErrorDescription();
                    int diCode = company.GetLastErrorCode();

                    if (diCode != 0)
                    {
                        return StatusCode(500, new
                        {
                            message = "Error en DI API de SAP B1",
                            error = diError,
                            code = diCode
                        });
                    }
                }

                return StatusCode(500, new
                {
                    message = "Error al consultar artículo",
                    error = ex.Message,
                    otros = ex.InnerException,
                    err = ex
                });
            }
            finally
            {
                if (item != null)
                {
                    Marshal.ReleaseComObject(item);
                }

                if (company != null)
                {
                    _diapiService.ReleaseConnection(company);
                }
            }
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
        [HttpGet("ListarMonitoreoSapSalidaEntradaRevertido")]
        public async Task<ActionResult> ListarMonitoreoSapSalidaEntradaRevertido(string grupoCotizacion, string fechaInicio, string fechaFin) //OK
        {
            var response = await this._usuarioService.ListarMonitoreoSapSalidaEntradaRevertido(grupoCotizacion, fechaInicio, fechaFin);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }



        [HttpPost("revertir")]
        public async Task<ActionResult<GeneralResponse<Object>>> RevertirProcesoSap1([FromBody] RevertirRequest request)
        {
            try
            {
               

                var response = await this._usuarioService.RevertirProceso(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error al guardar merma: {JsonConvert.SerializeObject(ex)}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al procesar la solicitud.", detalle = ex.Message });
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


        [HttpPost("GuardarEnviarMerma")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarEnviarMerma( [FromBody] List<ModificarMermaRequest> request,   [FromQuery] string idusuario)
        {
            try
            {
                if (request == null || !request.Any())
                {
                    return BadRequest(new { mensaje = "La solicitud no puede estar vacía." });
                }

                if (string.IsNullOrWhiteSpace(idusuario))
                {
                    return BadRequest(new { mensaje = "El ID de usuario es requerido." });
                }

                var response = await this._usuarioService.ModificarMerma(request, idusuario);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error al guardar merma: {JsonConvert.SerializeObject(ex)}");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { mensaje = "Ocurrió un error al procesar la solicitud.", detalle = ex.Message });
            }
        }

        [HttpGet("ListarMermaAEnviar")]
        public async Task<ActionResult<IEnumerable<object>>> ListarMermaAmodificar(string grupo)
        {
            try
            {
                var proyectos = await _usuarioService.ListarMermaAmodificar( grupo);
                var response = this._mapper.Map<IEnumerable<object>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Orden Produccion : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }


        [HttpGet("ListarComponentesPorCodigosProducto")]
        public async Task<ActionResult<IEnumerable<Tbl_Componentes>>> GetAll(string codigosProducto, string grupo)
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
        [HttpPost("EnviarSalidaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> EnviarSalidaSap(string cotizacion, string grupo, string idusuario) //OK
        {
            var response = await this._usuarioService.EnviarSalidaSap(cotizacion, grupo, idusuario);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("EnviarSalidaMermaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> EnviarSalidaMermaSap(string cotizacion, string grupo, string idusuario) //OK
        {
            var response = await this._usuarioService.EnviarSalidaMermaSap(cotizacion, grupo, idusuario);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("EnviarEntradaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> EnviarEntradaSap(string cotizacion, string grupo, string idusuario) //OK
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


        [HttpPost("JSONEnviarSalidaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> JSONEnviarSalidaSap(string cotizacion, string grupo) //OK
        {
            var response = await this._usuarioService.JSONEnviarSalidaSap(cotizacion, grupo);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("JSONEnviarEntradaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> JSONEnviarEntradaSap(string cotizacion, string grupo) //OK
        {
            var response = await this._usuarioService.JSONEnviarEntradaSap(cotizacion, grupo);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }


        [HttpPost("ModificarEnviarSalidaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> ModificarEnviarSalidaSap(dynamic datosModificados) //OK
        {
            var response = await this._usuarioService.ModificarEnviarSalidaSap(datosModificados);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }
        [HttpPost("ModificarEnviarEntradaSap")]
        public async Task<ActionResult<GeneralResponse<Object>>> ModificarEnviarEntradaSap(dynamic datosModificados) //OK
        {
            var response = await this._usuarioService.ModificarEnviarEntradaSap(datosModificados);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }

        [HttpGet("ListarFormulacionRollerShade")]
        public async Task<ActionResult> ListarFormulacionRollerShade(string numCotizacion, string grupoCotizacion, string tipoProducto, string accionamiento)
        {
            var response = await this._usuarioService.ListarFormulacionRollerShade(numCotizacion, grupoCotizacion, tipoProducto, accionamiento);
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }


        [HttpPost("GuardarFormulacionRollerShade")]
        public async Task<ActionResult<GeneralResponse<Object>>> GuardarFormulacionRollerShade([FromBody] List<MonitoreoFormulacionRollerRequest> request, [FromQuery] string tipo)
        {
            try
            {
                var response = await this._usuarioService.GuardarFormulacionRollerShade(request, tipo);
                return response;

            }
            catch (Exception ex)
            {

                this._logger.LogError($"Error Agregar Perfil : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }

    }
}
