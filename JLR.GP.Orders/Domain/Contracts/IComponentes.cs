using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IComponentes
    {
        Task<GeneralResponse<Object>> AgregarComponentes(AgregarComponentesRequest componente);
        Task<IEnumerable<Tbl_Componentes>> listarComponetes();
        Task<GeneralResponse<Object>> EliminarComponente(int id);
        Task<IEnumerable<Tbl_ComponteProducto>> listarTelaRielTubo(string tipo, string codigoProducto, string nombreProducto);
        Task<IEnumerable<Tbl_AccesorioProducto>> listarAccesorio(string codigoProducto);
    }
}
