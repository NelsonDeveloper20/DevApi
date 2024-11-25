using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Api_Dc.Domain.Models
{
    public class Tbl_MaestroArticulos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Identificador { get; set; }
        public int? CodigoGrupo { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoFamilia { get; set; }
        public string? NombreFamilia {  get; set; } 
    }
}
