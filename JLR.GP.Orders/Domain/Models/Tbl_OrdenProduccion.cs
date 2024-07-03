using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_OrdenProduccion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string? NumeroCotizacion { get; set; }
        public string? CodigoSisgeco { get; set; }
        public string? NumdoCref { get; set; }
        public DateTime? FechaSap { get; set; }
        // Datos de Sisgeco
        public string? FechaCotizacion { get; set; }
        public string? FechaVenta { get; set; }
        public string? TipoMoneda { get; set; }
        public string? TipoCambio { get; set; }
        public decimal? Monto { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TotalIgv { get; set; }
        public decimal? Total { get; set; }
        public string? Observacion { get; set; }
        public string? Observacion2 { get; set; }

        public int? TipoCliente { get; set; }
        public string? RucCliente { get; set; }
        public string? Cliente { get; set; }
        public string? Departamento { get; set; }
        public string? Provincia { get; set; }
        public string? Distrito { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public int? IdDestino { get; set; }
        public int? IdTipoPeracion { get; set; }
        public int? IdProyecto { get; set; }
        public string? Nivel { get; set; }
        public string? SubNivel { get; set; }

        public string? CodigoVendedor { get; set; }
        public string? NombreVendedor { get; set; }
        public string? Archivo { get; set; }

        public int? IdEstado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModifica { get; set; }


    }
}
