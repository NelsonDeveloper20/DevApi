namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AgregarOrdenProduccionRequest
    {

        public string Id { get; set; }

        public string? NumeroCotizacion { get; set; }
        public string? CodigoSisgeco { get; set; }
        public string? NumdoCref { get; set; }
        public string? FechaSap { get; set; }
        // Datos de Sisgeco
        public string? FechaCotizacion { get; set; }
        public string? FechaVenta { get; set; }
        public string? TipoMoneda { get; set; }
        public string? TipoCambio { get; set; }
        public string? Monto { get; set; }
        public string? SubTotal { get; set; }
        public string? TotalIgv { get; set; }
        public string? Total { get; set; }
        public string? Observacion { get; set; }
        public string? Observacion2 { get; set; }

        public string? IdTipoCliente { get; set; }
        public string? RucCliente { get; set; }
        public string? Cliente { get; set; }
        public string? Departamento { get; set; }
        public string? Provincia { get; set; }
        public string? Distrito { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public string? IdDestino { get; set; }
        public string? IdTipoPeracion { get; set; }
        public string? IdProyecto { get; set; }
        public string? Nivel { get; set; }
        public string? SubNivel { get; set; }

        public string? CodigoVendedor { get; set; }
        public string? NombreVendedor { get; set; }

        public string? Archivo { get; set; }
        public string? IdEstado { get; set; }
        public string? FechaRegistro { get; set; }
        public string? FechaModificacion { get; set; }
        public string? IdUsuarioCreacion { get; set; }
        public string? IdUsuarioModifica { get; set; }
    }
}
