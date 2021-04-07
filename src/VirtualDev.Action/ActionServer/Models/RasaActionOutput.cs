using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class RasaActionOutput
    {
        [JsonPropertyName("events")]
        public IEnumerable<dynamic> Events { get; set; } = new List<dynamic>();
        [JsonPropertyName("responses")]
        public IEnumerable<dynamic> Responses { get; set; } = new List<dynamic>();
    }
}
