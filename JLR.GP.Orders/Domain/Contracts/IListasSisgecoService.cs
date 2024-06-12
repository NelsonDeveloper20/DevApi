using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IListasSisgecoService
    {
        Task<IEnumerable<ListasSisgecoResponse>> GetListar(string tipo, string subfamilia);
        Task<GeneralResponse<dynamic>> ListarComponentesSsigeco();
    }
}
