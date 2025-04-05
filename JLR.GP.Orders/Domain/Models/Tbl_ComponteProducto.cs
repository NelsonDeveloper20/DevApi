using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Dc.Domain.Models
{
    public class Tbl_ComponteProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? CodigoProducto { get; set; }
        public string? NombreProducto { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModifica { get; set; }

        public int? Estado { get; set; }
    }
}
