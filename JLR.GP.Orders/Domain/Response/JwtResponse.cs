using ApiPortal_DataLake.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPortal_DataLake.Domain.Response
{
    public class JwtResponse
    {
        public string Id { get; set; }
        public string unidadNegocio { get; set; }
        public string Rol { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public Usuario user { get; set; } 
    }
}
