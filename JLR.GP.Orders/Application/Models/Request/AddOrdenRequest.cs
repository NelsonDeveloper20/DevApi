namespace ApiPortal_DataLake.Application.Models.Request
{
    public class AddOrdenRequest
    {

        public AgregarOrdenProduccionRequest orden { get; set; }
        public IFormFile? archivo { get; set; }
    }
}
