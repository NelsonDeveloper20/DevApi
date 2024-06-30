using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_CargaProduccion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CodigoProducto { get; set; }
        public string? Producto { get; set; }
        public string? Accionamiento { get; set; }
        public int? Equivalencia { get; set; }
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public int Estado { get; set; } = 1;






    }
}
