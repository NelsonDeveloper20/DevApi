using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IListasService
    {
        Task<IEnumerable<ListasResponse>> GetListar(string tabla);
        Task<GeneralResponse<int>> Agregar(AgregarListasRequest items);
        //Task<IEnumerable<Dim_Empresa>> GetDim_Empresa(); 
    }
}
