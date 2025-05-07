using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class ComponentesService : IComponentes
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;

        public ComponentesService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }
         
        public async Task<GeneralResponse<Object>> AgregarComponentes(AgregarComponentesRequest componente)
        {
            try
            { 

                int idUser = 0;
                var compoExiste = this._context.Tbl_Componentes.Find(componente.Id);
                var ComponenteNuevo = compoExiste == null;
                 

                if (ComponenteNuevo)
                {
                    var nuevoComponente = new Tbl_Componentes()
                    {
                
                    Codigo =componente.Codigo,
                    Nombre =componente.Nombre,
                    DescripcionComponente =componente.DescripcionComponente,
                    Color =componente.Color,
                    Unidad =componente.Unidad,
                    SubProducto =componente.SubProducto,
                    CodigoProducto =componente.CodigoProducto,
                    FechaCreacion = DateTime.Now,
                    IdUsuarioCreacion = 1,
                    Estado =1
                    };
                    await this._context.Tbl_Componentes.AddAsync(nuevoComponente);
                    this._context.SaveChanges();
                    idUser = nuevoComponente.Id;
                }
                else
                {
                    compoExiste.Codigo = componente.Codigo;
                    compoExiste.Nombre = componente.Nombre;
                    compoExiste.DescripcionComponente = componente.DescripcionComponente;
                    compoExiste.Color = componente.Color;
                    compoExiste.Unidad = componente.Unidad;
                    compoExiste.SubProducto = componente.SubProducto;
                    compoExiste.CodigoProducto = componente.CodigoProducto;
                    compoExiste.FechaModificacion = DateTime.Now;
                    compoExiste.IdUsuarioModifica = 1;

                    this._context.Tbl_Componentes.Update(compoExiste);
                    this._context.SaveChanges();
                    idUser = compoExiste.Id;
                }

               var jsonresponse = new
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
         public async Task<GeneralResponse<Object>> EliminarComponente(int id)
        {
            try
            {
                var _roles = this._context.Tbl_Componentes.Find(id);
                           
                this._context.Tbl_Componentes.Remove(_roles);
                await this._context.SaveChangesAsync();
                var jsonresponse = new
                {
                    Respuesta = "Operacion realizada correctamente",
                    idUsuario = 1,
                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Eliminar Componente Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0
                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
          
        //::::::::::::::::::::::::::::::::
        public async Task<IEnumerable<Tbl_Componentes>> listarComponetes()
        {

            var _user = this._context.Tbl_Componentes.ToList();
            return _user;
        }
         
        public async Task<IEnumerable<Tbl_AccesorioProducto>> listarAccesorio(string codigoProducto)
        {
            if (codigoProducto == "PRTRSMan" || codigoProducto == "PRTRSMot" || codigoProducto == "PRTRZ")
            {
                var _user = this._context.Tbl_AccesorioProducto
                    .Where(s => s.CodigoProducto.StartsWith(codigoProducto.Substring(0, 5)))
                    .ToList();
                return _user;
            }

            if (codigoProducto == "PRTRM00000016" || codigoProducto == "PRTRM00000001" ||
                codigoProducto == "PRTRH00000001" || codigoProducto == "PRTRF00000001")
            {
                var _user = this._context.Tbl_AccesorioProducto
                    .Where(s => s.CodigoProducto == codigoProducto)
                    .ToList();
                return _user;
            }

            if (codigoProducto == "PRTLU00000001_2_3" || codigoProducto== "PRTLU00000001" || codigoProducto== "PRTLU00000002" || codigoProducto== "PRTLU00000003")
            {
                var _user = this._context.Tbl_AccesorioProducto
                    .Where(s => new[] { "PRTLU00000001", "PRTLU00000002", "PRTLU00000003" }.Contains(s.CodigoProducto))
                    .ToList();
                return _user;
            }

            return Enumerable.Empty<Tbl_AccesorioProducto>();
        }

        public async Task<IEnumerable<Tbl_ComponteProducto>> listarTelaRielTubo(string tipo, string codigoProducto, string nombreProducto)
        {
            IQueryable<Tbl_ComponteProducto> query = this._context.Tbl_ComponteProducto;

            switch (tipo)
            {
                case "TELA":
                    if (codigoProducto == "PRTRS000000")
                    {
                        query = query.Where(s => s.Tipo == "TELA0" && s.CodigoProducto == codigoProducto && s.NombreProducto == nombreProducto);
                    }
                    else
                    {
                        query = query.Where(s => s.Tipo == "TELA" && s.CodigoProducto == codigoProducto);
                    }
                    break;

                case "TUBO":
                    query = query.Where(s => s.Tipo == "TUBO" && s.CodigoProducto.StartsWith(codigoProducto.Substring(0, 5)));
                    break;

                case "RIEL":
                    query = query.Where(s => s.Tipo == "RIEL" && s.CodigoProducto.StartsWith(codigoProducto.Substring(0, 5)));//PRTRS
                    break;
            }

            return await query.ToListAsync();
        }
    }
}
