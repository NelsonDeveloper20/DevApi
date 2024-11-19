using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_DetalleOpGrupo
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdTbl_OrdenProduccion { get; set; }
        public string? NumeroCotizacion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? CodigoSisgeco { get; set; }
        public DateTime? FechaProduccion { get; set; }
        public string? Turno { get; set; }
        public string? Tipo { get; set; }

        public int? IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int? IdEstado { get; set; }
        public string? CodigoSalidaSap {  get; set; }

    }
}
