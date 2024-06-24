namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ExplocionComponentesRequest
    { 
        public string NumeroCotizacion { get; set; }
        public string Grupo { get; set; }
        public string Usuario { get; set; }
        public string IdProducto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Componente { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        //public string Agregado { get; set; }
        public string? Color { get; set; }
        public string? UnidadMedida { get; set; }
        public string CantidadUtilizada { get; set; } 
        public string Merma { get; set; } 
    }
}
