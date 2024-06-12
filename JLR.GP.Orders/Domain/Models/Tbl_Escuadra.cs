using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Escuadra
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //public int Id_TBL_DetalleOrdenProduccion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? Cantidad { get; set; }
        public int? IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }






    }
}
