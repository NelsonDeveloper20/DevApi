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
        public async Task<IEnumerable<TBL_DetalleOrdenProduccion>> GetAllAsync(string NumeroCotizacion )
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

        }

        public async Task<GeneralResponse<Object>> AgregarOrden(AgregarOrdenProduccionRequest _ordeRequest, IFormFile? Archivo)
        {
            try
            {
                int idOrden = 0;

                /*
                if (_ordeRequest.Id== "undefined" || _ordeRequest.Id=="null" || _ordeRequest.Id == "" || _ordeRequest.Id==null)
                {

                    decimal Monto, SubTotal, TotalIgv, Total; 
                    decimal.TryParse(_ordeRequest.Monto, out Monto);
                    decimal.TryParse(_ordeRequest.SubTotal, out SubTotal);
                    decimal.TryParse(_ordeRequest.TotalIgv, out TotalIgv);
                    decimal.TryParse(_ordeRequest.Total, out Total);
                    var newOrden = new Tbl_Producto()
                    {
 
                    //FechSolicitud = fechaSoli.ToString("dd/MM/yyyy"),//agregarSolicitudE.fechsolicitud, 
                    NumeroCotizacion=_ordeRequest.NumeroCotizacion,
                    CodigoSisgeco=_ordeRequest.CodigoSisgeco,
                    NumdoCref=_ordeRequest.NumdoCref,
                    FechaSap=FechaSap,
                    // Datos de Sisgeco
                    FechaCotizacion=_ordeRequest.FechaCotizacion,
                    FechaVenta=_ordeRequest.FechaVenta,
                    TipoMoneda=_ordeRequest.TipoMoneda,
                    TipoCambio= _ordeRequest.TipoCambio,
                    Monto= Monto,
                    SubTotal = SubTotal,
                    TotalIgv = TotalIgv,
                    Total = Total,
                    Observacion=_ordeRequest.Observacion,
                    Observacion2=_ordeRequest.Observacion2,

                    IdTipoCliente= int.Parse(_ordeRequest.IdTipoCliente),
                    RucCliente=_ordeRequest.RucCliente,
                    Cliente=_ordeRequest.Cliente,
                    Departamento=_ordeRequest.Departamento,
                    Provincia=_ordeRequest.Provincia,
                    Distrito=_ordeRequest.Distrito,
                    Direccion=_ordeRequest.Direccion,
                    Telefono=_ordeRequest.Telefono,

                    IdDestino= int.Parse(_ordeRequest.IdDestino),
                    IdTipoPeracion= int.Parse(_ordeRequest.IdTipoPeracion),
                    IdProyecto= int.Parse(_ordeRequest.IdProyecto),
                    Nivel= _ordeRequest.Nivel,
                    SubNivel=_ordeRequest.SubNivel,

                    CodigoVendedor=_ordeRequest.CodigoVendedor,
                    NombreVendedor=_ordeRequest.NombreVendedor,

                    Archivo= NombreArchivo,
                    //estado
                    IdEstado=1,
                    FechaRegistro =DateTime.Now,
                    IdUsuarioCreacion=int.Parse( _ordeRequest.IdUsuarioCreacion) 
                     };
                    await this._context.Tbl_Producto.AddAsync(newOrden);
                    await this._context.SaveChangesAsync();
                    idOrden = newOrden.Id;


                }
                else
                {
                    idOrden = int.Parse(_ordeRequest.Id); 
                    var _orden = this._context.Tbl_Producto.Find(idOrden);      
                    if(_orden!= null) {
                       
                    
                        _orden.IdUsuarioModifica = int.Parse(_ordeRequest.IdUsuarioCreacion);
                        _orden.FechaModificacion = DateTime.Now;
                        this._context.Tbl_Producto.Update(_orden);
                        await this._context.SaveChangesAsync();
                    }



                }*/
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
