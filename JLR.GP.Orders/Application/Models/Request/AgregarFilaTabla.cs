namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AgregarFilaTabla
    {
        public string? Texto { get; set; }
        public IFormFile? Archivo { get; set; }
    }
}
