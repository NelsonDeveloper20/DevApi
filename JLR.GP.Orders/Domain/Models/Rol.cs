using System.ComponentModel.DataAnnotations;

namespace ApiPortal_DataLake.Domain.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
}
