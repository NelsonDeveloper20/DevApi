using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPortal_DataLake.Domain.Models
{
    public class RolUsuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int IdUnidadNegocio { get; set; }
        public int IdSede { get; set; }
        [ForeignKey("RolId")]
        public Rol? Rol { get; set; }
        //[ForeignKey("IdUnidadNegocio")]
       /* public UnidadNegocio? Unidad { get; set; }
        [ForeignKey("IdSede")]
        public Sede? Sede { get; set; }*/
    }
}
