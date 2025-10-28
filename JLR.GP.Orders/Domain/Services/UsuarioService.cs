using Api_Dc.Domain.Models;
using Api_Dc.Domain.Shared.Constants;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ApiPortal_DataLake.Domain.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        
        public UsuarioService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }
       
        public async Task<GeneralResponse<Object>> AgregarUsuario(AgregarUsuarioRequest agregarUsuarioRequest)
        {
            try
            {
                var jsonresponse = new
                {
                    Respuesta = "",
                    idUsuario = 0,
                };

                int idUser = 0;
                var usuarioExistente = this._context.Tbl_Usuario.Find(agregarUsuarioRequest.Id);
                var usuarioNuevo = usuarioExistente == null;

                // Verificar duplicados
                var existeUsuario = await this._context.Tbl_Usuario
                    .Where(u => u.Usuario.Trim().ToUpper() == agregarUsuarioRequest.Usuario.Trim().ToUpper() && u.Id != agregarUsuarioRequest.Id)
                    .ToListAsync();
                var existeDni = await this._context.Tbl_Usuario
                    .Where(u => u.Dni.Trim().ToUpper() == agregarUsuarioRequest.Dni.Trim().ToUpper() && u.Id != agregarUsuarioRequest.Id)
                    .ToListAsync();
               var existeCodigoUsuario = await this._context.Tbl_Usuario
                    .Where(u => u.Correo.Trim().ToUpper() == agregarUsuarioRequest.Correo.Trim().ToUpper() && u.Id != agregarUsuarioRequest.Id)
                    .ToListAsync(); 

                if (existeUsuario.Any() || existeDni.Any() || existeCodigoUsuario.Any())
                {
                    jsonresponse = new
                    {
                        Respuesta = existeUsuario.Any() ? "Ya existe el nombre del usuario" :
                                    existeDni.Any() ? "Ya existe Dni" :
                                    "Ya existe Correo",
                        idUsuario = 0,
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                }

                if (usuarioNuevo)
                {
                    var nuevoUsuario = new Tbl_Usuario()
                    {
                        IdUsuarioCreacion = agregarUsuarioRequest.IdUsuarioCreacion,
                        IdTipoUsuario = agregarUsuarioRequest.IdTipoUsuario,
                        Nombre = agregarUsuarioRequest.Nombre,
                        Apellido = agregarUsuarioRequest.Apellido,
                        Dni = agregarUsuarioRequest.Dni,
                        Correo=agregarUsuarioRequest.Correo,
                        IdRol = agregarUsuarioRequest.IdRol,
                        Usuario = agregarUsuarioRequest.Usuario,
                        Clave = Encriptacion.EncryptPassword(agregarUsuarioRequest.Clave.Trim().ToLower()), // Hash the password
                        Ping=agregarUsuarioRequest.Ping.Trim().ToLower(),
                        CodigoUsuario = agregarUsuarioRequest.CodigoUsuario,
                        Estado = 1
                    };
                    await this._context.Tbl_Usuario.AddAsync(nuevoUsuario);
                    this._context.SaveChanges();
                    idUser = nuevoUsuario.Id;
                }
                else
                {
                    usuarioExistente.IdUsuarioModifica = agregarUsuarioRequest.IdUsuarioCreacion;
                    usuarioExistente.IdTipoUsuario = agregarUsuarioRequest.IdTipoUsuario;
                    usuarioExistente.Nombre = agregarUsuarioRequest.Nombre;
                    usuarioExistente.Apellido = agregarUsuarioRequest.Apellido;
                    usuarioExistente.Dni = agregarUsuarioRequest.Dni;
                    usuarioExistente.Correo = agregarUsuarioRequest.Correo;
                    usuarioExistente.IdRol = agregarUsuarioRequest.IdRol;
                    usuarioExistente.Usuario = agregarUsuarioRequest.Usuario;
                    usuarioExistente.Clave = Encriptacion.EncryptPassword(agregarUsuarioRequest.Clave.Trim().ToLower()); // Hash the password
                    usuarioExistente.Ping = agregarUsuarioRequest.Ping.Trim().ToLower();
                    usuarioExistente.CodigoUsuario = agregarUsuarioRequest.CodigoUsuario;
                    usuarioExistente.Estado = agregarUsuarioRequest.Estado;

                    this._context.Tbl_Usuario.Update(usuarioExistente);
                    this._context.SaveChanges();
                    idUser = usuarioExistente.Id;
                }

                jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idUsuario = idUser,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Usuario Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
        public async Task<GeneralResponse<int>> ActualizarUsuario(int usuarioId, ActualizarUsuarioRequest actualizarUsuarioRequest, string? usuario, List<AgregarUsuarioRolesRequest> dataRoles)
        {
            try
            {
                var _usuario = this._context.Usuarios.Find(usuarioId);
                int idUser = 0;
                var users = this._context.Usuarios.Where(u => u.Correo == actualizarUsuarioRequest.Usuario && u.Id!= usuarioId  ).ToList();
                if (users.Count > 0)
                {
                    return new GeneralResponse<int>(HttpStatusCode.Conflict, idUser);
                }
                if (_usuario == null)
                {
                    return new GeneralResponse<int>(HttpStatusCode.Conflict);
                }

                //if (_usuario.Estado != actualizarUsuarioRequest.Estado)
                //{
                _usuario.Estado = actualizarUsuarioRequest.Estado;
                _usuario.Correo = actualizarUsuarioRequest.Correo;
                _usuario.FechaModificacion = DateTime.Now;
                _usuario.UsuarioModificacion = usuario;
                _usuario.Nombre = actualizarUsuarioRequest.Nombre;
                //_usuario.IdUnidadNegocio = int.Parse(actualizarUsuarioRequest.IdUnidadNegocio);
                this._context.Usuarios.Update(_usuario);
                await this._context.SaveChangesAsync();
                //}
                foreach (var row in dataRoles)
                {
                    if (row.idDelete == "0")
                    {
                        var roles = this._context.RolesUsuarios.Find(row.Id);
                        roles.IdUnidadNegocio = row.IdUnidadNegocio;
                        roles.RolId = row.RolId;
                        roles.IdSede = row.IdSede;
                        roles.UsuarioModificacion = usuario;
                        roles.FechaModificacion = DateTime.Now;
                        this._context.RolesUsuarios.Update(roles);
                    }
                    else
                    {
                        var rolesUsuario = new RolUsuario()
                        {
                            UsuarioId = _usuario.Id,
                            RolId = row.RolId,
                            UsuarioCreacion = usuario,
                            FechaCreacion = DateTime.Now,
                            IdUnidadNegocio = row.IdUnidadNegocio,
                            IdSede = row.IdSede
                        };
                        this._context.RolesUsuarios.Add(rolesUsuario);

                    }
                 
                }
                await this._context.SaveChangesAsync();
                /*
            var _rolesUsuario = await this._context.RolesUsuarios.Where(r => r.UsuarioId == usuarioId).ToListAsync();
            if (_rolesUsuario != null && _rolesUsuario.Any())
            {
                this._context.RolesUsuarios.RemoveRange(_rolesUsuario);
                await this._context.SaveChangesAsync();
            }
            if (actualizarUsuarioRequest.Roles != null && actualizarUsuarioRequest.Roles.Any())
            {
                var rolesUsuario = actualizarUsuarioRequest.Roles.Select(rolId => new RolUsuario() { 
                    UsuarioId = usuarioId,
                    RolId = rolId,
                    UsuarioCreacion = usuario,
                    FechaCreacion = DateTime.Now
                });
                await this._context.RolesUsuarios.AddRangeAsync(rolesUsuario);
                await this._context.SaveChangesAsync();
            }*/

                return new GeneralResponse<int>(HttpStatusCode.OK, usuarioId);

            }
            catch (Exception ex)
            {
                this._logger.LogError($"ActualizarUsuario Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<int>(HttpStatusCode.InternalServerError);
            }
        }
        public async Task<GeneralResponse<int>> ModificarEstadoUsuario(int usuarioId, string estado, string? usuario,string? nombre)
        {
            try
            {
                var _usuario = this._context.Usuarios.Find(usuarioId);

                if (_usuario == null)
                {
                    return new GeneralResponse<int>(HttpStatusCode.Conflict);
                }

                if (_usuario.Estado != estado)
                {
                    _usuario.Estado = estado;
                    _usuario.FechaModificacion = DateTime.Now;
                    _usuario.UsuarioModificacion = usuario;
                    _usuario.Nombre = nombre;

                    this._context.Usuarios.Update(_usuario);

                    await this._context.SaveChangesAsync();
                }

                return new GeneralResponse<int>(HttpStatusCode.OK, usuarioId);

            }
            catch (Exception ex)
            {
                this._logger.LogError($"ModificarEstadoUsuario Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<int>(HttpStatusCode.InternalServerError);
            }
        }
        public async Task<GeneralResponse<int>> EliminarRol(int id)
        {
            try
            {
                var _roles = this._context.RolesUsuarios.Find(id);

                if (_roles == null)
                {
                    return new GeneralResponse<int>(HttpStatusCode.Conflict);
                }                 
                this._context.RolesUsuarios.Remove(_roles);
                await this._context.SaveChangesAsync();                

                return new GeneralResponse<int>(HttpStatusCode.OK, _roles.Id);

            }
            catch (Exception ex)
            {
                this._logger.LogError($"ModificarEstadoUsuario Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<int>(HttpStatusCode.InternalServerError);
            }
        }
         
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var users  = this._context.Usuarios
                .Include(u => u.Roles) // Carga la entidad Roles relacionada en Usuario
                .ThenInclude(ru => ru.Rol) // Carga la entidad Rol relacionada en RolUsuario
            .Include(u => u.Roles) // Carga la entidad Roles relacionada en Usuario 
            .ToList(); 
            return users; 
        }
        public async Task<IEnumerable<Tbl_Usuario>> ValidarLogin(string usuario,string contrasena)
        {

            var _user = this._context.Tbl_Usuario.Where(u =>
            u.Usuario.Trim().ToLower() == usuario.Trim().ToLower() &&
            u.Clave.Trim().ToLower() == contrasena.Trim().ToLower()
            ).ToList();
            return _user;
        }

        public async Task<IEnumerable<Rol>> GetRolesAsync()
        {
            return await this._context.Roles.ToListAsync();
        }
      
        public async Task<Usuario?> GetByEmailAsync(string? correo)
        {
            return await this._context.Usuarios.Include("Roles.Rol").FirstOrDefaultAsync(u => u.Correo == correo);
        }
        //::::::::::::::::::::::::::::::::
        public async Task<IEnumerable<dynamic>> listarusuario()
        {
            //var _user = this._context.Tbl_Usuario.Where(u=>u.Estado==1) .ToList();
            var result = await (from u in _context.Tbl_Usuario
                                join r in _context.Tbl_Rol on u.IdRol equals r.Id
                                where u.Estado == 1
                                select new
                                {
                                    u.Id,
                                    u.IdTipoUsuario,
                                    u.Nombre,
                                    u.Apellido,
                                    u.Dni,
                                    u.Correo,
                                    u.Usuario,
                                    Clave = Encriptacion.DecryptPassword(u.Clave), // Nombrar la propiedad como "Clave"
                                    u.Ping,
                                    u.CodigoUsuario,
                                    u.FechaCreacion,
                                    u.FechaModificacion,
                                    u.IdUsuarioCreacion,
                                    u.IdUsuarioModifica,
                                    u.Estado,
                                    u.IdRol,
                                    NombreRol = r.Nombre
                                }).ToListAsync();

            return result.Cast<dynamic>();
        }
    }
}
