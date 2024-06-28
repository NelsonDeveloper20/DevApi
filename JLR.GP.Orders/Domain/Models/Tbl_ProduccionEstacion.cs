using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_ProduccionEstacion
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int?  IdDetalleOp  { get; set; }

    public int? IdEstacion { get; set; }
        public string? NumeroCotizacion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public int? IdUsuario { get; set; }
        public string? TipoProceso { get; set; }
        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioCreacion  { get; set; }

    public int? IdUsuarioModificacion  { get; set; }
	public int? Estado { get; set; }

    }
}
