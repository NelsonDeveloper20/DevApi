using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Response;

namespace Api_Dc.Domain.Contracts
{
    public interface IProducto
    {
        
        Task<IEnumerable<TBL_DetalleOrdenProduccion>> GetAllAsync(string NumeroCotizacion);
        Task<GeneralResponse<Object>> AgregarOrden(AgregarOrdenProduccionRequest _orden, IFormFile? Archivo); 
    }
}
