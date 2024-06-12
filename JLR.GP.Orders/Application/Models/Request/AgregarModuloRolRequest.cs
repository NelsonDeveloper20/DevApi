namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AgregarModuloRolRequest
    {
        public int idModulo { get; set; }
        public string modulo { get; set; }
        public int asignado { get; set; }
        public DateTime? fechaAsignado { get; set; }
    }
}
