namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ActualizarUsuarioRequest
    {
        public string? Nombre { get; set; }
        public string? Estado { get; set; }
        public IEnumerable<int>? Roles { get; set; }
        public string? Correo { get; set; }
        public string? Usuario { get; set; }
        public string? IdUnidadNegocio { get; set; }
    }
}
