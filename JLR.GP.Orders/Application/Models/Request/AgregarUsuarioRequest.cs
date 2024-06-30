namespace ApiPortal_DataLake.Application.Models.Request
{
    //llenar los atributos de base de datos
    public class AgregarUsuarioRequest
    {
        public int? Id { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdTipoUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Dni { get; set; }
        public string? Correo { get; set; }
        public int?  IdRol { get; set; }
        public string? Usuario { get; set; }
        public string? Clave { get; set; }
       public string? CodigoUsuario { get; set; }
        public int? Estado { get; set; }

    }
}


//public IEnumerable<int>? Roles { get; set; }