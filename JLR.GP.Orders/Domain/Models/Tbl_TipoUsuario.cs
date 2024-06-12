using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_TipoUsuario
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Nombre { get; set; }




    }
}
