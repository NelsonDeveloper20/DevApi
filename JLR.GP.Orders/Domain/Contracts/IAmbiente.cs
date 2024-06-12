using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace Api_Dc.Domain.Contracts
{
    public interface IAmbiente
    {
        Task<IEnumerable<Tbl_Ambiente>> listarAmbiente(string numeroCotizacion);
        Task<GeneralResponse<Object>> GuararAmbiente(string cotizacion, string indice, string ambiente, string cantidad);
        Task<GeneralResponse<Object>> EliminarAmbiente(string id);
    }
}
