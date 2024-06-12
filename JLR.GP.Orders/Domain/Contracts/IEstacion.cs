using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Response;
using System.Data;

namespace Api_Dc.Domain.Contracts
{
    public interface IEstacion
    {

        Task<IEnumerable<Tbl_DetalleProductoEstacion>> listarEstacion();
        Task<GeneralResponse<Object>> GuararProyecto(string nombreProyecto);

        Task<GeneralResponse<Object>> ModificarProyecto(string id, string nombreProyecto);
    }
}
