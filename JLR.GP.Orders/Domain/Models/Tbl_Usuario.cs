using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Usuario
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? IdTipoUsuario { get; set; }
        public int? IdRol {  get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Dni { get; set; }
        public string? Correo { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
        public string? CodigoUsuario { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public int? Estado { get; set; }




    }
}
