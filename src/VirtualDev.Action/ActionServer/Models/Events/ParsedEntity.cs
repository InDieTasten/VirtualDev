using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class ParsedEntity
    {
        [JsonPropertyName("entity")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("confidence_entity")]
        public double Confidence { get; set; }
    }
}
