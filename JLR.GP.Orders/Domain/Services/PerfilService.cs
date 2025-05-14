using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class PerfilService: IPerfilService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<PerfilService> _logger;

        private string CnDc_Blinds;
        public PerfilService(
            IConfiguration configuration,
            DBContext context,
            ILogger<PerfilService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configurations = builder.Build();
            CnDc_Blinds = configurations["ConnectionString:DefaultConnection"];
        }

        public async Task<GeneralResponse<Object>> AgregarModeloRol(int idRol, List<AgregarModuloRolRequest> modulos)

        {
            try
            {
                var idModulo = 1;
                foreach(var item in modulos)
                {

                  var _moduloRol=   this._context.Tbl_ModuloRol.Where(m => m.IdModulo == item.idModulo && m.IdRol == idRol).FirstOrDefault();
                    if (_moduloRol != null)
                    {
                        if (item.asignado == 0)
                        {
                            this._context.Tbl_ModuloRol.Remove(_moduloRol);
                        }
                    }
                    else
                    {
                        if (item.asignado == 1)
                        {
                            var _new = new Tbl_ModuloRol()
                            {
                                IdModulo=item.idModulo,
                                IdRol=idRol,
                                FechaCreacion=DateTime.Now,
                                Estado=1

                            }; 
                            this._context.Tbl_ModuloRol.Add(_new);

                        }

                    }


                }
                this._context.SaveChanges();

                var jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idModulo = idModulo,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ModuloRol Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }

        public async Task<GeneralResponse<Object>> AgregarPerfil(int Id,string Nombre,string Descripcion)
        {
            try
            {
                var jsonresponse = new
                {
                    Respuesta = "",
                    idUsuario = 0,
                };

                int idUser = 0;
                var perfilExiste = this._context.Tbl_Rol.Find(Id);
                var perfilNuevo = perfilExiste == null;

                 
                var existeUsuario = await this._context.Tbl_Rol
                    .Where(u => u.Nombre.Trim().ToUpper() == Nombre.Trim().ToUpper() && u.Id != Id)
                    .ToListAsync();

                if (existeUsuario.Any())
                {
                    jsonresponse = new
                    {
                        Respuesta =  "Ya existe el perfil ingresado",
                        idUsuario = 0,
                    };
                    return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
                }

                if (perfilNuevo)
                {
                    var nuevoPerfil = new Tbl_Rol()
                    {
                        Nombre=Nombre,
                        Descripcion=Descripcion
                    };
                    await this._context.Tbl_Rol.AddAsync(nuevoPerfil);
                    this._context.SaveChanges();
                    idUser = nuevoPerfil.Id;
                }
                else
                {

                    perfilExiste.Nombre = Nombre;
                    perfilExiste.Descripcion= Descripcion;

                    this._context.Tbl_Rol.Update(perfilExiste);
                    this._context.SaveChanges();
                    idUser = perfilExiste.Id;
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
        public async Task<GeneralResponse<Object>> EliminarPerfil(int Id)
        {
            try
            {

                var existePerfil =   this._context.Tbl_Rol.Find(Id);
                this._context.Tbl_Rol.Remove(existePerfil);
                this._context.SaveChanges();
               var  jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idUsuario = existePerfil,
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

        public async Task<IEnumerable<Tbl_Rol>> ListarPerfil()
        {

            var _user = this._context.Tbl_Rol.ToList();
            return _user;
        }
         
        public async Task<GeneralResponse<dynamic>> ListarModulosPorRol(int idRol)
        {
            try
            {
                
                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarModulosPorRol", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@IdRol", idRol));
                        using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                        {
                            adp.Fill(result);
                        }
                    }
                }

                return new GeneralResponse<object>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GeneralResponse<dynamic>> ListarModulosPorUsuario(int idUsuario)
        {
            try
            {
                var result = await (from mr in _context.Tbl_ModuloRol
                                    join m in _context.Tbl_Modulos on mr.IdModulo equals m.Id
                                    where _context.Tbl_Usuario.Any(u => u.Id == idUsuario && u.IdRol == mr.IdRol) && m.Estado==1
                                    orderby m.Orden ascending
                                    select new
                                    {
                                        m.Id,
                                        m.Nombre,
                                        m.Descripcion,
                                        m.Ruta,
                                        m.Icono,
                                        m.Estado
                                    }).ToListAsync();

                return new GeneralResponse<dynamic>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                // Opcionalmente, puedes registrar el error antes de lanzarlo.
                // _logger.LogError(ex, "Error al listar módulos por usuario.");
                throw new Exception("Error al listar módulos por usuario.", ex);
            }
        }
    }
}
