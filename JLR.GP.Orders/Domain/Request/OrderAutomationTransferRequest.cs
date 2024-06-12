namespace ApiPortal_DataLake.Domain.Request
{
    public class OrderAutomationTransferRequest
    {
        public string DocDate { set; get; } = DateTime.Now.ToString("yyyyMMdd");
        public string TaxDate { set; get; } = DateTime.Now.ToString("yyyyMMdd"); //"20220307";// DateTime.Now.ToString("yyyyMMdd");
        public string CardCode { set; get; } = string.Empty;
        public string? FromWarehouse { set; get; }
        public string? ToWarehouse { set; get; }
        public string u_SYP_MDMT { set; get; } = "01";
        public string u_SYP_MDTD { set; get; } = "09";
        public string u_SYP_MDTS { set; get; } = "TSI";
        public string? u_SYP_ALMACENERO { set; get; } = string.Empty;
        public string u_SYP_SOLICITANTE { set; get; } = string.Empty;
        public string u_SYP_TIPOTRAS { set; get; } = "T";
        public string u_SYP_LLAMADASER { set; get; } = string.Empty;
        public string u_SYP_NROSERIE { set; get; } = string.Empty;
        public string? u_SYP_NROPLACA { set; get; }
        public string? u_SYP_CCUN { set; get; }
        public string? u_SYP_CCTIENDA { set; get; }
        public string? u_SYP_CCCENCO { set; get; }
        public string? u_GP_PROCEDENCIA { set; get; }
        public Int32 SalesPersonCode { set; get; } = -1;

        // [JsonProperty(PropertyName = )]
        public IEnumerable<OrderAutomationTransferDetail>? DetalleListado { get; set; }
    }

    public class OrderAutomationTransferDetail
    {
        public string? ItemCode { set; get; }
        public int Quantity { set; get; }
        public decimal UnitPrice { set; get; }
        public string? WarehouseCode { set; get; }
        public string? ProjectCode { set; get; }
        public string? CostingCode { set; get; }
        public string? CostingCode2 { set; get; }
        public string? CostingCode3 { set; get; }
        public string CostingCode4 { set; get; } = string.Empty;
        public string CostingCode5 { set; get; } = string.Empty;
        public string DistributionRule { set; get; } = string.Empty;
        public string DistributionRule2 { set; get; } = string.Empty;
        public string DistributionRule3 { set; get; } = string.Empty;
        public string DistributionRule4 { set; get; } = string.Empty;
        public string DistributionRule5 { set; get; } = string.Empty;
    }

}
