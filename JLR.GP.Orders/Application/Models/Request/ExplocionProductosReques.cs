using static ApiPortal_DataLake.Domain.Services.MonitoreoService;

namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ExplocionProductosReques
    {
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
     //  public string WarehouseCode { get; set; }
        //public string AcctCode { get; set; }
        public string CostingCode { get; set; }
      //  public string ProjectCode { get; set; }
        public string CostingCode2 { get; set; }
        public string CostingCode3 { get; set; }
        public string CostingCode4 { get; set; }
        public string CostingCode5 { get; set; }
      //  public string IdSistemaExterno { get; set; }
     //   public string IdLineaSistemaE { get; set; }
       // public string IdOrdenVenta { get; set; }
        public string FamiliaPT { get; set; }
        public string subFamiliaPT { get; set; }
        //public List<BatchNumber> BatchNumbers { get; set; }  // Lista de BatchNumbers
        public string BatchNumberCode { get; set; }  // EN CASO QUE APLIQUE LOTE
        public decimal BatchQuantity { get; set; }
        //public List<SerialNumber> SerialNumbers { get; set; }  // Lista de SerialNumbers
        public string SerialNumberCode { get; set; }  // Ejemplo de campo en SerialNumbers
        public decimal SerialQuantity { get; set; }
    }
}
