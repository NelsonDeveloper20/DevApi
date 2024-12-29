using System.Xml.Linq;

namespace ApiPortal_DataLake.Application.Models.Request
{
    public class ExplocionComCargaRequest
    {
    public string   Cotizacion { get; set; }
        public string GrupoCotizacion { get; set; } 
        public string Usuario { get; set; }
        public List<ExplocionProductosReques>? DocumentLines { get; set; }  // Lista de Items
    }
}
