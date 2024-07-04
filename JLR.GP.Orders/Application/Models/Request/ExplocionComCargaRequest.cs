namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ExplocionComCargaRequest
    {
        public string Cotizacion { get; set; }
        public string Grupo { get; set; }
        public string Nombre_Producto { get; set; }
        public string Componente { get; set; }
        public string Cod_Componente { get; set; }
        public string Descripcion { get; set; }
        public string? Color { get; set; }
        public string? Unidad { get; set; }
        public string Cantidad { get; set; }
        public string Merma { get; set; }
        public string Codigo_Producto { get; set; } 
        public string Usuario { get; set;}
    }
}
