using Api_Dc.Application.Models.Response;
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
using System.Threading;

namespace ApiPortal_DataLake.Domain.Services
{
    public class ProductoService: IProducto
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(
            IConfiguration configuration,
            DBContext context,
            ILogger<ProductoService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }
      /*  public async Task<IEnumerable<TBL_DetalleOrdenProduccion>> GetAllAsync(string NumeroCotizacion )
        {

            try
            {

                var query = await this._context.TBL_DetalleOrdenProduccion
             .Where(o => o.NumeroCotizacion == NumeroCotizacion).ToListAsync();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }

        }*/
        public async Task<IEnumerable<Tbl_Escuadra>> GetAllAsync(string id)
        {

            try
            {

                var query = await this._context.Tbl_Escuadra
             .Where(o => o.IdProducto == Convert.ToInt32(id)).ToListAsync();
                return query;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<GeneralResponse<Object>> AgregarOrden(AgregarOrdenProduccionRequest _ordeRequest, IFormFile? Archivo)
        {
            try
            {
                int idOrden = 0;

              
                var jsonresponse = new
                {
                    Respuesta="Ok",
                    idOrden=idOrden,

                };

                return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse);

            }
            catch (Exception ex)
            {
                this._logger.LogError($"Insertar Orden produccion Error try catch: {JsonConvert.SerializeObject(ex)}");
                var jsonresponse = new
                {
                    Respuesta = ex,
                    idOrden = 0

                };
                return new GeneralResponse<Object>(HttpStatusCode.InternalServerError, jsonresponse);
            }
        }
         


    }
}
