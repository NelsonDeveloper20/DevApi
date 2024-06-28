using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IOperacionesContruccion
    {
        //Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo();
        Task<GeneralResponse<dynamic>> ListarOperacionesConstruccion(string NumeroCotizacionGrupo);
        Task<GeneralResponse<object>> ValidarEstacion(string paso, string codigoEstacion, string idusuario, string grupo);
        Task<GeneralResponse<dynamic>> ListarProductoXEstacionGrupo(string grupoCotizacion, string estacion);
        Task<GeneralResponse<dynamic>> ListarAvanceEstacion(string grupoCotizacion);
        Task<GeneralResponse<Object>> InsertarEstacionProducto(EstacionProductoRequest _request);
    }
}
