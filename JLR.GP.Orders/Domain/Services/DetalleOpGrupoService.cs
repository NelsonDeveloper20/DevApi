using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class DetalleOpGrupoService: IDetalleOpGrupo
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public DetalleOpGrupoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;

            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configurations = builder.Build();
            CnDc_Blinds = configurations["ConnectionString:DefaultConnection"];
        }
         
        //::::::::::::::::::::::::::::::::
        /*public async Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo()
        {
            var _user = this._context.Tbl_DetalleOpGrupo.ToList();
            return _user;
        }*/
        public async Task<GeneralResponse<dynamic>> ListarDetalleOpGrupo(OPGrupoRequest Request)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetOrdenProduccionDetalleGrupoVenta", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Vendedor", Request.Vendedor));
                        cmd.Parameters.Add(new SqlParameter("@NumeroCotizacion ", Request.NumeroCotizacion));
                        cmd.Parameters.Add(new SqlParameter("@Cliente", Request.Cliente));
                        cmd.Parameters.Add(new SqlParameter("@FechaInicio", Request.FechaInicio));
                        cmd.Parameters.Add(new SqlParameter("@FechaFin", Request.FechaFin));
                        cmd.Parameters.Add(new SqlParameter("@CodigoSisgeco", Request.CodigoSisgeco));
                        cmd.Parameters.Add(new SqlParameter("@RucCliente", Request.RucCliente));
                        cmd.Parameters.Add(new SqlParameter("@IdProyecto", Request.IdProyecto));
                        cmd.Parameters.Add(new SqlParameter("@IdTipoCliente", Request.IdTipoCliente));
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

        public async Task<GeneralResponse<Object>> EnviarEstado(string destino, List<GruposIDRequest> request)

        {
            try
            {
                var idModulo = 1;
                foreach (var item in request)
                {
                    var _Grupo = this._context.Tbl_DetalleOpGrupo.Find(item.Id);
                    switch(destino)
                    {
                        case "Venta":
                            _Grupo.IdEstado = 2;//Pendiente Venta
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Operaciones":
                            _Grupo.IdEstado = 3; //Pendiente Operaciones
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Construccion":
                            _Grupo.IdEstado = 4;//Pendiente Construccion
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Construccion Terminada":
                            _Grupo.IdEstado = 5;//Construccion Terminada
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                        case "Terminado":
                            _Grupo.IdEstado = 6;//Terminado
                            this._context.Tbl_DetalleOpGrupo.Update(_Grupo);
                            break;
                    }                    
                }
                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idModulo = idModulo,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idModulo = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListarFiltros()
        {
            try
            {
                var vendedores = await _context.Tbl_OrdenProduccion
            .Select(d => d.NombreVendedor)
            .Distinct()
            .ToListAsync();


                var cotizaciones = await _context.Tbl_DetalleOpGrupo
                    .Select(d => d.NumeroCotizacion)
                    .Distinct()
                    .ToListAsync();

                var proyectos = await _context.Tbl_Proyecto.ToListAsync();

                var tiposClientes = await _context.Tbl_TipoCliente.ToListAsync();

                var rucs = await _context.Tbl_OrdenProduccion
                    .Select(o => o.RucCliente)
                    .Distinct()
                    .ToListAsync();
                var grupos = await _context.Tbl_DetalleOpGrupo
          .Select(d => d.CotizacionGrupo)
          .Distinct()
          .ToListAsync();
                var result = new
                {
                    vendedores,
                    cotizaciones,
                    proyectos,
                    tiposClientes,
                    rucs,
                    grupos

                };

                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListatarProductosDetallePorGrupo(string grupo)
        {
            try
            {
                //var _productos=this._context.TBL_DetalleOrdenProduccion.Where(p=> p.CotizacionGrupo==grupo).ToList();
                var _productos = await (from dop in _context.TBL_DetalleOrdenProduccion
                                        where dop.CotizacionGrupo == grupo
                                        orderby dop.Linea
                                        select new 
                                        {
                                            Id = dop.Id,
                                            NumeroCotizacion = dop.NumeroCotizacion,
                                            CodigoSisgeco = dop.CodigoSisgeco,
                                            CodigoProducto = dop.CodigoProducto,
                                            Linea = dop.Linea,
                                            NombreProducto = dop.NombreProducto,
                                            UnidadMedida = dop.UnidadMedida,
                                            Cantidad = dop.Cantidad,
                                            Alto = dop.Alto,
                                            Ancho = dop.Ancho,
                                            IndiceAgrupacion = dop.IndiceAgrupacion,
                                            IndexDetalle = dop.IndexDetalle,
                                            Pase = "",
                                            Existe = "",
                                            Familia = dop.Familia,
                                            SubFamilia = dop.SubFamilia,
                                            Precio = dop.Precio,
                                            PrecioInc = dop.PrecioInc,
                                            Igv = dop.Igv,
                                            Lote = dop.Lote,
                                            FechaProduccion = dop.FechaProduccion != null ? dop.FechaProduccion.Value.ToString("dd/MM/yyyy H:mm:ss") : null,
                                            
                                            FechaEntrega = dop.FechaEntrega != null ? dop.FechaEntrega.Value.ToString("dd/MM/yyyy H:mm:ss") : null,


                                            Nota = dop.Nota,
                                            Color = dop.Color,
                                            IdTbl_Ambiente = dop.IdTbl_Ambiente,
                                            Ambiente = dop.Ambiente,
                                            Turno = dop.Turno,
                                            SoporteCentral = dop.SoporteCentral,
                                            TipoSoporteCentral = dop.TipoSoporteCentral,
                                            Caida = dop.Caida,
                                            Accionamiento = dop.Accionamiento,
                                            CodigoTubo = dop.CodigoTubo,
                                            NombreTubo = dop.NombreTubo,
                                            Mando = dop.Mando,
                                            TipoMecanismo = dop.TipoMecanismo,
                                            ModeloMecanismo = dop.ModeloMecanismo,
                                            TipoCadena = dop.TipoCadena,
                                            CodigoCadena = dop.CodigoCadena,
                                            Cadena = dop.Cadena,
                                            TipoRiel = dop.TipoRiel,
                                            TipoInstalacion = dop.TipoInstalacion,
                                            CodigoRiel = dop.CodigoRiel,
                                            Riel = dop.Riel,
                                            TipoCassete = dop.TipoCassete,
                                            Lamina = dop.Lamina,
                                            Apertura = dop.Apertura,
                                            ViaRecogida = dop.ViaRecogida,
                                            TipoSuperior = dop.TipoSuperior,
                                            CodigoBaston = dop.CodigoBaston,
                                            Baston = dop.Baston,
                                            NumeroVias = dop.NumeroVias,
                                            TipoCadenaInferior = dop.TipoCadenaInferior,
                                            MandoCordon = dop.MandoCordon,
                                            MandoBaston = dop.MandoBaston,
                                            CodigoBastonVarrilla = dop.CodigoBastonVarrilla,
                                            BastonVarrilla = dop.BastonVarrilla,
                                            Cabezal = dop.Cabezal,
                                            CodigoCordon = dop.CodigoCordon,
                                            Cordon = dop.Cordon,
                                            CodigoCordonTipo2 = dop.CodigoCordonTipo2,
                                            CordonTipo2 = dop.CordonTipo2,
                                            Cruzeta = dop.Cruzeta,
                                            Dispositivo = dop.Dispositivo,
                                            CodigoControl = dop.CodigoControl,
                                            Control = dop.Control,
                                            CodigoSwitch = dop.CodigoSwitch,
                                            Switch = dop.Switch,
                                            LlevaBaston = dop.LlevaBaston,
                                            MandoAdaptador = dop.MandoAdaptador,
                                            CodigoMotor = dop.CodigoMotor,
                                            Motor = dop.Motor,
                                            CodigoTela = dop.CodigoTela,
                                            Tela = dop.Tela,
                                            Cenefa = dop.Cenefa,
                                            NumeroMotores = dop.NumeroMotores,
                                            Serie = dop.Serie,
                                            AlturaCadena = dop.AlturaCadena,
                                            AlturaCordon = dop.AlturaCordon,
                                            MarcaMotor = dop.MarcaMotor,
                                            IdUsuarioCrea = dop.IdUsuarioCrea,
                                            IdUsuarioModifica = dop.IdUsuarioModifica,
                                            FechaCreacion = dop.FechaCreacion,
                                            FechaModifica = dop.FechaModifica,
                                            IdEstado = dop.IdEstado,
                                            CotizacionGrupo = dop.CotizacionGrupo,
                                            Tipo = dop.Tipo,
                                            EstadoOp = "",
                                            Escuadra = dop.Escuadra
                                        }).ToListAsync();

                return new GeneralResponse<object>(HttpStatusCode.OK, _productos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GeneralResponse<Object>> AplicarCentral(int id, string valor)

        {
            try
            {
                var _grupo=this._context.TBL_DetalleOrdenProduccion.Find(id);
                _grupo.Central = valor.ToUpper();
                _grupo.CentralIndex = valor.ToUpper();
                this._context.TBL_DetalleOrdenProduccion.Update(_grupo);
                this._context.SaveChanges();
                var jsonresponse = new
                {
                    Respuesta = "OK",
                    idModulo = id,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex.Message,
                    idModulo = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }


    }
}
