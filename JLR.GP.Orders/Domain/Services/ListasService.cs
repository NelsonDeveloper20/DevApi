using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace ApiPortal_DataLake.Domain.Services
{
    public class ListasService: IListasService
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<ListasService> _logger;

        public ListasService(
            IConfiguration configuration,
            DBContext context,
            ILogger<ListasService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }  
        //INIT BANCOS 
        public async Task<IEnumerable<ListasResponse>> GetListar(string tabla)
        {
            List<ListasResponse> list = new List<ListasResponse>();
             
            switch (tabla)
            {
                case "TipoCliente":
                     var lista = await this._context.Tbl_TipoCliente.ToListAsync();
                    foreach (var rows in lista)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre 
                        };
                        list.Add(fila);
                    };
                    break;
                case "Destino":
                    var lista1 = await this._context.Tbl_Destino.ToListAsync();
                    foreach (var rows in lista1)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre
                        };
                        list.Add(fila);
                    };
                    break; 
                case "TipoOperacion":
                    var lista2 = await this._context.Tbl_TipoOperacion.ToListAsync();
                    foreach (var rows in lista2)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre
                        };
                        list.Add(fila);
                    };
                    break;
                case "Proyecto":
                    var lista3 = await this._context.Tbl_Proyecto.ToListAsync();
                    foreach (var rows in lista3)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre
                        };
                        list.Add(fila);
                    };
                    break;
                case "TipoUsuario":
                    var lista4 = await this._context.Tbl_TipoUsuario.ToListAsync();
                    foreach (var rows in lista4)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre
                        };
                        list.Add(fila);
                    };
                    break;
                case "Rol":
                    var lista5 = await this._context.Tbl_Rol.ToListAsync();
                    foreach (var rows in lista5)
                    {
                        var fila = new ListasResponse()
                        {
                            Id = rows.Id,
                            Nombre = rows.Nombre
                        };
                        list.Add(fila);
                    };
                    break;
                default:

                    break;

            }
             

            return  list;
        }
        public async Task<GeneralResponse<int>> Agregar(AgregarListasRequest items)
        {
            try
            { 
                 
                return new GeneralResponse<int>(HttpStatusCode.OK, 1);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"FacturaCarga Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<int>(HttpStatusCode.InternalServerError);
            }
        }


        //INIT FORMA PAGO

     


    }
}
