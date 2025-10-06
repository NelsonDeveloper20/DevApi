using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_ExplocionSap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        // Cabecera
        public int Id { get; set; }
        public string? DocDate { get; set; }
        public string? TaxDate { get; set; }
        public string? Comments { get; set; }
        public string? Reference2 { get; set; }
        public string? U_EXX_TIPOOPER { get; set; }
        public string? IdSistemaExterno { get; set; }
        public string? IdOrdenVenta { get; set; }

        // Detalle
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string? Quantity { get; set; }
        public string? WarehouseCode { get; set; }
        public string? AcctCode { get; set; }
        public string? CostingCode { get; set; }
        public string? ProjectCode { get; set; }
        public string? CostingCode2 { get; set; }
        public string? CostingCode3 { get; set; }
        public string? CostingCode4 { get; set; }
        public string? CostingCode5 { get; set; }
        public string? IdLineaSistemaE { get; set; }
        public string? FamiliaPT { get; set; }
        public string? SubFamiliaPT { get; set; }

        // Listas anidadas (JSON)
        public string? BatchNumbers { get; set; }
        public string? SerialNumbers { get; set; }
        public string? CodigoSalidaSap { get; set; }
        public string? CodigoSalidaMermaSap { get; set; }
        public string? CodigoEntradaSap { get; set; }
        public int?    IdUsuarioCrea { get; set; }
        public int? IdUsuarioCreaMerma { get; set; }
        public DateTime?  FechaCreacion { get; set; }
        public DateTime? FechaSalidaMerma { get; set; }
        public string? Tipo { get; set; }
        public string? CotizacionGrupo { get; set; } 
    }
}
