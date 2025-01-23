using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IDetalleOpGrupo
    {
        //Task<IEnumerable<Tbl_DetalleOpGrupo>> ListarDetalleOpGrupo();
        Task<GeneralResponse<dynamic>> ListarDetalleOpGrupo(OPGrupoRequest Request);

        Task<GeneralResponse<Object>> EnviarEstado(string destino, List<GruposIDRequest> request);
        Task<GeneralResponse<dynamic>> ListarFiltros();
        Task<GeneralResponse<dynamic>> ListatarProductosDetallePorGrupo(string grupo);
        Task<GeneralResponse<Object>> AplicarCentral(string cotizacionGrupo, int id, string valor);
        Task<GeneralResponse<Object>> ReinicioGrupo(int id);
        Task<GeneralResponse<Object>> ModificarTurnoFechaGrupo(string grupo, string turno, string fecha);


    }
}
