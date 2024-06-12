using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPortal_DataLake.Domain.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string Estado { get; set; } = "0";
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUnidadNegocio { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<RolUsuario>? Roles { get; set; } 

    }
}
