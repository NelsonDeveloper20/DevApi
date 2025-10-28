namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ModificarMermaRequest
    {
        public int Id { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemDescription { get; set; }
        public string Merma { get; set; }
        public string? Lote { get; set; }
    }
}
