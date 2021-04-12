using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class RasaEvent
    {
        [JsonPropertyName("event")]
        public string Type { get; set; }
        [JsonPropertyName("parse_data")]
        public ParseData ParseData { get; set; }
    }
}
