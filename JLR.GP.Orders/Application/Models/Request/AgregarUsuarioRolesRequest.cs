namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AgregarUsuarioRolesRequest
    { 
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int RolId { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdUnidadNegocio { get; set; }
        public int IdSede { get; set; }
        public string isSelected { get; set; }
        public string idDelete { get; set; }

    }
}
