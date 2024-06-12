using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Dc.Domain.Models
{
    public class Tbl_DetalleProductoEstacion
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? IdProduccionEstacion { get; set; }
        public int? IdDetalleOrdenProduccion { get; set; }

        public string? NumeroCotizacion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? CodigoProducto { get; set; }
       /* public string? NombreProducto { get; set; }
        public string? Unidad { get; set; }
        public string? Accionamiento { get; set; }
        public string? CantidadReal { get; set; }

        public string? TipoAccesorio { get; set; }
        public string? CodigoAccesorio { get; set; }
        public string? NombreAccesorio { get; set; }
        public string? CantidadAccesorio { get; set; }
        public string? MermaAccesorio { get; set; }*/
       public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }

        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }

        public int? Estado { get; set; } = 1;
    }



}
