using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface ILineaProd
    {
        Task<GeneralResponse<dynamic>> ListarLinaProduccion(string fecha);
        Task<GeneralResponse<dynamic>> DetalleListarLinaProduccion(string turno, string dia);
    }
}
