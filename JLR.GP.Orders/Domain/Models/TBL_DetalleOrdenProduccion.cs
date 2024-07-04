using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Dc.Domain.Models
{
    public class TBL_DetalleOrdenProduccion
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? IdTbl_DetalleOpGrupo { get; set; }
        public string? NumeroCotizacion { get; set; }
        public string? CotizacionGrupo { get; set; }
        public string? CodigoSisgeco { get; set; }
        // Producto
        public string? CodigoProducto { get; set; }
        public string? NombreProducto { get; set; }
        public string? Familia { get; set; }
        public string? SubFamilia { get; set; }
        public string? UnidadMedida { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Alto { get; set; }
        public decimal? Ancho { get; set; }
        public string? Linea { get; set; }
        public string? Precio { get; set; }
        public string? PrecioInc { get; set; }
        public string? Igv { get; set; }//ERROR MARISOL
        public string? Lote { get; set; }

        //pendientes -agregados
        public decimal? TuboMedida{get;set; }
       public decimal? TelaMedida { get; set; }
        public decimal? AlturaMedida {get; set; } 
        public string? Central{get;set; }
        public string? CentralIndex { get; set; }
        public int? IndexDetalle {get; set; }
        public string? Escuadra { get; set; }

        //END
        public DateTime? FechaProduccion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Nota { get; set; }
        public string? Color { get; set; }
        public string? Tipo { get; set; }
        // Campos del formulario
        public string? IndiceAgrupacion { get; set; }
        public string? IdTbl_Ambiente { get; set; }
        public string? Ambiente { get; set; }
        public string? Turno { get; set; }
        public string? SoporteCentral { get; set; }
        public string? TipoSoporteCentral { get; set; }
        public string? Caida { get; set; }
        public string? Accionamiento { get; set; }
        public string? CodigoTubo { get; set; }
        public string? NombreTubo { get; set; }
        public string? Mando { get; set; }
        public string? TipoMecanismo { get; set; }
        public string? ModeloMecanismo { get; set; }
        public string? TipoCadena { get; set; }
        public string? CodigoCadena { get; set; }
        public string? Cadena { get; set; }
        public string? TipoRiel { get; set; }
        public string? TipoInstalacion { get; set; }
        public string? CodigoRiel { get; set; }
        public string? Riel { get; set; }
        public string? TipoCassete { get; set; }
        public string? Lamina { get; set; }
        public string? Apertura { get; set; }
        public string? ViaRecogida { get; set; }
        public string? TipoSuperior { get; set; }
        public string? CodigoBaston { get; set; }
        public string? Baston { get; set; }
        public string? NumeroVias { get; set; }
        public string? TipoCadenaInferior { get; set; }
        public string? MandoCordon { get; set; }
        public string? MandoBaston { get; set; }
        public string? CodigoBastonVarrilla { get; set; }
        public string? BastonVarrilla { get; set; }
        public string? Cabezal { get; set; }
        public string? CodigoCordon { get; set; }
        public string? Cordon { get; set; }
        public string? CodigoCordonTipo2 { get; set; }
        public string? CordonTipo2 { get; set; }
        public string? Cruzeta { get; set; }
        public string? Dispositivo { get; set; }
        public string? CodigoControl { get; set; }
        public string? Control { get; set; }
        public string? CodigoSwitch { get; set; }
        public string? Switch { get; set; }
        public string? LlevaBaston { get; set; }
        public string? MandoAdaptador { get; set; }
        public string? CodigoMotor { get; set; }
        public string? Motor { get; set; }
        public string? CodigoTela { get; set; }
        public string? Tela { get; set; }
        public string? Cenefa { get; set; }
        public int? NumeroMotores { get; set; }
        public string? Serie { get; set; }
        public decimal? AlturaCadena { get; set; }
        public decimal? AlturaCordon { get; set; }
        public string? MarcaMotor { get; set; }

        public int? IdUsuarioCrea { get; set; }
        public int? IdUsuarioModifica { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModifica { get; set; }
        public int? IdEstado { get; set; }




    }
}
