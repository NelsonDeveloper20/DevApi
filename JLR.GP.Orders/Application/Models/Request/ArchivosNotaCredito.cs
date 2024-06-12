namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ArchivosNotaCredito
    {
        public string Id { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
        public IFormFile? Archivo { get; set; }
        public IFormFile? archivoporaplicar { get; set; }
        public string? finalizaadv { get; set; }
        public string ussercreacion { get; set; }
    }
}
