using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IMonitoreo
    { 
        Task<GeneralResponse<dynamic>> ListarExplocion(string grupoCotizacion, string fechaInicio, string fechaFin); //OK
        Task<GeneralResponse<dynamic>> ListarMonitoreoSapSalidaEntrada(string grupoCotizacion, string fechaInicio, string fechaFin); //OK
        Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id); //OK OK 
        Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request);
        Task<GeneralResponse<Object>> GuardarExplocion(List<ExplocionComponentesRequest> request);
        Task<GeneralResponse<Object>> GuardarFormulacionRollerShade(List<MonitoreoFormulacionRollerRequest> request);

        

        Task<GeneralResponse<Object>> CargaExcelExplocion(ExplocionComCargaRequest request);
        Task<GeneralResponse<dynamic>> ListarReporteExplocion(string grupoCotizacion, string fechaInicio, string fechaFin);
        Task<GeneralResponse<dynamic>> ListarMantenimientoExplocion(string grupoCotizacion);
        Task<IEnumerable<Tbl_Componentes>> ListarComponentesPorCodigosProducto(string codigosProducto,string grupo);
        Task<GeneralResponse<Object>> GuardarExplocionMantenimiento(List<ExplocionComponentesMantRequest> request);
        Task<GeneralResponse<Object>> EnviarEntradaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario);
        Task<GeneralResponse<Object>> EnviarSalidaSap(string P_NumeroCotizacion, string P_grupoCotizacion, string idusuario); 
        Task<GeneralResponse<Object>> GuardarCodigoSalida(string P_NumeroCotizacion, string P_grupoCotizacion, string codigoSalida);

        Task<GeneralResponse<dynamic>> ListarFormulacionRollerShade(string numCotizacion, string grupoCotizacion, string tipoProducto,string accionamiento);
    }
}
