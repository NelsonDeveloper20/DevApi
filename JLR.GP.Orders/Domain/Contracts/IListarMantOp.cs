using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IMantenimientoOp
    {
        //Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo();
        Task<GeneralResponse<dynamic>> ListarOp(string numCotizacion);
        Task<GeneralResponse<Object>> EliminarMantenimientooOP(string id);
    }
}
