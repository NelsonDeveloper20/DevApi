using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_UsuarioRol
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public int? Estado { get; set; }



    }
}
