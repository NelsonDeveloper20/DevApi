using System.ComponentModel.DataAnnotations;

namespace ApiPortal_DataLake.Domain.Models
{
    public class TokenSecurity
    {
        [Key]
        public int Id { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
