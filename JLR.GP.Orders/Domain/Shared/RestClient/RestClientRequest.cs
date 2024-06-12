using RestSharp;

namespace ApiPortal_DataLake.Domain.Shared
{
    public class RestClientRequest
    {
        public Method Method { get; set; }

        public string? Url { get; set; }

        public string? ServicePath { get; set; }

        public string? Token { get; set; }

        public Dictionary<string, string>? Params { get; set; }

        public Dictionary<string, string>? Headers { get; set; }

        public object? Body { get; set; }
    }
}
