using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IMonitoreo
    {

        //#region 

        Task<GeneralResponse<Object>> ModificarMerma(List<ModificarMermaRequest> request, string idusuario);
        Task<IEnumerable<object>> ListarMermaAmodificar(string grupo);
        Task<GeneralResponse<Object>> RevertirProceso(RevertirRequest _request);

        //#endregion
        Task<GeneralResponse<dynamic>> ListarExplocion(string grupoCotizacion, string fechaInicio, string fechaFin); //OK
        Task<GeneralResponse<dynamic>> ListarMonitoreoSapSalidaEntrada(string grupoCotizacion, string fechaInicio, string fechaFin); //OK
        Task<GeneralResponse<dynamic>> ListarMonitoreoSapSalidaEntradaRevertido(string grupoCotizacion, string fechaInicio, string fechaFin);
        Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id); //OK OK 
        Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request);
        Task<GeneralResponse<Object>> GuardarExplocion(List<ExplocionComponentesRequest> request);
        Task<GeneralResponse<Object>> GuardarFormulacionRollerShade(List<MonitoreoFormulacionRollerRequest> request, string tipo);

        

        Task<GeneralResponse<Object>> CargaExcelExplocion(ExplocionComCargaRequest request);
        Task<GeneralResponse<dynamic>> ListarReporteExplocion(string grupoCotizacion, string fechaInicio, string fechaFin);
        Task<GeneralResponse<dynamic>> ListarMantenimientoExplocion(string grupoCotizacion);
        Task<IEnumerable<Tbl_Componentes>> ListarComponentesPorCodigosProducto(string codigosProducto,string grupo);
        Task<GeneralResponse<Object>> GuardarExplocionMantenimiento(List<ExplocionComponentesMantRequest> request);
        Task<GeneralResponse<Object>> EnviarEntradaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario);
        Task<GeneralResponse<Object>> EnviarSalidaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario);
        Task<GeneralResponse<Object>> EnviarSalidaMermaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario);
        Task<GeneralResponse<Object>> GuardarCodigoSalida(string P_NumeroCotizacion, string P_grupoCotizacion, string codigoSalida);

        Task<GeneralResponse<dynamic>> ListarFormulacionRollerShade(string numCotizacion, string grupoCotizacion, string tipoProducto,string accionamiento);

        Task<GeneralResponse<Object>> JSONEnviarSalidaSap(string P_NumeroCotizacion, string P_grupoCotizacion);
        Task<GeneralResponse<Object>> JSONEnviarEntradaSap(string P_NumeroCotizacion, string P_grupoCotizacion);

        Task<GeneralResponse<Object>> ModificarEnviarEntradaSap(dynamic request);
        Task<GeneralResponse<Object>> ModificarEnviarSalidaSap(dynamic request);
    }
}
