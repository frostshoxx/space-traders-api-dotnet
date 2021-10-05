using System.Text.Json.Serialization;

namespace SpaceTradersApiDotNet.Data
{
    public class ServerStatus
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
