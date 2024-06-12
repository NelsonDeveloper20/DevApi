using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Response;

namespace Api_Dc.Domain.Contracts
{
    public interface IDetalleOrdenProduccionService
    {
        Task<GeneralResponse<object>> AgregarProducto(dynamic formData, dynamic escuadra,string tipo);
        Task<IEnumerable<TBL_DetalleOrdenProduccion>> GetAllAsync();
    }
}
