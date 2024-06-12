namespace Api_Dc.Domain.Request
{
    public class EstacionProductoRequest
    {
        public int? IdEstacion { get; set; } = 0;
        public string? NumeroCotizacion { get; set; } = "";
        public string? CotizacionGrupo { get; set; } = "";
        public string? CodigoUsuario { get; set; } = "";
        public string? FechaInicio { get; set; } = "";
        public string? FechaFin { get; set; } = "";
        public int? IdUsuarioCreacion { get; set; } =0; 

    }
}
