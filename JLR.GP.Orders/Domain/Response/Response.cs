using System.Text.Json.Serialization;

namespace Api_Dc.Domain.Response
{
    public class Response
    {
        public string? Messages { get; set; }
        public int Code { get; set; }
        public string? ResponseType { get; set; }
        ///[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? Errors { get; set; }
    }
}
