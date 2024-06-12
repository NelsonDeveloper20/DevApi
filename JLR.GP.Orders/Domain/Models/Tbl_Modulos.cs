using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class Tbl_Modulos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string? Ruta { get; set; }
        public string? Icono { get; set; }
        public int? Estado { get; set; }







    }
}
