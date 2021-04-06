using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class RasaActionOutput
    {
        [JsonPropertyName("events")]
        public dynamic[] Events { get; set; }
        [JsonPropertyName("responses")]
        public dynamic[] Responses { get; set; }
    }
}
