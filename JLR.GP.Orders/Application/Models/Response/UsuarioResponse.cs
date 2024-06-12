namespace ApiPortal_DataLake.Application.Models.Response
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Estado { get; set; }
        public RolResponse? Roles { get; set; }
        public string? IdUnidadNegocio { get; set; }
    }
}
