using Api_Dc.Domain.Models;
using Api_Dc.Domain.Shared.Constants;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class LoginService: ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<LoginService> _logger;

        public LoginService(
            IConfiguration configuration,
            DBContext context,
            ILogger<LoginService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }
         
        public async Task<IEnumerable<dynamic>> ValidarLogin(string usuario,string contrasena)
        {
            var contra = Encriptacion.EncryptPassword(contrasena.Trim().ToLower());
            var result = await (from u in _context.Tbl_Usuario
                                join r in _context.Tbl_Rol on u.IdRol equals r.Id
                                where u.Usuario.Trim().ToLower() == usuario.Trim().ToLower() &&
                                      u.Clave.Trim().ToLower() == contra
                                select new
                                {
                                    Id = u.Id,
                                    IdTipoUsuario = u.IdTipoUsuario,
                                    Nombre = u.Nombre,
                                    Apellido = u.Apellido,
                                    Dni = u.Dni,
                                    Correo = u.Correo,
                                    Usuario = u.Usuario,
                                    Perfil = r.Nombre,
                                    idS=r.Id
                                }).ToListAsync();

            return result.Cast<dynamic>();
        }
         
    }
}
