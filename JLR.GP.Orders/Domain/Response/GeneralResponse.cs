using System.Net;

namespace ApiPortal_DataLake.Domain.Response
{
    public class GeneralResponse<T>
    {
        public GeneralResponse(HttpStatusCode status)
        {
            Status = status;
        }

        public GeneralResponse(HttpStatusCode status, T json)
        {
            Status = status;
            JSON = json;
        }

        public HttpStatusCode Status { get; set; }
        public T? JSON { get; set; }
    }
}
