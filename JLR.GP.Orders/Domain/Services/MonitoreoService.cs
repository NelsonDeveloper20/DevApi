using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static ApiPortal_DataLake.Domain.Services.MonitoreoService;

namespace ApiPortal_DataLake.Domain.Services
{
    public class MonitoreoService : IMonitoreo
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;

        private readonly HttpClient _httpClient;
        public MonitoreoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger,
        HttpClient httpClient
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
            _httpClient = httpClient;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configurations = builder.Build();
            CnDc_Blinds = configurations["ConnectionString:DefaultConnection"];
        }
          
        public async Task<GeneralResponse<dynamic>> ListarExplocion(string grupoCotizacion, string fechaInicio, string fechaFin) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin ", fechaFin));
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListarMonitoreoSapSalidaEntrada(string grupoCotizacion, string fechaInicio, string fechaFin)
        {
            try
            {
                List<dynamic> resultList = new List<dynamic>();

                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_listarReporteExplocionSapSalidaEntrada", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));  // Corregido el nombre del parámetro 
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                        

                            // Recorre los resultados de SqlDataReader
                            while (await reader.ReadAsync())
                            {
                                string _cotizacionGrupo = reader["CotizacionGrupo"]?.ToString();
                                
                                // Ejecuta las consultas asíncronas fuera del ciclo while
                                var detallesSalida = await this._context.Tbl_ExplocionSap
                                    .Where(x => x.CotizacionGrupo == _cotizacionGrupo && x.Tipo == "Salida")
                                    .ToListAsync();

                                var detallesEntrada = await this._context.Tbl_ExplocionSap
                                    .Where(x => x.CotizacionGrupo == _cotizacionGrupo && x.Tipo == "Entrada")
                                    .ToListAsync();
                                var row = new
                                {
                                    DetalleSalida = detallesSalida,  // Asegúrate de que estos detalles sean relevantes para cada fila
                                    DetalleEntrada = detallesEntrada,  // Asegúrate de que estos detalles sean relevantes para cada fila
                                    RucCliente = reader["RucCliente"]?.ToString(),
                                    Cliente = reader["Cliente"]?.ToString(),
                                    NumeroCotizacion = reader["NumeroCotizacion"]?.ToString(),
                                    CotizacionGrupo = reader["CotizacionGrupo"]?.ToString(),
                                    CodigoSalidaSap = reader["CodigoSalidaSap"]?.ToString(),
                                    FechaEnvioSalida = reader["FechaEnvioSalida"] != DBNull.Value ? (DateTime)reader["FechaEnvioSalida"] : DateTime.MinValue,
                                    FechaEntradaSap = reader["FechaEntradaSap"] != DBNull.Value ? (DateTime)reader["FechaEntradaSap"] : DateTime.MinValue,
                                    UsuarioEnvioSalida = reader["UsuarioEnvioSalida"]?.ToString(),
                                    UsuarioEnvioEntrada = reader["UsuarioEnvioEntrada"]?.ToString(),
                                    CodigoEntradaSap = reader["CodigoEntradaSap"]?.ToString() ?? "Pendiente"  // Defecto en caso de null
                                };

                                resultList.Add(row);
                            }
                        }
                    }
                }

                return new GeneralResponse<dynamic>(HttpStatusCode.OK, resultList);
                /*
                
SELECT 
    op.RucCliente,
    op.cliente,
    e.NumeroCotizacion,
    e.CotizacionGrupo,
    e.CodigoSalidaSap,
    e.FechaCreacion AS FechaEnvioSalida,
    e.FechaEntradaSap,
    u.Nombre + ' ' + u.Apellido AS UsuarioEnvioSalida,
    u2.Nombre + ' ' + u2.Apellido AS UsuarioEnvioEntrada,
    ISNULL(e.CodigoEntradaSap, 'Pendiente') AS CodigoEntradaSap
FROM Tbl_Explocion e
INNER JOIN Tbl_OrdenProduccion op ON op.NumeroCotizacion = e.NumeroCotizacion
LEFT JOIN Tbl_Usuario u ON u.Id = e.IdUsuarioCrea
LEFT JOIN Tbl_Usuario u2 ON u2.Id = e.IdUsuarioModifica
WHERE 
    (@NumeroCotizacion = '--' OR op.NumeroCotizacion = @NumeroCotizacion) AND
    (@FechaInicio = '--' OR e.FechaCreacion >= @FechaInicioConv) AND
    (@FechaFin = '--' OR e.FechaCreacion <= @FechaFinConv)
ORDER BY e.FechaCreacion DESC;
                */
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<dynamic>> ListarReporteExplocion(string grupoCotizacion, string fechaInicio, string fechaFin) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_listarReporteExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin ", fechaFin));
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<GeneralResponse<dynamic>> ListarMantenimientoExplocion(string grupoCotizacion) //OK OK
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_ListarMantenimientoExplocion", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", grupoCotizacion)); 
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id) //MOSTRAR EN POPUP DETALLE
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_listarComponentesProductoPorGrupo", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Grupo", grupoCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@Id", id)); 
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Tbl_Componentes>> ListarComponentesPorCodigosProducto(string codigosProducto, string grupo) //MOSTRAR COMPONENTES EN COMBOS DEL DETALLE DEL GRUPO
        {

            try
            {
                if (grupo.Contains("-0"))
                {

                    var query = await this._context.Tbl_Componentes.ToListAsync();
                    return query;
                }
                else
                {
                    var query = await this._context.Tbl_Componentes
                              .Where(o => codigosProducto.Contains(o.CodigoProducto))
                              .ToListAsync();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request)
        {
            try
            {
                var newEstacion = new Tbl_ProduccionEstacion()
                { 
                     

         IdEstacion =_request.IdEstacion,
         NumeroCotizacion =_request.NumeroCotizacion,
         CotizacionGrupo =_request.CotizacionGrupo,
         //CodigoUsuario =_request.CodigoUsuario,
         /*FechaInicio =_request.FechaInicio,
         FechaFin =_request.FechaFin,*/
         FechaCreacion =DateTime.Now,
         IdUsuarioCreacion =_request.IdUsuarioCreacion,
         Estado =1
    };
                this._context.Tbl_ProduccionEstacion.Add(newEstacion);
                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = newEstacion.Id,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0

                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> GuardarExplocion(List<ExplocionComponentesRequest> request)
        {
            using var transaction = await this._context.Database.BeginTransactionAsync();
            try
            {

                foreach (var item in request)
                { 
                    string Adicional = "NO"; 
                    var nuevaFila = new Tbl_Explocion()
                    {
                        NumeroCotizacion = item.NumeroCotizacion,
                        CotizacionGrupo = item.Grupo,
                        Nombre_Producto = item.NombreProducto,
                        Codigo_Producto = item.CodigoProducto,
                        Descrip_Componente = item.Componente,
                        Cod_Componente = item.Codigo,
                        Descripcion = item.Nombre,
                        Color = item.Color,
                        Unidad = item.UnidadMedida,
                        Cantidad = item.CantidadUtilizada,
                        Merma = item.Merma,
                        Origen = "Explocion",
                        IdUsuarioCrea = Convert.ToInt32(item.Usuario),
                        FechaCreacion = DateTime.Now, 
                        Adicional= Adicional,
                        //
                        Familia=item.Familia,
                        SubFamilia=item.SubFamilia,
                        WhsCode=item.WhsCode,
                        Serie=item.Serie,
                        Lote = item.Lote,
                    };
                    this._context.Tbl_Explocion.Add(nuevaFila);
                }
                // Actualizar el estado de grupo dentro de la transacción
                var grupo = await this._context.Tbl_DetalleOpGrupo
                                             .Where(g => g.CotizacionGrupo == request[0].Grupo)
                                             .FirstOrDefaultAsync();
                if (grupo != null)
                {
                    grupo.IdEstado = 6; // Estado TERMINADO
                    this._context.Tbl_DetalleOpGrupo.Update(grupo);
                }
                else
                {
                    throw new Exception("No se encontró el grupo especificado.");
                }
                await this._context.SaveChangesAsync();
                await transaction.CommitAsync();
                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idOrden = 1,
                };
                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Revertir todos los cambios
                this._logger.LogError($"Error en GuardarExplocion: {JsonConvert.SerializeObject(ex)}");

                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };

                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        public async Task<GeneralResponse<Object>> GuardarExplocionMantenimiento(List<ExplocionComponentesMantRequest> request)
        {
            if (request == null || !request.Any())
            {
                var errorResponse = new
                {
                    Respuesta = "La solicitud no puede estar vacía",
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.BadRequest, errorResponse);
            }

            try
            {
                var firstRequest = request.FirstOrDefault(r => r.id != "0");
                if (firstRequest == null)
                {
                    var errorResponse = new
                    {
                        Respuesta = "No se encontró un ID válido en la solicitud",
                        idOrden = 0
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.BadRequest, errorResponse);
                }

                var fechaExplocion = _context.Tbl_Explocion
                    .Where(e => e.Id == Convert.ToInt32(firstRequest.id))
                    .Select(e => e.FechaCreacion)
                    .FirstOrDefault();

                foreach (var item in request)
                {
                    if (item.id == "0")
                    {
                        var nuevaFila = new Tbl_Explocion
                        {
                            NumeroCotizacion = item.numeroCotizacion,
                            CotizacionGrupo = item.cotizacionGrupo.Trim(),
                            Nombre_Producto = item.nombre_Producto,
                            Codigo_Producto = item.codigo_Producto,
                            Descrip_Componente = item.componente,
                            Cod_Componente = item.cod_Componente,
                            Descripcion = item.descripcion,
                            Color = item.color,
                            Unidad = item.unidad,
                            Cantidad = item.cantidad,
                            Merma = item.merma,
                            Origen = "Explocion",
                            IdUsuarioCrea = Convert.ToInt32(item.idUsuarioCrea),
                            FechaCreacion = fechaExplocion
                        };
                        _context.Tbl_Explocion.Add(nuevaFila);
                    }
                    else
                    {
                        var _explocion = await _context.Tbl_Explocion.FindAsync(Convert.ToInt32(item.id));
                        if (_explocion != null)
                        {
                            _explocion.Descrip_Componente = item.componente;
                            _explocion.Cod_Componente = item.cod_Componente;
                            _explocion.Descripcion = item.descripcion;
                            _explocion.Color = item.color;
                            _explocion.Unidad = item.unidad;
                            _explocion.Cantidad = item.cantidad;
                            _explocion.Merma = item.merma;
                            _explocion.IdUsuarioModifica = Convert.ToInt32(item.idUsuarioCrea);
                            _explocion.FechaModifica = DateTime.Now;
                            _context.Tbl_Explocion.Update(_explocion);
                        }
                    }
                }

                await _context.SaveChangesAsync();

                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idOrden = 1
                };
                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        /*
        codigo de almacen	nombre de almacen
00000000	Tienda: PRODUCTO TERMINADO
00000002	TALLER DE PRODUCCION
        taller de produccion es el almacen de donde se dará la salida a los componentes
        y el almacen tienda: producto terminado es para el ingreso de los PRT

        */

        private static readonly HttpClient client = new HttpClient();

        private async Task<string> LoginAsync()
        {
            try
            {
                var loginUrl = _configuration["ApiSAP:BaseUrl"] + "/api/Login";
                var loginData = new
                {
                    Username = _configuration["ApiSAP:Username"],
                    Password = _configuration["ApiSAP:Password"]
                };

                var jsonData = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(loginUrl, content);

                var responseString = await response.Content.ReadAsStringAsync();

                // Si la respuesta no es exitosa, intentamos deserializar como error
                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
                        throw new Exception(errorResponse?.title ?? "Error de autenticación");
                    }
                    catch (Exception)
                    {
                        // Si no se puede deserializar como JSON, usamos el mensaje completo
                        throw new Exception(responseString);
                    }
                }

                // La respuesta exitosa viene en texto plano
                if (string.IsNullOrEmpty(responseString))
                {
                    throw new Exception("No se recibió el token.");
                }

                return responseString;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión: {ex.Message}");
            }
            catch (Exception ex) when (ex.Message.Contains("Unauthorized"))
            {
                throw new Exception("Credenciales inválidas");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener token: {ex.Message}");
            }
        }

        public async Task<GeneralResponse<Object>> CargaExcelExplocion(ExplocionComCargaRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Validación de datos
                var (_TbCotizacion, grupoExplotado, validationError) = await ValidateRequest(request);
                if (!string.IsNullOrEmpty(validationError))
                {
                    return new GeneralResponse<Object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Error al procesar la carga de datos.", Detalle = validationError });
                }
                // Procesar items
                var (listItems, processingError) = ProcessItems(request, grupoExplotado, _TbCotizacion);
                if (!string.IsNullOrEmpty(processingError))
                {
                    return new GeneralResponse<Object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Error al procesar la carga de datos.", Detalle = processingError });
                }

                // Convertir el string a DateTime
                DateTime fechaCot = DateTime.ParseExact(_TbCotizacion.FechaCotizacion, "yyyyMMdd", null);
                DateTime fechaVen = DateTime.ParseExact(_TbCotizacion.FechaVenta, "yyyyMMdd", null);
                // Crear cotización
                var cotizacion = new CotizacionCabecera
                { 
                    
                    DocNum=request.Cotizacion,
                    DocDate = fechaCot.ToString("dd/MM/yyyy"),
                    TaxDate = fechaVen.ToString("dd/MM/yyyy"),
                    Comments = "Salida",
                    Reference2 = "Ref2",
                    U_EXX_TIPOOPER = "10",
                    IdSistemaExterno = _TbCotizacion.Id.ToString(),
                    DocumentLines = listItems
                };
                // Obtener token y hacer llamada a API
               var token = await LoginAsync();
                var inventoryUrl = _configuration["ApiSAP:BaseUrl"] + "/api/InventoryGenExit";
                var jsonInventoryData = System.Text.Json.JsonSerializer.Serialize(cotizacion);
                var content = new StringContent(jsonInventoryData, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(inventoryUrl, content);                 
                // Leer la respuesta antes de validar el estado
                var responseString = await response.Content.ReadAsStringAsync(); //"{\"DocumentLines\":[{\"ItemCode\":\"ACCRS00000114\",\"ItemDescription\":\"\",\"Quantity\":5,\"UnitPrice\":5,\"AcctCode\":\"_SYS00000025169\",\"WareHouseCode\":\"00000000\",\"Project\":\"\",\"CostingCode\":\"\",\"CostingCode2\":\"\",\"CostingCode3\":\"\",\"CostingCode4\":\"\",\"CostingCode5\":\"\",\"IdOrdenVenta\":\"17\",\"IdSistemaExterno\":\"4\",\"IdLineaSistemaE\":\"4\",\"FamiliaPT\":\"ACC\",\"SubFamiliaPT\":\"RS\",\"Ancho\":0,\"Alto\":0,\"BatchNumbers\":[],\"SerialNumbers\":[]},{\"ItemCode\":\"ACCRS00000167\",\"ItemDescription\":\"\",\"Quantity\":5,\"UnitPrice\":5,\"AcctCode\":\"_SYS00000025169\",\"WareHouseCode\":\"00000000\",\"Project\":\"\",\"CostingCode\":\"\",\"CostingCode2\":\"\",\"CostingCode3\":\"\",\"CostingCode4\":\"\",\"CostingCode5\":\"\",\"IdOrdenVenta\":\"17\",\"IdSistemaExterno\":\"4\",\"IdLineaSistemaE\":\"4\",\"FamiliaPT\":\"ACC\",\"SubFamiliaPT\":\"RS\",\"Ancho\":0,\"Alto\":0,\"BatchNumbers\":[],\"SerialNumbers\":[]},{\"ItemCode\":\"PALRS00000001\",\"ItemDescription\":\"\",\"Quantity\":5,\"UnitPrice\":5,\"AcctCode\":\"_SYS00000025169\",\"WareHouseCode\":\"00000000\",\"Project\":\"\",\"CostingCode\":\"\",\"CostingCode2\":\"\",\"CostingCode3\":\"\",\"CostingCode4\":\"\",\"CostingCode5\":\"\",\"IdOrdenVenta\":\"17\",\"IdSistemaExterno\":\"4\",\"IdLineaSistemaE\":\"4\",\"FamiliaPT\":\"PAL\",\"SubFamiliaPT\":\"RS\",\"Ancho\":0,\"Alto\":0,\"BatchNumbers\":[],\"SerialNumbers\":[]},{\"ItemCode\":\"TELRS00000153\",\"ItemDescription\":\"\",\"Quantity\":5,\"UnitPrice\":5,\"AcctCode\":\"_SYS00000025169\",\"WareHouseCode\":\"00000000\",\"Project\":\"\",\"CostingCode\":\"\",\"CostingCode2\":\"\",\"CostingCode3\":\"\",\"CostingCode4\":\"\",\"CostingCode5\":\"\",\"IdOrdenVenta\":\"17\",\"IdSistemaExterno\":\"4\",\"IdLineaSistemaE\":\"4\",\"FamiliaPT\":\"TEL\",\"SubFamiliaPT\":\"RS\",\"Ancho\":0,\"Alto\":0,\"BatchNumbers\":[],\"SerialNumbers\":[]},{\"ItemCode\":\"TELTO00000013\",\"ItemDescription\":\"\",\"Quantity\":5,\"UnitPrice\":5,\"AcctCode\":\"_SYS00000025169\",\"WareHouseCode\":\"00000000\",\"Project\":\"\",\"CostingCode\":\"\",\"CostingCode2\":\"\",\"CostingCode3\":\"\",\"CostingCode4\":\"\",\"CostingCode5\":\"\",\"IdOrdenVenta\":\"17\",\"IdSistemaExterno\":\"4\",\"IdLineaSistemaE\":\"4\",\"FamiliaPT\":\"TEL\",\"SubFamiliaPT\":\"TO\",\"Ancho\":0,\"Alto\":0,\"BatchNumbers\":[],\"SerialNumbers\":[]}],\"DocEntry\":22,\"DocNum\":\"2700007\",\"DocDate\":\"2025-01-06\",\"TaxDate\":\"2025-01-06\",\"Comments\":\"Salida\",\"Reference2\":\"Ref2\",\"IdSistemaExterno\":\"\",\"U_EXX_TIPOOPER\":\"\"}";// await response.Content.ReadAsStringAsync(); 
                Console.WriteLine("Respuesta del servidor: " + responseString);

                // Manejar respuesta no exitosa
                if (!response.IsSuccessStatusCode)
                {
                    return new GeneralResponse<Object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Error al procesar la carga de datos.", Detalle = responseString });
                }
                else
                {
                    // Usamos JsonDocument para parsear el JSON sin clases predefinidas
                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        // Acceder a DocEntry usando el path adecuado
                        JsonElement root = doc.RootElement;
                        // Verificar si "DocEntry" está presente y obtener su valor
                        if (root.TryGetProperty("DocEntry", out JsonElement docEntryElement))
                        {
                            int docEntry = docEntryElement.GetInt32();
                            Console.WriteLine("DocEntry: " + docEntry);
                            // Guardar en base de datos
                            await SaveExplocionSap(cotizacion, docEntry.ToString(), request);
                            await transaction.CommitAsync(); 
                            return new GeneralResponse<Object>(
                                HttpStatusCode.OK,
                                new { Respuesta = "OK", idOrden = 0 });
                        }
                        else
                        {
                            // Si no encontramos el DocEntry
                            return new GeneralResponse<Object>(
                                HttpStatusCode.BadRequest,
                                new { Respuesta = "Error: DocEntry no encontrado.", Detalle = "Error: DocEntry (codigo Salida) no encontrado. " });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error al procesar la carga de datos");
                return new GeneralResponse<Object>(
                    HttpStatusCode.InternalServerError,
                    new { Respuesta = "Error al procesar la carga de datos.", Detalle = ex.Message +" DETALLE: "+ex.InnerException.Message });
            }
        }

        private async Task<(Tbl_OrdenProduccion Cotizacion, Tbl_DetalleOpGrupo Grupo, string Error)> ValidateRequest(
            ExplocionComCargaRequest request)
        {
            var _TbCotizacion = await _context.Tbl_OrdenProduccion
                .FirstOrDefaultAsync(o => o.NumeroCotizacion == request.Cotizacion);

            var error = _TbCotizacion == null ? "<b>-</b>Numero cotización No existe" : "";

            var grupoExplotado = await _context.Tbl_DetalleOpGrupo
                .FirstOrDefaultAsync(e => e.CotizacionGrupo == request.GrupoCotizacion
                                     && e.NumeroCotizacion == request.Cotizacion);

            if (grupoExplotado == null)
            {
                error += (error != "" ? "<br/>" : "") + "<b>-</b>Grupo Cotización no existe";
            }

            return (_TbCotizacion, grupoExplotado, error);
        }

        private (List<Item> Items, string Error) ProcessItems(
            ExplocionComCargaRequest request,
            Tbl_DetalleOpGrupo grupo,
            Tbl_OrdenProduccion cotizacion)
        {
            var listItems = new List<Item>();
            var error = "";

            foreach (var item in request.DocumentLines)
            {
                var (batchNumbers, batchError) = ValidateBatchNumbers(item);
                if (!string.IsNullOrEmpty(batchError))
                {
                    error += (error != "" ? "<br/>" : "") + batchError;
                    continue;
                }

                var (serialNumbers, serialError) = ValidateSerialNumbers(item);
                if (!string.IsNullOrEmpty(serialError))
                {
                    error += (error != "" ? "<br/>" : "") + serialError;
                    continue;
                }

                listItems.Add(new Item
                {
                    ItemCode = item.ItemCode,
                    Quantity = item.Quantity,
                    WarehouseCode = _configuration["ApiSAP:WarehouseCode"],
                    AcctCode = _configuration["ApiSAP:AcctcodeSalida"],// item.AcctCode,
                    CostingCode = item.CostingCode,
                    ProjectCode = "",
                    CostingCode2 = item.CostingCode2,
                    CostingCode3 = item.CostingCode3,
                    CostingCode4 = item.CostingCode4,
                    CostingCode5 = item.CostingCode5,
                    IdSistemaExterno = grupo.Id.ToString(),
                    IdLineaSistemaE = grupo.Id.ToString(),
                    IdOrdenVenta =  cotizacion.DocEntrySap.ToString(),
                    FamiliaPT = item.FamiliaPT,
                    SubFamiliaPT=item.subFamiliaPT,
                    BatchNumbers = batchNumbers,
                    SerialNumbers = serialNumbers
                });
            }

            return (listItems, error);
        }

        private (List<BatchNumbers> Numbers, string Error) ValidateBatchNumbers(ExplocionProductosReques item)
        {
            var numbers = new List<BatchNumbers>();
            var error = "";

            if (!string.IsNullOrEmpty(item.BatchNumberCode?.Trim()))
            {
                if (string.IsNullOrEmpty(item.BatchQuantity.ToString()?.Trim()))
                {
                    error = "<b>-</b>Cantidad Lote(BatchQuantity) es requerido";
                }
                else
                {
                    numbers.Add(new BatchNumbers
                    {
                        BatchNumber = item.BatchNumberCode,
                        Quantity = item.BatchQuantity
                    });
                }
            }

            return (numbers, error);
        }

        private (List<SerialNumber> Numbers, string Error) ValidateSerialNumbers(ExplocionProductosReques item)
        {
            var numbers = new List<SerialNumber>();
            var error = "";

            if (!string.IsNullOrEmpty(item.SerialNumberCode?.Trim()))
            {
                if (string.IsNullOrEmpty(item.SerialQuantity.ToString()?.Trim()))
                {
                    error = "<b>-</b>Cantidad Serie(SerialQuantity) es requerido";
                }
                else
                {
                    numbers.Add(new SerialNumber
                    {
                        SerialNumberCode = Convert.ToInt32(item.SerialNumberCode),
                        Quantity = item.SerialQuantity
                    });
                }
            }

            return (numbers, error);
        }

        private async Task SaveExplocionSap(CotizacionCabecera cotizacion, string responseString, ExplocionComCargaRequest DataExcel )
        {
            foreach (var item in cotizacion.DocumentLines)
            {
                var cotizacionEntity = new Tbl_ExplocionSap
                {
                    DocDate = cotizacion.DocDate.ToString(),
                    TaxDate = cotizacion.TaxDate.ToString(),
                    Comments = cotizacion.Comments,
                    Reference2 = cotizacion.Reference2,
                    U_EXX_TIPOOPER = cotizacion.U_EXX_TIPOOPER,
                    IdSistemaExterno = cotizacion.IdSistemaExterno,
                    IdOrdenVenta = item.IdOrdenVenta.ToString(),
                    ItemCode = item.ItemCode,
                    Quantity = item.Quantity.ToString(),
                    WarehouseCode = item.WarehouseCode,
                    AcctCode = _configuration["ApiSAP:AcctcodeSalida"],// item.AcctCode,
                    CostingCode = item.CostingCode,
                    ProjectCode = item.ProjectCode,
                    CostingCode2 = item.CostingCode2,
                    CostingCode3 = item.CostingCode3,
                    CostingCode4 = item.CostingCode4,
                    CostingCode5 = item.CostingCode5,
                    IdLineaSistemaE = item.IdLineaSistemaE,
                    FamiliaPT = item.FamiliaPT,
                    SubFamiliaPT=item.SubFamiliaPT,
                    BatchNumbers = JsonConvert.SerializeObject(item.BatchNumbers),
                    SerialNumbers = JsonConvert.SerializeObject(item.SerialNumbers),
                    CodigoSalidaSap = responseString ,
                    Tipo="Salida",
                    CotizacionGrupo=DataExcel.GrupoCotizacion,
                    IdUsuarioCrea = Convert.ToInt32(DataExcel.Usuario),
                    FechaCreacion = DateTime.Now,
                };

                _context.Tbl_ExplocionSap.Add(cotizacionEntity);


                var nuevaFilaExplo = new Tbl_Explocion()
                {
                    NumeroCotizacion = DataExcel.Cotizacion,
                    CotizacionGrupo = DataExcel.GrupoCotizacion,
                    //Nombre_Producto = "ROLLER SHADE",
                    //Codigo_Producto = "PRTS",
                    Descrip_Componente = item.ItemCode,
                    Cod_Componente = item.ItemCode,
                    Descripcion = item.ItemCode,
                    Color = "",
                    Unidad = "",
                    Cantidad = item.Quantity.ToString(),
                    Merma = "",
                    Origen = "Carga Sap",
                    IdUsuarioCrea = Convert.ToInt32(DataExcel.Usuario),
                    FechaCreacion = DateTime.Now,
                    CodigoSalidaSap = responseString,
                    Adicional = "",
                    //
                    Familia = item.FamiliaPT,
                    WhsCode = item.WarehouseCode,
                    SubFamilia = item.SubFamiliaPT,
                    Serie = "",
                    Lote ="",
                    //Alto = item.Alto,
                    //Ancho = item.Ancho,
                };

                this._context.Tbl_Explocion.Add(nuevaFilaExplo);
            } 
            // Actualizar el estado de grupo dentro de la transacción
            var grupo = await this._context.Tbl_DetalleOpGrupo
                                         .Where(g => g.CotizacionGrupo == DataExcel.GrupoCotizacion)
                                         .FirstOrDefaultAsync();
            
                grupo.IdEstado = 6; // Estado TERMINADO
                this._context.Tbl_DetalleOpGrupo.Update(grupo);             

            await _context.SaveChangesAsync();
        }
        public async Task<GeneralResponse<Object>> ModificarEnviarSalidaSap(dynamic request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in request)
                {
                    var itemExplocion = await _context.Tbl_Explocion.FindAsync((int)item.Id);

                    if (itemExplocion != null)
                    {
                        itemExplocion.Cod_Componente = item.ItemCode;
                        itemExplocion.Descripcion = item.ItemDescription;
                        itemExplocion.Cantidad = item.Quantity.ToString();
                        itemExplocion.CodFamilia = item.FamiliaPT;
                        itemExplocion.SubFamilia = item.SubFamiliaPT;
                        // Manejar lotes - convertir array de BatchNumbers a string separado por comas
                        if (item.BatchNumbers != null && item.BatchNumbers.Count > 0)
                        {
                            var lotes = new List<string>();
                            foreach (var batch in item.BatchNumbers)
                            {
                                if (!string.IsNullOrWhiteSpace(batch.BatchNumber.ToString()))
                                {
                                    lotes.Add(batch.BatchNumber.ToString().Trim());
                                }
                            }
                            itemExplocion.Lote = string.Join(",", lotes);
                        }
                        else
                        {
                            itemExplocion.Lote = "";
                        }

                        // Manejar series - convertir array de SerialNumbers a string separado por comas
                        if (item.SerialNumbers != null && item.SerialNumbers.Count > 0)
                        {
                            var series = new List<string>();
                            foreach (var serial in item.SerialNumbers)
                            {
                                if (!string.IsNullOrWhiteSpace(serial.SerialNumber.ToString()))
                                {
                                    series.Add(serial.SerialNumber.ToString().Trim());
                                }
                            }
                            itemExplocion.Serie = string.Join(",", series); // Asumiendo que tienes un campo Serie en la tabla
                        }
                        else
                        {
                            itemExplocion.Serie = "";
                        }

                        _context.Tbl_Explocion.Update(itemExplocion);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var jsonresponse = new
                {
                    Respuesta = "Guardado correctamente",
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        } 
        public async Task<GeneralResponse<Object>> JSONEnviarSalidaSap(string P_NumeroCotizacion, string P_grupoCotizacion)
        {
             
            try
            {
                var _TbCotizacion = await _context.Tbl_OrdenProduccion
                    .FirstOrDefaultAsync(o => o.NumeroCotizacion == P_NumeroCotizacion);
                var grupoExplotado = await _context.Tbl_DetalleOpGrupo
               .FirstOrDefaultAsync(e => e.CotizacionGrupo == P_grupoCotizacion
                                    && e.NumeroCotizacion == P_NumeroCotizacion);
                var explocionSap = await _context.Tbl_Explocion
                    .Where(e => e.CotizacionGrupo == P_grupoCotizacion && e.Cod_Componente != "Ninguno")
                    .ToListAsync();

                var listItems = new List<dynamic>();
                var error = "";



                // Convertir el string a DateTime
                DateTime fechaCot = DateTime.ParseExact(_TbCotizacion.FechaCotizacion, "yyyyMMdd", null);
                DateTime fechaVen = DateTime.ParseExact(_TbCotizacion.FechaVenta, "yyyyMMdd", null);
                //ITEMS
                foreach (var item in explocionSap)
                {
                    var batchNumbers = new List<BatchNumbers>();
                    var serialNumbers = new List<SerialNumber>();
                    if (!string.IsNullOrWhiteSpace(item.Lote))
                    {
                        var lotes = item.Lote.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                             .Select(l => l.Trim());
                        foreach (var i in lotes)
                        {
                            batchNumbers.Add(new BatchNumbers
                            {
                                BatchNumber = i,
                                Quantity = Convert.ToDecimal(item.Cantidad)
                            });
                        }
                    }
                    // Manejar series
                    if (!string.IsNullOrWhiteSpace(item.Serie))
                    {
                        var series = item.Serie.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                              .Select(s => s.Trim());
                        foreach (var serie in series)
                        {
                            serialNumbers.Add(new SerialNumber
                            {
                                SerialNumberCode =Convert.ToInt32( serie),
                                Quantity = 1 // Para series siempre es 1
                            });
                        }
                    }
                    string jsonLotes = JsonConvert.SerializeObject(batchNumbers ?? new List<BatchNumbers>());
                    string jsonSeries = JsonConvert.SerializeObject(serialNumbers ?? new List<SerialNumber>());
                    listItems.Add(new 
                    {
                        Id=item.Id,
                        ItemCode = item.Cod_Componente,
                        ItemDescription=item.Descripcion,
                        Quantity = Convert.ToDecimal(item.Cantidad),
                        WarehouseCode = _configuration["ApiSAP:WarehouseCode"],
                        AcctCode = _configuration["ApiSAP:AcctcodeSalida"],
                        CostingCode = "",
                        ProjectCode = "",
                        CostingCode2 = "",
                        CostingCode3 = "",
                        CostingCode4 = "",
                        CostingCode5 = "",
                        IdSistemaExterno = grupoExplotado.Id.ToString(),
                        IdLineaSistemaE = grupoExplotado.Id.ToString(),
                        IdOrdenVenta = _TbCotizacion.DocEntrySap.ToString(),
                        FamiliaPT = item.CodFamilia,
                        SubFamiliaPT = item.SubFamilia,
                        BatchNumbers = batchNumbers,
                        SerialNumbers = serialNumbers,
                    });
                }
                // Crear cotización
                var cotizacion = new 
                {
                    DocNum = _TbCotizacion.NumeroCotizacion,
                    DocDate = fechaCot.ToString("dd/MM/yyyy"),
                    TaxDate = fechaVen.ToString("dd/MM/yyyy"),
                    Comments = "Salida",
                    Reference2 = "Ref2",
                    U_EXX_TIPOOPER = "10",
                    IdSistemaExterno = _TbCotizacion.Id.ToString(),
                    DocumentLines = listItems
                };
                 
                var jsonInventoryData = System.Text.Json.JsonSerializer.Serialize(cotizacion);  
                 
                            var jsonresponse = new
                            {
                                Respuesta = jsonInventoryData,
                                idOrden = 0
                            };
                            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                     
                    



            }
            catch (Exception ex)
            { 
                _logger.LogError($"JSON Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> ModificarEnviarEntradaSap(dynamic request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var item in request)
                {
                   
                    var row = await _context.Tbl_ExplocionSap.FindAsync((int)item.Id);

                    if (row != null)
                    {
                        // Actualizar solo los campos modificables 

                        row.ItemCode = item.ItemCode ?? row.ItemCode;
                        row.ItemDescription = item.ItemDescription ?? row.ItemDescription;
                        row.Quantity = item.Quantity ?? row.Quantity;
                        row.WarehouseCode = item.WarehouseCode ?? row.WarehouseCode;
                        row.AcctCode = item.AcctCode ?? row.AcctCode;
                        row.CostingCode = item.CostingCode ?? row.CostingCode;
                        row.CostingCode2 = item.CostingCode2 ?? row.CostingCode2;
                        row.CostingCode3 = item.CostingCode3 ?? row.CostingCode3;
                        row.CostingCode4 = item.CostingCode4 ?? row.CostingCode4;
                        row.CostingCode5 = item.CostingCode5 ?? row.CostingCode5;
                        row.FamiliaPT = item.FamiliaPT ?? row.FamiliaPT;
                        row.SubFamiliaPT = item.SubFamiliaPT ?? row.SubFamiliaPT;
                         

                        _context.Tbl_ExplocionSap.Update(row);
                    } 
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var jsonresponse = new
                {
                    respuesta = "Modificado",
                    detalle = "Datos modificados correctamente"
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Error en ModificarEnviarEntradaSap: {JsonConvert.SerializeObject(ex)}");

                var jsonresponse = new
                {
                    respuesta = "Error",
                    detalle = ex.Message
                };

                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> JSONEnviarEntradaSap(string P_NumeroCotizacion, string P_grupoCotizacion)
        {
           try
            {
                var _TbCotizacion = await _context.Tbl_OrdenProduccion
                   .FirstOrDefaultAsync(o => o.NumeroCotizacion == P_NumeroCotizacion);

                var datEntrada = await _context.TBL_DetalleOrdenProduccion.Where(d => d.CotizacionGrupo == P_grupoCotizacion).ToListAsync();

                var explocionSap = await _context.Tbl_ExplocionSap
                .Where(e => e.CotizacionGrupo == P_grupoCotizacion)
                .FirstAsync();
                var listItems = new List<ItemEntrada>();
                var error = "";



                // Convertir el string a DateTime
                DateTime fechaCot = DateTime.ParseExact(_TbCotizacion.FechaCotizacion, "yyyyMMdd", null);
                DateTime fechaVen = DateTime.ParseExact(_TbCotizacion.FechaVenta, "yyyyMMdd", null);
                foreach (var item in datEntrada)
                {

                    var batchNumbers = JsonConvert.DeserializeObject<List<BatchNumbers>>("[]");
                    var serialNumbers = JsonConvert.DeserializeObject<List<SerialNumber>>("[]");
                    listItems.Add(new ItemEntrada
                    {
                        ItemCode = item.CodigoProducto.Trim(),
                        ItemDescription = item.NombreProducto?.Trim() ?? "",
                        Quantity = Convert.ToDecimal(item.Cantidad),
                        WarehouseCode = _configuration["ApiSAP:WarehouseCodeEntrada"],
                        AcctCode = _configuration["ApiSAP:AcctcodeEntrada"],// //item.AcctCode,  
                        //FamiliaPT = "PRT", 
                        //SuibFamiliaPT = item.SubFamilia?.Trim() ?? "",
                        //SubFamiliaPT =item.SubFamiliaPT,
                        //BatchNumbers = batchNumbers,
                        //SerialNumbers = serialNumbers,
                        IdSalida = Convert.ToInt32(explocionSap.CodigoSalidaSap),
                        Alto = Convert.ToDecimal(item.Alto),
                        Ancho = Convert.ToDecimal(item.Ancho),
                        IdOrdenVenta = _TbCotizacion.DocEntrySap.ToString()

                    });
                     
                }
                var cotizacion = new CotizacionCabeceraEntrada
                {//2024-10-03
                    DocEntry = Convert.ToInt32(explocionSap.CodigoSalidaSap),
                    DocNum = P_NumeroCotizacion + "" + DateTime.Now.ToString("ddHHmmss"),
                    DocDate = fechaCot.ToString("yyyy-MM-dd"),
                    TaxDate = fechaVen.ToString("yyyy-MM-dd"),
                    Comments = "Entrada",
                    Reference2 = "Ref2",
                    U_EXX_TIPOOPER = "19",
                    IdSistemaExterno = _TbCotizacion.Id.ToString(),
                    DocumentLines = listItems
                };

                var jsonInventoryData = System.Text.Json.JsonSerializer.Serialize(cotizacion);                 
                            var jsonresponse = new
                            {
                                Respuesta = jsonInventoryData,
                                idOrden = 0
                            };
                            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);    
            }
            catch (Exception ex)
            { 
                _logger.LogError($"JSON Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        public async Task<GeneralResponse<Object>> EnviarSalidaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario)
        {

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var _TbCotizacion = await _context.Tbl_OrdenProduccion
                    .FirstOrDefaultAsync(o => o.NumeroCotizacion == P_NumeroCotizacion);
                var grupoExplotado = await _context.Tbl_DetalleOpGrupo
               .FirstOrDefaultAsync(e => e.CotizacionGrupo == P_grupoCotizacion
                                    && e.NumeroCotizacion == P_NumeroCotizacion);
                var explocionSap = await _context.Tbl_Explocion
                    .Where(e => e.CotizacionGrupo == P_grupoCotizacion && e.Cod_Componente!= "Ninguno")
                    .ToListAsync();

                var listItems = new List<Item>();
                var error = "";



                // Convertir el string a DateTime
                DateTime fechaCot = DateTime.ParseExact(_TbCotizacion.FechaCotizacion, "yyyyMMdd", null);
                DateTime fechaVen = DateTime.ParseExact(_TbCotizacion.FechaVenta, "yyyyMMdd", null);
                //ITEMS
                foreach (var item in explocionSap)
                {
                    var batchNumbers = new List<BatchNumbers>();
                    var serialNumbers = new List<SerialNumber>(); 
                    if (!string.IsNullOrWhiteSpace(item.Lote))
                    {
                        var lotes = item.Lote.Split(",", StringSplitOptions.RemoveEmptyEntries)
                                             .Select(l => l.Trim());
                        foreach (var i in lotes)
                        {
                            batchNumbers.Add(new BatchNumbers
                            {
                                BatchNumber = i,
                                Quantity = Convert.ToDecimal(item.Cantidad)
                            });
                        }
                    }
                    string jsonLotes = JsonConvert.SerializeObject(batchNumbers ?? new List<BatchNumbers>());
                    string jsonSeries = JsonConvert.SerializeObject(serialNumbers ?? new List<SerialNumber>());
                    listItems.Add(new Item
                    { 
                        ItemCode = item.Cod_Componente,
                        ItemDescription=item.Descripcion,
                        Quantity = Convert.ToDecimal(item.Cantidad),
                        WarehouseCode = _configuration["ApiSAP:WarehouseCode"],
                        AcctCode = _configuration["ApiSAP:AcctcodeSalida"],// //item.AcctCode,
                        CostingCode = "",//item.CostingCode,
                        ProjectCode = "",
                        CostingCode2 ="",// item.CostingCode2,
                        CostingCode3 = "",// item.CostingCode3,
                        CostingCode4 = "",//item.CostingCode4,
                        CostingCode5 = "",//item.CostingCode5,
                        IdSistemaExterno = grupoExplotado.Id.ToString(),
                        IdLineaSistemaE = grupoExplotado.Id.ToString(),
                        IdOrdenVenta = _TbCotizacion.DocEntrySap.ToString(),
                        FamiliaPT = item.CodFamilia,
                        SubFamiliaPT = item.SubFamilia, 
                        BatchNumbers = batchNumbers,
                        SerialNumbers = serialNumbers, 
                    });
                    var cotizacionEntity = new Tbl_ExplocionSap
                    {
                        DocDate = fechaCot.ToString("dd/MM/yyyy"),
                        TaxDate = fechaVen.ToString("dd/MM/yyyy"),
                        Comments = "Salida",
                        Reference2 = "Ref2",
                        U_EXX_TIPOOPER = "10",
                        IdSistemaExterno =  _TbCotizacion.Id.ToString(),
                        IdOrdenVenta = _TbCotizacion.DocEntrySap.ToString(),
                        ItemCode = item.Cod_Componente,
                        ItemDescription=item.Descripcion,
                        Quantity = item.Cantidad,
                        WarehouseCode = _configuration["ApiSAP:WarehouseCode"],
                        AcctCode = _configuration["ApiSAP:AcctcodeSalida"],
                        CostingCode = "",//item.CostingCode,
                        ProjectCode = "",
                        CostingCode2 = "",// item.CostingCode2,
                        CostingCode3 = "",// item.CostingCode3,
                        CostingCode4 = "",//item.CostingCode4,
                        CostingCode5 = "",//item.CostingCode5,
                        IdLineaSistemaE = grupoExplotado.Id.ToString(),
                        FamiliaPT = item.CodFamilia,
                        SubFamiliaPT = item.SubFamilia,
                        BatchNumbers = jsonLotes,//"[]",
                        SerialNumbers = jsonSeries,// "[]", 
                        //CodigoSalidaSap = responseString,
                        Tipo = "Salida",
                        CotizacionGrupo = P_grupoCotizacion,
                        IdUsuarioCrea = Convert.ToInt32(idusuario),
                        FechaCreacion = DateTime.Now, 
                    };

                    _context.Tbl_ExplocionSap.Add(cotizacionEntity);
                }
                // Crear cotización
                var cotizacion = new CotizacionCabecera
                { 
                    DocNum = _TbCotizacion.NumeroCotizacion,
                    DocDate = fechaCot.ToString("dd/MM/yyyy"),
                    TaxDate = fechaVen.ToString("dd/MM/yyyy"),
                    Comments = "Salida",
                    Reference2 = "Ref2",
                    U_EXX_TIPOOPER = "10",
                    IdSistemaExterno = _TbCotizacion.Id.ToString(),
                    DocumentLines = listItems
                };

                var token = await LoginAsync();
                var inventoryUrl = _configuration["ApiSAP:BaseUrl"] + "/api/InventoryGenExit";
                var jsonInventoryData = System.Text.Json.JsonSerializer.Serialize(cotizacion);
                var content = new StringContent(jsonInventoryData, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(inventoryUrl, content);

                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta del servidor: " + responseString);

                if (!response.IsSuccessStatusCode)
                {
                    await transaction.RollbackAsync();
                    // Buscar si es un error de inventario negativo
                    if (responseString.Contains("La cantidad recae en un inventario negativo"))
                    {
                        // Extraer ItemCode y línea
                        var match = Regex.Match(responseString, @"\[(?<campo>IGE1\.ItemCode)\]\[line: (?<linea>\d+)\]");
                        if (match.Success)
                        {
                            var linea = int.Parse(match.Groups["linea"].Value);
                            var itemCode = cotizacion.DocumentLines[linea - 1].ItemCode; // línea 12 → índice 11

                            var mensajePersonalizado = $"No hay unidades disponibles del ítem {itemCode} en el almacén {cotizacion.DocumentLines[linea - 1].WarehouseCode}.";
                 
                            return new GeneralResponse<Object>(
                                HttpStatusCode.BadRequest,
                                new { Respuesta = "Error de stock", Detalle = responseString  + " Comentario: "+ mensajePersonalizado });
                        }
                    }

                    // Otro tipo de error
                    return new GeneralResponse<Object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Error al procesar la carga de datos.", Detalle = responseString });
                }
                else
                {
                    // Usamos JsonDocument para parsear la respuesta JSON
                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        JsonElement root = doc.RootElement;

                        // Verificar si "DocEntry" está presente y obtener su valor
                        if (root.TryGetProperty("DocEntry", out JsonElement docEntryElement))
                        {
                            int docEntry =  docEntryElement.GetInt32();
                            Console.WriteLine("DocEntry: " + docEntry);
                            // Guardar en base de datos
                             
                            var explocionTbList = await _context.Tbl_Explocion
                            .Where(e => e.NumeroCotizacion == P_NumeroCotizacion && e.CotizacionGrupo == P_grupoCotizacion)
                            .ToListAsync();

                            if (explocionTbList.Any())
                            {
                                foreach (var item in explocionTbList)
                                {
                                    item.CodigoSalidaSap = docEntry.ToString();
                                    item.FechaSalidaSap = DateTime.Now;
                                    item.IdUsuarioModifica = Convert.ToInt32(idusuario);
                                }

                                _context.Tbl_Explocion.UpdateRange(explocionTbList); 
                            }

                            await _context.SaveChangesAsync();
                            var explocionTbSapTbList = await _context.Tbl_ExplocionSap
                            .Where(e => e.CotizacionGrupo == P_grupoCotizacion && e.Tipo == "Salida")
                            .ToListAsync();

                            if (explocionTbSapTbList.Any())
                            {
                                foreach (var item in explocionTbSapTbList)
                                {
                                    item.CodigoSalidaSap = docEntry.ToString();
                                    item.CodigoEntradaSap = "";
                                    item.IdUsuarioCrea = Convert.ToInt32(idusuario);
                                }

                                _context.Tbl_ExplocionSap.UpdateRange(explocionTbSapTbList); 
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();

                            var jsonresponse = new
                            {
                                Respuesta = "OPERACION REALIZADA CORRECTAMENTE",
                                idOrden = 0
                            };
                            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                         }
                        else
                        {
                            await transaction.RollbackAsync();
                            // Si no encontramos el DocEntry
                            return new GeneralResponse<Object>(
                                HttpStatusCode.BadRequest,
                                new { Respuesta = "Error: DocEntry no encontrado.", Detalle = "Error: DocEntry (Codigo Salida) no encontrado." });
                        } 
                    }
                 }



            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> EnviarEntradaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var _TbCotizacion = await _context.Tbl_OrdenProduccion
                    .FirstOrDefaultAsync(o => o.NumeroCotizacion == P_NumeroCotizacion);
                 
                var datEntrada = await _context.TBL_DetalleOrdenProduccion.Where(d => d.CotizacionGrupo == P_grupoCotizacion).ToListAsync();

                var explocionSap = await _context.Tbl_ExplocionSap
                .Where(e => e.CotizacionGrupo == P_grupoCotizacion)
                .FirstAsync();
                var listItems = new List<ItemEntrada>();
                var error = "";

                

                // Convertir el string a DateTime
                DateTime fechaCot = DateTime.ParseExact(_TbCotizacion.FechaCotizacion, "yyyyMMdd", null);
                DateTime fechaVen = DateTime.ParseExact(_TbCotizacion.FechaVenta, "yyyyMMdd", null);
                foreach (var item in datEntrada)
                { 

                    var batchNumbers = JsonConvert.DeserializeObject<List<BatchNumbers>>("[]");                   
                    var serialNumbers = JsonConvert.DeserializeObject<List<SerialNumber>>("[]");
                    listItems.Add(new ItemEntrada
                    {
                        ItemCode = item.CodigoProducto.Trim(),
                        ItemDescription = item.NombreProducto?.Trim() ?? "",
                        Quantity = Convert.ToDecimal(item.Cantidad),
                        WarehouseCode = _configuration["ApiSAP:WarehouseCodeEntrada"],
                        AcctCode = _configuration["ApiSAP:AcctcodeEntrada"],// //item.AcctCode,  
                        //FamiliaPT = "PRT", 
                        //SuibFamiliaPT = item.SubFamilia?.Trim() ?? "",
                        //SubFamiliaPT =item.SubFamiliaPT,
                        //BatchNumbers = batchNumbers,
                        //SerialNumbers = serialNumbers,
                        IdSalida = Convert.ToInt32(explocionSap.CodigoSalidaSap),
                        Alto = Convert.ToDecimal(item.Alto),
                        Ancho = Convert.ToDecimal(item.Ancho),
                        IdOrdenVenta = _TbCotizacion.DocEntrySap.ToString()

                    });

                    var cotizacionEntity = new Tbl_ExplocionSap
                    {
                        DocDate = fechaCot.ToString("dd/MM/yyyy"),
                        TaxDate = fechaVen.ToString("dd/MM/yyyy"),
                        Comments = "Entrada",
                        Reference2 = "Referencia2",
                        U_EXX_TIPOOPER = "19",
                        IdSistemaExterno = explocionSap.IdSistemaExterno,
                        IdOrdenVenta = explocionSap.IdOrdenVenta?.ToString(),
                        ItemCode = item.CodigoProducto,
                        ItemDescription=item.NombreProducto,
                        Quantity = item.Cantidad.ToString(),
                        WarehouseCode =  _configuration["ApiSAP:WarehouseCodeEntrada"],
                        AcctCode = _configuration["ApiSAP:AcctcodeEntrada"],// item.AcctCode,
                        CostingCode = null,
                        ProjectCode = null,
                        CostingCode2 = null,
                        CostingCode3 = null,
                        CostingCode4 = null,
                        CostingCode5 = null,
                        IdLineaSistemaE = explocionSap.IdLineaSistemaE,
                        FamiliaPT = "PRT",//item.CodFamilia,
                        SubFamiliaPT = item.SubFamilia,
                        BatchNumbers = "[]",
                        SerialNumbers = "[]",
                        CodigoSalidaSap = "",
                        Tipo = "Entrada",
                        CotizacionGrupo = P_grupoCotizacion,
                        IdUsuarioCrea = Convert.ToInt32(idusuario),
                        FechaCreacion = DateTime.Now, 
                    };

                    _context.Tbl_ExplocionSap.Add(cotizacionEntity);
                }

                var cotizacion = new CotizacionCabeceraEntrada
                {//2024-10-03
                    DocEntry =Convert.ToInt32(explocionSap.CodigoSalidaSap),
                    DocNum = P_NumeroCotizacion + "" + DateTime.Now.ToString("ddHHmmss"),
                    DocDate =  fechaCot.ToString("yyyy-MM-dd"),
                    TaxDate =  fechaVen.ToString("yyyy-MM-dd"),
                    Comments = "Entrada",
                    Reference2 = "Ref2",
                    U_EXX_TIPOOPER = "19",
                    IdSistemaExterno = _TbCotizacion.Id.ToString(),
                    DocumentLines = listItems
                };
                
                var token = await LoginAsync();
                var inventoryUrl = _configuration["ApiSAP:BaseUrl"] + "/api/InventoryGenEntry";
                var options = new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var jsonInventoryData = System.Text.Json.JsonSerializer.Serialize(cotizacion, options);
                var content = new StringContent(jsonInventoryData, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsync(inventoryUrl, content);

                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta del servidor: " + responseString);

                if (!response.IsSuccessStatusCode)
                {
                    await transaction.RollbackAsync();
                    return new GeneralResponse<Object>(
                        HttpStatusCode.BadRequest,
                        new { Respuesta = "Error al procesar la carga de datos.", Detalle = responseString });
                }
                else
                {
                    // Usamos JsonDocument para parsear la respuesta JSON
                    using (JsonDocument doc = JsonDocument.Parse(responseString))
                    {
                        JsonElement root = doc.RootElement;

                        // Verificar si "DocEntry" está presente y obtener su valor
                        if (root.TryGetProperty("DocEntry", out JsonElement docEntryElement))
                        {
                            int docEntry =   docEntryElement.GetInt32();
                            Console.WriteLine("DocEntry: " + docEntry);                            

                            var explocionTb = await _context.Tbl_Explocion
                                .FirstAsync(e => e.NumeroCotizacion == P_NumeroCotizacion && e.CotizacionGrupo == P_grupoCotizacion);
                            explocionTb.CodigoEntradaSap = docEntry.ToString();
                            explocionTb.FechaEntradaSap = DateTime.Now;
                            explocionTb.IdUsuarioModifica = Convert.ToInt32(idusuario);
                            _context.Tbl_Explocion.Update(explocionTb);

                            await _context.SaveChangesAsync();
                            var explocionTbList = await _context.Tbl_Explocion
                            .Where(e => e.NumeroCotizacion == P_NumeroCotizacion && e.CotizacionGrupo == P_grupoCotizacion)
                            .ToListAsync();
                            if (explocionTbList.Any())
                            {
                                foreach (var item in explocionTbList)
                                {
                                    item.CodigoEntradaSap = docEntry.ToString();
                                    item.FechaEntradaSap = DateTime.Now;
                                    item.IdUsuarioModifica = Convert.ToInt32(idusuario);
                                }
                                _context.Tbl_Explocion.UpdateRange(explocionTbList); 
                            }
                            var explocionTbSapTbList = await _context.Tbl_ExplocionSap
                            .Where(e => e.CotizacionGrupo == P_grupoCotizacion && e.Tipo == "Entrada")
                            .ToListAsync();

                            if (explocionTbSapTbList.Any())
                            {
                                foreach (var item in explocionTbSapTbList)
                                {
                                    item.CodigoEntradaSap = docEntry.ToString();
                                    item.CodigoSalidaSap = "";  
                                }
                                _context.Tbl_ExplocionSap.UpdateRange(explocionTbSapTbList); 
                            }

                            await _context.SaveChangesAsync();
                            await transaction.CommitAsync();
                            var jsonresponse = new
                            {
                                Respuesta = "OPERACION REALIZADA CORRECTAMENTE",
                                idOrden = 0
                            };
                            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                            // Si no encontramos el DocEntry
                            return new GeneralResponse<Object>(
                                HttpStatusCode.BadRequest,
                                new { Respuesta = "Error: DocEntry no encontrado.", Detalle = "Error: DocEntry (Codigo Entrada) no encontrado." });
                        }
                    }
                }

                 

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<Object>> GuardarCodigoSalida(string P_NumeroCotizacion, string P_grupoCotizacion, string codigoSalida)
        {
            using var transaction = await this._context.Database.BeginTransactionAsync();
            try
            {
                // Obtener el grupo correspondiente
                var grupo = await this._context.Tbl_DetalleOpGrupo
                                               .Where(g => g.CotizacionGrupo == P_grupoCotizacion && g.NumeroCotizacion == P_NumeroCotizacion)
                                               .FirstOrDefaultAsync();

                if (grupo == null)
                {
                    throw new Exception("No se encontró el grupo correspondiente para los parámetros proporcionados.");
                }

                // Obtener la lista de explosiones asociadas
                var explocion = await this._context.Tbl_Explocion
                                                   .Where(e => e.CotizacionGrupo == P_grupoCotizacion && e.NumeroCotizacion == P_NumeroCotizacion)
                                                   .ToListAsync();

                if (!explocion.Any())
                {
                    throw new Exception("No se encontraron registros en Tbl_Explocion para los parámetros proporcionados.");
                }

                // Actualizar el código de salida en el grupo
                grupo.CodigoSalidaSap = codigoSalida;
                this._context.Tbl_DetalleOpGrupo.Update(grupo);

                // Actualizar el código de salida en cada registro de explosión
                explocion.ForEach(e => e.CodigoSalidaSap = codigoSalida);
                this._context.Tbl_Explocion.UpdateRange(explocion);

                // Guardar cambios
                await this._context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                // Respuesta exitosa
                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idOrden = 1,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                // Revertir cambios en caso de error
                await transaction.RollbackAsync();
                this._logger.LogError($"Error en GuardarCodigoSalida: {JsonConvert.SerializeObject(ex)}");

                // Respuesta de error
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };

                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }



       

        #region ESTRUCTURA SALIDA SAP
        public class CotizacionCabecera
        { public string DocNum { get; set; }
            public string DocDate { get; set; }
            public string TaxDate { get; set; }
            public string Comments { get; set; }
            public string Reference2 { get; set; }
            public string U_EXX_TIPOOPER { get; set; }
            public string IdSistemaExterno { get; set; }
            public List<Item>? DocumentLines { get; set; }  // Lista de Items
        }
        
        public class Item
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal Quantity { get; set; }
            public string WarehouseCode { get; set; }
            public string AcctCode { get; set; }
            public string CostingCode { get; set; }
            public string ProjectCode { get; set; }
            public string CostingCode2 { get; set; }
            public string CostingCode3 { get; set; }
            public string CostingCode4 { get; set; }
            public string CostingCode5 { get; set; }
            public string IdSistemaExterno { get; set; }
            public string IdLineaSistemaE { get; set; }
            public string IdOrdenVenta { get; set; }
            public string FamiliaPT { get; set; }
            public string SubFamiliaPT { get; set; }
            public List<BatchNumbers> BatchNumbers { get; set; }  // Lista de BatchNumbers
            public List<SerialNumber> SerialNumbers { get; set; }  // Lista de SerialNumbers
        }

        public class CotizacionCabeceraEntrada
        {
            public int DocEntry { get; set; }
            public string DocNum { get; set; }
            public string DocDate { get; set; }
            public string TaxDate { get; set; }
            public string Comments { get; set; }
            public string Reference2 { get; set; }
            public string U_EXX_TIPOOPER { get; set; }
            public string IdSistemaExterno { get; set; }
            public List<ItemEntrada>? DocumentLines { get; set; }  // Lista de Items
        }
        public class ItemEntrada
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public decimal Quantity { get; set; }
            public string WarehouseCode { get; set; }
            public string AcctCode { get; set; }
            public string CostingCode { get; set; }
            public string ProjectCode { get; set; }
            public string CostingCode2 { get; set; }
            public string CostingCode3 { get; set; }
            public string CostingCode4 { get; set; }
            public string CostingCode5 { get; set; }
            public string IdSistemaExterno { get; set; }
            public string IdLineaSistemaE { get; set; }
            public string IdOrdenVenta { get; set; }
            public string FamiliaPT { get; set; }
            public string SuibFamiliaPT { get; set; }
            public int IdSalida {  get; set; }

            public decimal Ancho { get; set; }
            public decimal Alto { get; set; }
            public List<BatchNumbers> BatchNumbers { get; set; }  // Lista de BatchNumbers
            public List<SerialNumber> SerialNumbers { get; set; }  // Lista de SerialNumbers
        }
        public class BatchNumbers
        {
            public string BatchNumber { get; set; }  // Ejemplo de campo en BatchNumbers
            public decimal Quantity { get; set; }
        }

        public class SerialNumber
        {
            public int SerialNumberCode { get; set; }  // Ejemplo de campo en SerialNumbers
            public decimal Quantity { get; set; }
        }
        #endregion



        #region ROLLER SHADE FORMULACION

        public async Task<GeneralResponse<dynamic>> ListarFormulacionRollerShade(string numCotizacion,string grupoCotizacion,string tipoProducto,string accionamiento) 
        {
            try
            {
                string procedure = "";
                var productosOtros = new List<string> {
                "PRTRM00000016", "PRTRM00000001", "PRTRH00000001", "PRTRF00000001",
                "PRTLU00000001", "PRTLU00000002", "PRTLU00000003"
                }; 

                switch (tipoProducto)
                {
                    case "PRTRSMan": //MANUAL
                        procedure = "SP_ListarFormulacionRollerShade";
                        break;

                    case "PRTRSMot": //MOTORIZADO
                        procedure = "SP_ListarFormulacionRollerShadeMot";
                        break;

                    case "PRTRZ": //MOTORIZADO Y MANUAL
                        procedure = "SP_ListarFormulacionRollerZebra";
                        break;

                    case "PRTCV": //CORTINA VERTICAL
                        procedure = "SP_ListarFormulacionOtros";
                        break;

                    default:
                        if (productosOtros.Contains(tipoProducto))
                        {
                            procedure = "SP_ListarFormulacionOtros";
                        }else if (tipoProducto.ToUpper().Contains("PRTCV"))
                        {
                            procedure = "SP_ListarFormulacionOtros";
                        }
                        break;
                }
                if (string.IsNullOrEmpty(procedure))
                    return new GeneralResponse<object>(HttpStatusCode.BadRequest, "Tipo de producto no reconocido");

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(procedure, cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (tipoProducto == "PRTRSMan" || tipoProducto == "PRTRSMot" || tipoProducto == "PRTRZ")
                        {
                            cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion", numCotizacion));
                            cmd.Parameters.Add(new SqlParameter("@grupoCotizacion", grupoCotizacion));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@TipoProducto", tipoProducto));
                            cmd.Parameters.Add(new SqlParameter("@grupoCotizacion", grupoCotizacion));
                        }

                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }
                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return new GeneralResponse<object>(HttpStatusCode.InternalServerError, ex.Message); 
                //throw new Exception(ex.Message);
            }
        } 
        public async Task<GeneralResponse<Object>> GuardarFormulacionRollerShade(List<MonitoreoFormulacionRollerRequest> request)
        {
            using var transaction = await this._context.Database.BeginTransactionAsync();
            try
            {

                foreach (var item in request)
                {
                    string Adicional = "";
                    if (item.Agregado!=null && item.Agregado == "true")
                    {
                        Adicional = "SI";
                        item.CodFamilia = "PRT";
                    } 
                    if (string.IsNullOrEmpty(item.CodFamilia))
                    {
                        item.CodFamilia = "PRT";
                    }
                    item.Familia = "PRT";
                    
                    var nuevaFila = new Tbl_Explocion()
                    {
                        NumeroCotizacion = item.NumeroCotizacion,
                        CotizacionGrupo = item.Grupo,
                        Nombre_Producto = item.Producto,
                        Codigo_Producto =item.TipoProducto, //PRTRS , PRTRZ, PRTRM00000016 , ETC
                        Descrip_Componente = item.Descrip_Componente,
                        Cod_Componente = item.Codigo_Componente,
                        Descripcion = item.Componente,
                        Color = item.Color,
                        Unidad = item.UnidadMedida,
                        Cantidad = item.CantidadUtilizada,
                        Merma = item.Merma,
                        Origen = "Explocion",
                        IdUsuarioCrea = Convert.ToInt32(item.Usuario),
                        FechaCreacion = DateTime.Now,
                        Adicional = Adicional,
                        //
                        Familia = item.Familia,
                        WhsCode = item.WhsCode,
                        SubFamilia=item.SubFamilia,
                        Serie = item.Serie,
                        Lote = item.Lote,
                        Alto = item.Alto,
                        Ancho = item.Ancho,
                        CodFamilia=item.CodFamilia
                    };
                    this._context.Tbl_Explocion.Add(nuevaFila);
                }
                // Actualizar el estado de grupo dentro de la transacción
                var grupo = await this._context.Tbl_DetalleOpGrupo
                                             .Where(g => g.CotizacionGrupo == request[0].Grupo)
                                             .FirstOrDefaultAsync();
                if (grupo != null)
                {
                    grupo.IdEstado = 6; // Estado TERMINADO
                    this._context.Tbl_DetalleOpGrupo.Update(grupo);
                }
                else
                {
                    throw new Exception("No se encontró el grupo especificado.");
                }
                await this._context.SaveChangesAsync();
                await transaction.CommitAsync();
                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idOrden = 1,
                };
                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Revertir todos los cambios
                this._logger.LogError($"Error en GuardarExplocion: {JsonConvert.SerializeObject(ex)}");

                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idOrden = 0
                };

                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        #endregion
    }
}
