using Api_Dc.Application.Models.Request;
using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface ISupervision
    {
        Task<GeneralResponse<Object>> Aprobacion(SuperAprobacionRequest item);
        Task<GeneralResponse<dynamic>> ListarOp(string numCotizacion); 
    }
}
