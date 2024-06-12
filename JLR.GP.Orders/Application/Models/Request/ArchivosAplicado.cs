namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ArchivosAplicado
    {
        public string Id { get; set; }
        public string Fecha { get; set; }
        public string Nrosap { get; set; } 
        public string Estado { get; set; }
        public IFormFile? Archivo { get; set; }
        public string ussercreacion { get; set; }
    }
}
