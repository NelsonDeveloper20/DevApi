using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace ApiPortal_DataLake.Domain.Services
{
    public class AmbienteService: IAmbiente
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<AmbienteService> _logger;

        public AmbienteService(
            IConfiguration configuration,
            DBContext context,
            ILogger<AmbienteService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }

        public async Task<IEnumerable<Tbl_Ambiente>> listarAmbiente(string numeroCotizacion)
        {
            var _proyecto = await this._context.Tbl_Ambiente.Where(a=> a.NumeroCotizacion== numeroCotizacion) .ToListAsync();
            return _proyecto;
        }
        public async Task<GeneralResponse<Object>> GuararAmbiente(string cotizacion, string indice,string ambiente,string cantidad)
        {
            try
            {
                var _ambiente = new Tbl_Ambiente()
                {
                    NumeroCotizacion= cotizacion,
                    Indice =int.Parse( indice),
                    Ambiente =ambiente,
                    CantidadProducto =int.Parse( cantidad),
                };

                await this._context.Tbl_Ambiente.AddAsync(_ambiente);
                await this._context.SaveChangesAsync();
                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = _ambiente.Id,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar ambiente Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError);
            }
        }
        public async Task<GeneralResponse<Object>> EliminarAmbiente(string id)
        {
            try
            {
                var _ambiente = this._context.Tbl_Ambiente.Find(int.Parse(id)); 
                this._context.Tbl_Ambiente.Remove(_ambiente);
                await this._context.SaveChangesAsync();
                var jsonresponse = new
                {
                    Respuesta = "Ok",
                    idOrden = _ambiente.Id,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"error eliminar ambiente Error try catch: {JsonConvert.SerializeObject(ex)}");
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError);
            }
        }

    }
}
