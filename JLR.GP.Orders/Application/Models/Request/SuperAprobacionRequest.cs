namespace Api_Dc.Application.Models.Request
{
    public class SuperAprobacionRequest
    {
        public string? CotizacionGrupo { get; set; }
        public string? TurnoInicial { get; set; }
        public string? TurnoCambio { get; set; }
        public string? FechProdInicial { get; set; }
        public string? FechaProdCambio { get; set; }
        public string? FechaCreacion { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdUsuarioSolicita { get; set; }
        public int? NumeroCotizacion { get; set; }
    }
}
