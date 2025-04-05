namespace ApiPortal_DataLake.Application.Models.Request
{
    public class MonitoreoFormulacionRollerRequest
    { 
        public string? NumeroCotizacion { get; set; }
        public string? Grupo { get; set; }
        public string? Usuario { get; set; }
        public string? Descrip_Componente { get; set; }
        public string? Codigo_Componente { get; set; }
        public string? Componente { get; set; }
        public string? Color { get; set; }
        public string? UnidadMedida { get; set; }
        public string? CantidadUtilizada { get; set; } 
        public string? Merma { get; set; }
        public string? Agregado { get; set; }
        public string? Familia { get; set; }
        public string? SubFamilia { get; set; }
        public string? WhsCode {  get; set; }
        public string? Serie { get; set; }
        public string? Lote { get; set; }
        public string? Alto { get; set; }
        public string? Ancho { get; set; }

    }
}
