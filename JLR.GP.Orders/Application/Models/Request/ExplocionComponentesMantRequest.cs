namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ExplocionComponentesMantRequest
    { 
        public string id { get; set; }
        public string? codigo_Producto { get; set; }
        public string? nombre_Producto { get; set; }
        public string? componente { get; set; }
        public string? cod_Componente { get; set; }
        public string? descripcion { get; set; }
        public string? color { get; set; }
        public string? unidad { get; set; }
        public string? cantidad { get; set; }
        public string? merma { get; set; } 
        public string? numeroCotizacion { get; set; }
        public string? cotizacionGrupo { get; set; }
        public string idUsuarioCrea { get; set; }  
    
}
}
