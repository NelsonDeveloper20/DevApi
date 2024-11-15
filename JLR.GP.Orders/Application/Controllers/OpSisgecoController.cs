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
using Microsoft.Data.SqlClient;
using System.Data;
using Api_Dc.Application.Models.Response;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/OPSisgecoDc")]
    [ApiController]
    public class OpSisgecoController : ControllerBase
    {
        
        private readonly ILogger<OpSisgecoController> _logger;
        private readonly IMapper _mapper;
        private string CnSisgeco;
        private string CnDc_Blinds;
        public OpSisgecoController(
            ILogger<OpSisgecoController> logger,
            IMapper mapper
        )
        { 
            this._logger = logger;
            this._mapper = mapper;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
              CnSisgeco = configuration["ConnectionString:DefaultConnectionSisgeco"];
              CnDc_Blinds = configuration["ConnectionString:DefaultConnection"];
        } 

        [HttpGet]
        public async Task<ActionResult> GeOpSisgeco(string numeroCotizacion)//LISTA TODOS LOS OP DE SIGECO
        {
            try
            {
                // Cadena de conexión
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(CnDc_Blinds))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("Sp_BuscarOPEdicion", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@numero", numeroCotizacion); 
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                    catch (SqlException ex)
                    {
                        // Registrar el error en los logs
                        _logger.LogError($"Error de SQL: {ex.Message}");
                        // Devolver un mensaje de error
                        return StatusCode(500, "Error de SQL al obtener los datos.");
                    }
                }
                // Devolver el DataTable serializado como JSON
                return Ok(dataTable);
            }
            catch (Exception ex)
            {
                // Registrar el error en los logs
                _logger.LogError($"Error al obtener los datos: {ex.Message}");
                // Devolver un mensaje de error
                return StatusCode(500, "Ocurrió un error al obtener los datos.");
            }
        }
        [HttpPost]
        public async Task<ActionResult> ListarProductosSisgecoPorOp(string numeroCotizacion)//Listar OP TANTO DE SISGECO Y DC-BLINDS
        {
            try
            {
                // Consulta Sisgeco
                //var sisgecoData = await ConsultarSisgeco(numeroCotizacion);
                // Consulta DC Blinds
                var dcBlindsData = await ConsultarDCBlinds(numeroCotizacion);
                // Fusionar resultados
                var mergedData = MergeData(null, dcBlindsData);
                return Ok(mergedData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al listar productos: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al procesar la solicitud. "+ex.Message);
            }
        }
        [HttpPut] 
        public async Task<ActionResult<GeneralResponse<int>>> ListarProductoSisgeco(string id)//TRAE ATRIBUTOS DEL PRODUCTOS DESDE SISGECO
        {
            try
            {
                // Crear una nueva conexión
                DataTable dt = new DataTable();
                using (var connection = new SqlConnection(CnSisgeco))
                {
                    await connection.OpenAsync();
                    var query = @"select  top 5  * from  TBL_DetalleOrdenProduccion where Id= @id";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    var reader = await command.ExecuteReaderAsync();
                    dt.Load(reader);

                }
                 
                return Ok(dt);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        [HttpDelete]
        public async Task<ActionResult> ListarProductoBy(string numeroCotizacion, string codigoProucto, string linea)//PRODUCTOS GUARDADO PARA MOSTRAR EN TIPO EDICION DE CAMPOS EN DC-BLINDS 
        {
            try
            {
                // Crear una nueva conexión
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(CnDc_Blinds))
                {
                    try
                    {
                        connection.Open();
                        // Crear un comando para ejecutar el procedimiento almacenado
                        SqlCommand command = new SqlCommand("getDetalleOPCodProdLinea", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetros al comando
                        command.Parameters.AddWithValue("@id", numeroCotizacion);
                        command.Parameters.AddWithValue("@codProd", codigoProucto);
                        command.Parameters.AddWithValue("@linea", linea);
                        // Ejecutar el comando y leer los resultados si los hay                          
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }

                    }
                    catch (SqlException ex)
                    {
                        // Manejar excepciones de SQL Server
                        Console.WriteLine("Error de SQL: " + ex.Message);
                    }
                }
                return Ok(dt);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error guardar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Conflict();
            }
        }
        #region UNION DE BD
        private async Task<List<DetalleOPSigecoDCResponse>> ConsultarSisgeco(string numeroCotizacion)
        {
            using (var connection = new SqlConnection(CnSisgeco))
            {
                await connection.OpenAsync();

                var query = @"
            SELECT
                '' AS Id,
                CC.numero AS NumeroCotizacion,
                CC.numdocref AS CodigoSisgeco,
                CD.codarticulo AS CodigoProducto,
                CD.linea AS Linea,
                CD.[des] AS NombreProducto,
                A.codunidad AS UnidadMedida,
                CD.cantidad AS Cantidad,
                CD.alto AS Alto,
                CD.ancho AS Ancho,
                '' AS IndiceAgrupacion,
                '' AS IndexDetalle,
                CASE
                    WHEN SUBSTRING(cd.codarticulo, 1, 3) != 'PRT' THEN 'PASDIRECCT'
                    ELSE ''
                END AS Pase,
                '' AS Existe
				--ADICIONAL        
				,A.codfamilia Familia  
				,A.codsubfamilia SubFamilia  
				,CD.valor AS Precio,  
				CD.valorinc AS PrecioInc,  
				CD.igv AS Igv,  
				A.lote AS Lote,  
                -- BD DC
                '' as FechaProduccion,
                '' as FechaEntrega,
                '' as Nota,
                '' as Color, 
                '' as IdTbl_Ambiente,
                '' as Ambiente,
                '' as Turno,
                '' as SoporteCentral,
                '' as TipoSoporteCentral,
                '' as Caida,
                '' as Accionamiento,
                '' as CodigoTubo,
                '' as NombreTubo,
                '' as Mando,
                '' as TipoMecanismo,
                '' as ModeloMecanismo,
                '' as TipoCadena,
                '' as CodigoCadena,
                '' as Cadena,
                '' as TipoRiel,
                '' as TipoInstalacion,
                '' as CodigoRiel,
                '' as Riel,
                '' as TipoCassete,
                '' as Lamina,
                '' as Apertura,
                '' as ViaRecogida,
                '' as TipoSuperior,
                '' as CodigoBaston,
                '' as Baston,
                '' as NumeroVias,
                '' as TipoCadenaInferior,
                '' as MandoCordon,
                '' as MandoBaston,
                '' as CodigoBastonVarrilla,
                '' as BastonVarrilla,
                '' as Cabezal,
                '' as CodigoCordon,
                '' as Cordon,
                '' as CodigoCordonTipo2,
                '' as CordonTipo2,
                '' as Cruzeta,
                '' as Dispositivo,
                '' as CodigoControl,
                '' as Control,
                '' as CodigoSwitch,
                '' as Switch,
                '' as LlevaBaston,
                '' as MandoAdaptador,
                '' as CodigoMotor,
                '' as Motor,
                '' as CodigoTela,
                '' as Tela,
                '' as Cenefa,
                '' as NumeroMotores,
                '' as Serie,
                '' as AlturaCadena,
                '' as AlturaCordon,
                '' as MarcaMotor,
                '' as IdUsuarioCrea,
                '' as IdUsuarioModifica,
                '' as FechaCreacion,
                '' as FechaModifica,
                '' as IdEstado,
                '' as CotizacionGrupo,
                '' as Tipo,
                '2' as EstadoOp,
                '' as Escuadra,
                '' as Central
            FROM
                [dbo].[CotizacionDet] AS CD
                INNER JOIN [dbo].[CotizacionCab] AS CC ON CD.numero = CC.numero
                INNER JOIN [dbo].[Articulo] AS A ON CD.codarticulo = A.codigo
            WHERE
                CC.numero = @NumeroCotizacion
            ORDER BY
                CD.linea ASC";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroCotizacion", numeroCotizacion);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    return MapToDetalleOPSigecoDCResponseList(reader);
                }
            }
        }

        private async Task<List<DetalleOPSigecoDCResponse>> ConsultarDCBlinds(string numeroCotizacion)
        {
            using (var connection = new SqlConnection(CnDc_Blinds))
            {
                await connection.OpenAsync();

                var query = @"
            SELECT
                Id,
                NumeroCotizacion,
                CodigoSisgeco,
                CodigoProducto,
                linea,
                NombreProducto,
                UnidadMedida,
                Cantidad,
                Alto,
                Ancho,
                IndiceAgrupacion,
                IndexDetalle,
                '' AS Pase,
                '' AS Existe			
				--ADICIONAL 	
				,Familia  
				,SubFamilia  
				,Precio  
				,PrecioInc 
				,Igv
				,Lote  
                --DC
                ,FechaProduccion,
                FechaEntrega,
                Nota,
                Color, 
                IdTbl_Ambiente,
                Ambiente,
                Turno,
                SoporteCentral,
                TipoSoporteCentral,
                Caida,
                Accionamiento,
                CodigoTubo,
                NombreTubo,
                Mando,
                TipoMecanismo,
                ModeloMecanismo,
                TipoCadena,
                CodigoCadena,
                Cadena,
                TipoRiel,
                TipoInstalacion,
                CodigoRiel,
                Riel,
                TipoCassete,
                Lamina,
                Apertura,
                ViaRecogida,
                TipoSuperior,
                CodigoBaston,
                Baston,
                NumeroVias,
                TipoCadenaInferior,
                MandoCordon,
                MandoBaston,
                CodigoBastonVarrilla,
                BastonVarrilla,
                Cabezal,
                CodigoCordon,
                Cordon,
                CodigoCordonTipo2,
                CordonTipo2,
                Cruzeta,
                Dispositivo,
                CodigoControl,
                Control,
                CodigoSwitch,
                Switch,
                LlevaBaston,
                MandoAdaptador,
                CodigoMotor,
                Motor,
                CodigoTela,
                Tela,
                Cenefa,
                NumeroMotores,
                Serie,
                AlturaCadena,
                AlturaCordon,
                MarcaMotor,
                IdUsuarioCrea,
                IdUsuarioModifica,
                FechaCreacion,
                FechaModifica,
                IdEstado,
                CotizacionGrupo,
                Tipo,                
                (SELECT IdEstado FROM Tbl_DetalleOpGrupo GRD where GRD.CotizacionGrupo=DOP.CotizacionGrupo) AS EstadoOp,
                Escuadra,Central
            FROM
                TBL_DetalleOrdenProduccion DOP
            WHERE
                NumeroCotizacion = @NumeroCotizacion
            ORDER BY
                CotizacionGrupo ASC";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroCotizacion", numeroCotizacion);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    return MapToDetalleOPSigecoDCResponseList(reader);
                }
            }
        }

        private List<DetalleOPSigecoDCResponse> MapToDetalleOPSigecoDCResponseList(SqlDataReader reader)
        {
            var list = new List<DetalleOPSigecoDCResponse>();
            while (reader.Read())
            {
                list.Add(new DetalleOPSigecoDCResponse
                {
                    Id = reader["Id"].ToString(),
                    NumeroCotizacion = reader["NumeroCotizacion"].ToString(),
                    CodigoSisgeco = reader["CodigoSisgeco"].ToString(),
                    CodigoProducto = reader["CodigoProducto"].ToString(),
                    Linea = reader["Linea"].ToString(),
                    NombreProducto = reader["NombreProducto"].ToString(),
                    UnidadMedida = reader["UnidadMedida"].ToString(),
                    Cantidad = reader["Cantidad"].ToString(),
                    Alto = reader["Alto"].ToString(),
                    Ancho = reader["Ancho"].ToString(),
                    IndiceAgrupacion = reader["IndiceAgrupacion"].ToString(),
                    IndexDetalle = reader["IndexDetalle"].ToString(),
                    Pase = reader["Pase"].ToString(),
                    Existe = "SI",
                    Familia = reader["Familia"].ToString(),
                    SubFamilia = reader["SubFamilia"].ToString(),
                    Precio = reader["Precio"].ToString(),
                    PrecioInc = reader["PrecioInc"].ToString(),
                    Igv = reader["Igv"].ToString(),
                    Lote = reader["Lote"].ToString(),

                    // Campos adicionales
                    FechaProduccion = reader["FechaProduccion"].ToString(),
                    FechaEntrega = reader["FechaEntrega"].ToString(),
                    Nota = reader["Nota"].ToString(),
                    Color = reader["Color"].ToString(),
                    IdTbl_Ambiente = reader["IdTbl_Ambiente"].ToString(),
                    Ambiente = reader["Ambiente"].ToString(),
                    Turno = reader["Turno"].ToString(),
                    SoporteCentral = reader["SoporteCentral"].ToString(),
                    TipoSoporteCentral = reader["TipoSoporteCentral"].ToString(),
                    Caida = reader["Caida"].ToString(),
                    Accionamiento = reader["Accionamiento"].ToString(),
                    CodigoTubo = reader["CodigoTubo"].ToString(),
                    NombreTubo = reader["NombreTubo"].ToString(),
                    Mando = reader["Mando"].ToString(),
                    TipoMecanismo = reader["TipoMecanismo"].ToString(),
                    ModeloMecanismo = reader["ModeloMecanismo"].ToString(),
                    TipoCadena = reader["TipoCadena"].ToString(),
                    CodigoCadena = reader["CodigoCadena"].ToString(),
                    Cadena = reader["Cadena"].ToString(),
                    TipoRiel = reader["TipoRiel"].ToString(),
                    TipoInstalacion = reader["TipoInstalacion"].ToString(),
                    CodigoRiel = reader["CodigoRiel"].ToString(),
                    Riel = reader["Riel"].ToString(),
                    TipoCassete = reader["TipoCassete"].ToString(),
                    Lamina = reader["Lamina"].ToString(),
                    Apertura = reader["Apertura"].ToString(),
                    ViaRecogida = reader["ViaRecogida"].ToString(),
                    TipoSuperior = reader["TipoSuperior"].ToString(),
                    CodigoBaston = reader["CodigoBaston"].ToString(),
                    Baston = reader["Baston"].ToString(),
                    NumeroVias = reader["NumeroVias"].ToString(),
                    TipoCadenaInferior = reader["TipoCadenaInferior"].ToString(),
                    MandoCordon = reader["MandoCordon"].ToString(),
                    MandoBaston = reader["MandoBaston"].ToString(),
                    CodigoBastonVarrilla = reader["CodigoBastonVarrilla"].ToString(),
                    BastonVarrilla = reader["BastonVarrilla"].ToString(),
                    Cabezal = reader["Cabezal"].ToString(),
                    CodigoCordon = reader["CodigoCordon"].ToString(),
                    Cordon = reader["Cordon"].ToString(),
                    CodigoCordonTipo2 = reader["CodigoCordonTipo2"].ToString(),
                    CordonTipo2 = reader["CordonTipo2"].ToString(),
                    Cruzeta = reader["Cruzeta"].ToString(),
                    Dispositivo = reader["Dispositivo"].ToString(),
                    CodigoControl = reader["CodigoControl"].ToString(),
                    Control = reader["Control"].ToString(),
                    CodigoSwitch = reader["CodigoSwitch"].ToString(),
                    Switch = reader["Switch"].ToString(),
                    LlevaBaston = reader["LlevaBaston"].ToString(),
                    MandoAdaptador = reader["MandoAdaptador"].ToString(),
                    CodigoMotor = reader["CodigoMotor"].ToString(),
                    Motor = reader["Motor"].ToString(),
                    CodigoTela = reader["CodigoTela"].ToString(),
                    Tela = reader["Tela"].ToString(),
                    Cenefa = reader["Cenefa"].ToString(),
                    NumeroMotores = reader["NumeroMotores"].ToString(),
                    Serie = reader["Serie"].ToString(),
                    AlturaCadena = reader["AlturaCadena"].ToString(),
                    AlturaCordon = reader["AlturaCordon"].ToString(),
                    MarcaMotor = reader["MarcaMotor"].ToString(),
                    IdUsuarioCrea = reader["IdUsuarioCrea"].ToString(),
                    IdUsuarioModifica = reader["IdUsuarioModifica"].ToString(),
                    FechaCreacion = reader["FechaCreacion"].ToString(),
                    FechaModifica = reader["FechaModifica"].ToString(),
                    IdEstado = reader["IdEstado"].ToString(),
                    CotizacionGrupo = reader["CotizacionGrupo"].ToString(),
                    Tipo = reader["Tipo"].ToString(),
                    EstadoOp = reader["EstadoOp"].ToString(),
                    Escuadra = reader["Escuadra"].ToString(),
                    Central = reader["Central"].ToString()
                });
            }
            return list;
        }

        private List<DetalleOPSigecoDCResponse> MergeData(List<DetalleOPSigecoDCResponse> sisgecoData, 
            List<DetalleOPSigecoDCResponse> dcBlindsData)
        {
            /*  var mergedData = new List<DetalleOPSigecoDCResponse>();

              foreach (var sisgecoItem in sisgecoData)
              {
                  var dcBlindsItem = dcBlindsData.FirstOrDefault(d => d.CodigoProducto == sisgecoItem.CodigoProducto && d.Linea == sisgecoItem.Linea);

                  if (dcBlindsItem != null)
                  {
                      sisgecoItem.Existe = "SI";
                      mergedData.Add(dcBlindsItem);
                  }
                  else
                  {
                      sisgecoItem.Existe = "NO";
                      mergedData.Add(sisgecoItem);
                  }
              }*/

            //return mergedData.OrderBy(m => string.IsNullOrEmpty(m.CotizacionGrupo) ? 1 : 0)
            //        .ThenBy(m => m.CotizacionGrupo.EndsWith("-0") ? 1 : 0)
            //        .ThenBy(m => m.CotizacionGrupo)
            //        .ThenBy(m => string.IsNullOrEmpty(m.Id) ? 1 : 0)
            //        .ThenBy(m => m.Id)
            //        .ThenBy(m => m.IndexDetalle)
            //        .ToList();
            foreach (var item in dcBlindsData)
            {
                item.Existe = "SI";
                
            }
            return dcBlindsData.OrderBy(m => string.IsNullOrEmpty(m.CotizacionGrupo) ? 1 : 0)
                    .ThenBy(m => m.CotizacionGrupo.EndsWith("-0") ? 1 : 0)
                    .ThenBy(m => m.CotizacionGrupo)
                    .ThenBy(m => string.IsNullOrEmpty(m.Id) ? 1 : 0)
                    .ThenBy(m => m.Id)
                    .ThenBy(m => m.IndexDetalle)
                    .ToList();
        }
        #endregion
    }
}
