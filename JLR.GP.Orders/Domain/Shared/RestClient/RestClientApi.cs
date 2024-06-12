using Newtonsoft.Json;
using RestSharp;

namespace ApiPortal_DataLake.Domain.Shared
{
    public class RestClientApi
    {
        private readonly IConfiguration _configuration;
        public RestClientApi(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<RestResponse> ExecuteClientAsync(RestClientRequest request)
        {
            var restClient = new RestClient(this._configuration[$"Services:{request.ServicePath}"]);
            return await restClient.ExecuteAsync(this.GetRequest(request));
        }

        public async Task<RestResponse<T>> ExecuteClientAsync<T>(RestClientRequest request)
        {
            var restClient = new RestClient(this._configuration[$"Services:{request.ServicePath}"]);
            return await restClient.ExecuteAsync<T>(this.GetRequest(request));
        }

        private RestRequest GetRequest(RestClientRequest request)
        {
            var requestClient = new RestRequest(request.Url, request.Method);

            if (request.Headers != null)
            {
                foreach (KeyValuePair<string, string> item in request.Headers)
                {
                    requestClient.AddHeader(item.Key, item.Value);
                }
            }

            if (request.Params != null)
            {
                foreach (KeyValuePair<string, string> item in request.Params)
                {
                    requestClient.AddParameter(item.Key, item.Value);
                }
            }

            if (request.Body != null)
            {
                requestClient.AddJsonBody(request.Body);
            }

            if (request.Token != null)
            {
                requestClient.AddHeader("Authorization", $"Bearer {request.Token}");
            }

            requestClient.AddHeader("Content-Type", "application/json");
            requestClient.AddHeader("Accept", "application/json");

            return requestClient;
        }
    }
}
