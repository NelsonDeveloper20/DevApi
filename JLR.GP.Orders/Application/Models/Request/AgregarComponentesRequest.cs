namespace ApiPortal_DataLake.Application.Models.Request
{
    //llenar los atributos de base de datos
    public class AgregarComponentesRequest
    {
        public int Id { get; set; }

        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? DescripcionComponente { get; set; }
        public string? Color { get; set; }
        public string? Unidad { get; set; }
        public string? SubProducto { get; set; }
        public string? CodigoProducto { get; set; } 


    }
}


//public IEnumerable<int>? Roles { get; set; }