﻿using Api_Dc.Domain.Models;
using Api_Dc.Domain.Request;
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
    public class MantenimientoOpService: IMantenimientoOp
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<UsuarioService> _logger;
        private string CnDc_Blinds;
        public MantenimientoOpService(
            IConfiguration configuration,
            DBContext context,
            ILogger<UsuarioService> logger
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
         
       
        public async Task<GeneralResponse<dynamic>> ListarOp(string numCotizacion)
        {
            try
            {

                DataTable result = new DataTable();
                using (SqlConnection cnm = new SqlConnection(CnDc_Blinds))
                {
                    await cnm.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("Sp_ListarMantOP", cnm))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@NumCotizacion", numCotizacion));
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
        ///AQUI ESTOY EDITANDO.
        public async Task<GeneralResponse<object>> EliminarMantenimientooOP(string id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var mantenimientoOp = await _context.Tbl_OrdenProduccion.FindAsync(int.Parse(id));

                    if (mantenimientoOp == null)
                    {
                        return new GeneralResponse<object>(HttpStatusCode.NotFound, new { Respuesta = "El mantenimiento con el ID especificado no fue encontrado", id = 0 });
                    }

                    var grupos = await _context.Tbl_DetalleOpGrupo.Where(g => g.NumeroCotizacion == mantenimientoOp.NumeroCotizacion).ToListAsync();
                    var detallesProduccion = await _context.TBL_DetalleOrdenProduccion.Where(p => p.NumeroCotizacion == mantenimientoOp.NumeroCotizacion).ToListAsync();
                    var ambientes = await _context.Tbl_Ambiente.Where(a => a.NumeroCotizacion == mantenimientoOp.NumeroCotizacion).ToListAsync();

                    _context.Tbl_DetalleOpGrupo.RemoveRange(grupos);
                    _context.TBL_DetalleOrdenProduccion.RemoveRange(detallesProduccion);
                    _context.Tbl_Ambiente.RemoveRange(ambientes);
                    _context.Tbl_OrdenProduccion.Remove(mantenimientoOp);

                    await _context.SaveChangesAsync();

                    // Confirma la transacción
                    await transaction.CommitAsync();

                    var jsonResponse = new
                    {
                        Respuesta = "Ok",
                        id = mantenimientoOp.Id
                    };

                    return new GeneralResponse<object>(HttpStatusCode.OK, jsonResponse);
                }
                catch (Exception ex)
                {
                    // Realiza rollback en caso de error
                    await transaction.RollbackAsync();
                    _logger.LogError($"Error al eliminar la orden de producción: {ex.Message}");

                    var jsonResponse = new
                    {
                        Respuesta = "Error al eliminar la orden de producción: " + ex.Message,
                        idOrden = 0
                    };
                    return new GeneralResponse<object>(HttpStatusCode.InternalServerError, jsonResponse);
                }
            }
        }

    }
}

