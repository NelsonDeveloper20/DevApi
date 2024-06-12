namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AgregarListasRequest
    {
         public string ids { get; set; }
        public string nombre { get; set; }
        public string accion { get; set; }
        public string tabla { get; set; }
        public string? valor { get; set; }

    }
}
