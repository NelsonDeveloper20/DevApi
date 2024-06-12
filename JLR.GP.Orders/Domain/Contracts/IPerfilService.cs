using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IPerfilService
    {

        Task<GeneralResponse<Object>> AgregarPerfil(int Id, string Nombre, string Descripcion);
        Task<GeneralResponse<Object>> EliminarPerfil(int Id);
        Task<IEnumerable<Tbl_Rol>> ListarPerfil();
        Task<GeneralResponse<dynamic>> ListarModulosPorRol(int idRol);

        Task<GeneralResponse<Object>> AgregarModeloRol(int idRol, List<AgregarModuloRolRequest> modulos);
        Task<GeneralResponse<dynamic>> ListarModulosPorUsuario(int idUsuario);


    }
}
