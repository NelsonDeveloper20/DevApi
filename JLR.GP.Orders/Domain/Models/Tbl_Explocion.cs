using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Explocion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? NumeroCotizacion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? Nombre_Producto { get; set; }
        public string? Codigo_Producto { get; set; }
        public string? Descrip_Componente { get; set; }
        public string? Cod_Componente { get; set; }
        public string? Descripcion { get; set; }
        public string? Color { get; set; }
        public string? Unidad { get; set; }
        public string? Cantidad { get; set; }
        public string? Merma { get; set; }
        public string? Origen { get; set; } // CARGA O EXPLOCION 
        public int? IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int? IdProducto { get; set; }
        public string? Adicional {  get; set; }
        public string? CodigoSalidaSap { get; set; }
        public string? Familia {  get; set; }
        public string? WhsCode { get; set; }
        public string? Serie { get; set; }
        public string? Lote { get; set; }
        public DateTime FechaEntradaSap { get; set; }
        public string? CodigoEntradaSap { get; set; }

    }
}
