using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Core;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ApiPortal_DataLake.Domain.Services
{
    public class OrdenProduccionService: IOrdenProduccion
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<OrdenProduccionService> _logger;
        private readonly IWebHostEnvironment _env;


        public OrdenProduccionService(
            IConfiguration configuration,
            DBContext context,
            ILogger<OrdenProduccionService> logger, IWebHostEnvironment env
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
            _env = env;
        }
        public async Task<IEnumerable<Tbl_OrdenProduccion>> GetAllAsync(string numeroCotizacion)
        {
           
                var query = await _context.Tbl_OrdenProduccion
                    .Where(o => o.NumeroCotizacion == numeroCotizacion)
                    .ToListAsync();
                return query;
          
             
        }
        #region // AGREGAR PRODUCTO
        public async Task<GeneralResponse<Object>> AgregarOrden(AgregarOrdenProduccionRequest _ordeRequest, IFormFile? Archivo)
        {
            try
            {
                int idOrden = 0;

                var NombreArchivo = "";
                string formatoFecha = "dd/MM/yyyy";
                var query = await this._context.Tbl_OrdenProduccion.Where(o => o.NumeroCotizacion == _ordeRequest.NumeroCotizacion).ToListAsync();

                if(query.Count > 0 && _ordeRequest.Id == null)
                {
                    var jsonresponse1 = new
                    {
                        Respuesta = "Ya existe",
                        idOrden = idOrden,

                    };

                    return new GeneralResponse<Object>(HttpStatusCode.OK, jsonresponse1);

                }
                //DateTime FechaSap = DateTime.ParseExact(_ordeRequest.FechaSap, formatoFecha, CultureInfo.InvariantCulture);
                string FechaSap = _ordeRequest.FechaSap;
                if (Archivo != null && Archivo.Length > 0)
                {
                   
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    var configuration = builder.Build();
                    var rutaArchivo = configuration["Archivos:ruta"];                
                     
                    
                      NombreArchivo = $"{_ordeRequest.NumeroCotizacion}_{"File"}{Path.GetExtension(Archivo.FileName)}";
                    var filePath = Path.Combine(rutaArchivo, NombreArchivo);     
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Asegura que el directorio existe
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Archivo.CopyToAsync(stream);
                    }

                }
                if (_ordeRequest.Id== "undefined" || _ordeRequest.Id=="null" || _ordeRequest.Id == "" || _ordeRequest.Id==null)
                {

                    //decimal Monto, SubTotal, TotalIgv, Total; 
                    //decimal.TryParse(_ordeRequest.Monto, out Monto);
                    //decimal.TryParse(_ordeRequest.SubTotal, out SubTotal);
                    //decimal.TryParse(_ordeRequest.TotalIgv, out TotalIgv);
                    //decimal.TryParse(_ordeRequest.Total, out Total);
                   
                    var newOrden = new Tbl_OrdenProduccion()
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
                    Monto= Convert.ToDecimal(_ordeRequest.Monto.Replace(".", ",")),
                    SubTotal = Convert.ToDecimal(_ordeRequest.SubTotal.Replace(".", ",")),
                    TotalIgv = Convert.ToDecimal(_ordeRequest.TotalIgv.Replace(".", ",")),
                    Total = Convert.ToDecimal(_ordeRequest.Total.Replace(".", ",")),
                    Observacion=_ordeRequest.Observacion,
                    Observacion2=_ordeRequest.Observacion2,

                    TipoCliente= _ordeRequest.TipoCliente,
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
                    await this._context.Tbl_OrdenProduccion.AddAsync(newOrden);
                    await this._context.SaveChangesAsync();
                    idOrden = newOrden.Id;


                }
                else
                {
                    idOrden = int.Parse(_ordeRequest.Id); 
                    var _orden = this._context.Tbl_OrdenProduccion.Find(idOrden);      
                    if(_orden!= null) {
                       
                       // _orden.NumeroCotizacion = _ordeRequest.NumeroCotizacion;
                      _orden.CodigoSisgeco = _ordeRequest.NumdoCref;
                       // _orden.NumdoCref = _ordeRequest.NumdoCref;
                        _orden.FechaSap = FechaSap;
                    // Datos de Sisgeco
                    /*
                        _orden.FechaCotizacion = _ordeRequest.FechaCotizacion;
                        _orden.FechaVenta = _ordeRequest.FechaVenta;
                        _orden.TipoMoneda = _ordeRequest.TipoMoneda;
                        _orden.TipoCambio =_ordeRequest.TipoCambio;
                        _orden.Monto = Monto;
                        _orden.SubTotal =SubTotal;
                        _orden.TotalIgv = TotalIgv;
                        _orden.Total = Total;
                        _orden.Observacion = _ordeRequest.Observacion;
                        _orden.Observacion2 = _ordeRequest.Observacion2;*/

                        _orden.TipoCliente = _ordeRequest.TipoCliente;
                        /*_orden.RucCliente = _ordeRequest.RucCliente;
                        _orden.Cliente = _ordeRequest.Cliente;
                        _orden.Departamento = _ordeRequest.Departamento;
                        _orden.Provincia = _ordeRequest.Provincia;
                        _orden.Distrito = _ordeRequest.Distrito;
                        _orden.Direccion = _ordeRequest.Direccion;
                        _orden.Telefono = _ordeRequest.Telefono;*/

                        _orden.IdDestino = int.Parse(_ordeRequest.IdDestino);
                        _orden.IdTipoPeracion = int.Parse(_ordeRequest.IdTipoPeracion);
                        _orden.IdProyecto = int.Parse(_ordeRequest.IdProyecto);
                        _orden.Nivel = _ordeRequest.Nivel;
                        _orden.SubNivel = _ordeRequest.SubNivel;
                        _orden.Archivo = NombreArchivo;
                        /*
                        _orden.CodigoVendedor = _ordeRequest.CodigoVendedor;
                        _orden.NombreVendedor = _ordeRequest.NombreVendedor;*/

                        //estado
                        //IdEstado=
                        _orden.IdUsuarioModifica = int.Parse(_ordeRequest.IdUsuarioCreacion);
                        _orden.FechaModificacion = DateTime.Now;
                        this._context.Tbl_OrdenProduccion.Update(_orden);
                        await this._context.SaveChangesAsync();
                    }



                }
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

#endregion

    }
}
