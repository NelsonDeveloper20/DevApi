using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_SupervisorAprobacion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? TurnoInicial { get; set; }
        public string? TurnoCambio { get; set; }
        public DateTime? FechProdInicial { get; set; }
        public DateTime? FechaProdCambio { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdUsuarioSolicita { get; set; }
        public int? NumeroCotizacion { get; set; }
        public string Estado { get; set; }
    }
}
