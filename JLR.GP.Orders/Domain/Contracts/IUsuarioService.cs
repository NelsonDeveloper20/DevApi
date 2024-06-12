using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;

namespace ApiPortal_DataLake.Domain.Contracts
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<IEnumerable<Rol>> GetRolesAsync();
        Task<Usuario?> GetByEmailAsync(string? correo);
        Task<GeneralResponse<Object>> AgregarUsuario(AgregarUsuarioRequest agregarUsuarioRequest);
        //Task<GeneralResponse<int>> AgregarUsuario(AgregarUsuarioRequest agregarUsuarioRequest, string? usuario, List<AgregarUsuarioRolesRequest> dataRoles);
        Task<GeneralResponse<int>> ActualizarUsuario(int usuarioId, ActualizarUsuarioRequest actualizarUsuarioRequest, string? usuario, List<AgregarUsuarioRolesRequest> dataRoles);
        Task<GeneralResponse<int>> ModificarEstadoUsuario(int usuarioId, string estado, string? usuario, string? nombre);
        Task<GeneralResponse<int>> EliminarRol(int id);
        /* Task<IEnumerable<UnidadNegocio>> GetUnidadNegocio();
         Task<IEnumerable<Sede>> GetSede();*/
        Task<IEnumerable<dynamic>> listarusuario();
    }
}
