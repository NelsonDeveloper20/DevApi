using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ApiPortal_DataLake.Domain.Services
{
    public class ProyectoService: IProyecto
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<ProyectoService> _logger;

        public ProyectoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<ProyectoService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }
        public async Task<IEnumerable<Tbl_Proyecto>> listarProyecto()
        {
            var _proyecto =  await this._context.Tbl_Proyecto.OrderByDescending(p=>p.Id).ToListAsync();

            return _proyecto;
        }
        public async Task<GeneralResponse<Object>> GuararProyecto(string nombreProyecto)
        {
            try { 
            var _proyecto = new Tbl_Proyecto()
            {
                Nombre=nombreProyecto
            };

            await this._context.Tbl_Proyecto.AddAsync(_proyecto);
            await this._context.SaveChangesAsync();
            var jsonresponse = new
            {
                Respuesta = "Ok",
                idOrden = _proyecto.Id,

            };

            return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError);
            }
        }
        public async Task<GeneralResponse<Object>> ModificarProyecto(string id, string nombreProyecto)
        {
            try
            {
                var _proyecto = this._context.Tbl_Proyecto.Find(int.Parse(id));
                _proyecto.Nombre = nombreProyecto;

                this._context.Tbl_Proyecto.Update(_proyecto);
                await this._context.SaveChangesAsync();
                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = _proyecto.Id,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError);
            }
        }

    }
}
