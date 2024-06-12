using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Componentes
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? DescripcionComponente { get; set; }
        public string? Color { get; set; }
        public string? Unidad { get; set; }
        public string? SubProducto { get; set; }
        public string? CodigoProducto { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public int? Estado { get; set; }






    }
}
