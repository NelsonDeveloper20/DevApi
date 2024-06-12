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
        Task<GeneralResponse<dynamic>> listarComponentesProductoPorGrupo(string grupoCotizacion, string id); //OK OK 
        Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request);
    }
}
