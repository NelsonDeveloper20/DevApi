using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Ambiente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NumeroCotizacion { get; set; }
        public int? Indice { get; set; }
        public string? Ambiente { get; set; }
        public int? CantidadProducto { get; set; }
        public int? Stock {  get; set; }
        public DateTime? FechaProduccion { get; set; }
        public string? Turno { get; set; }
    }
}
