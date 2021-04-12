using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class ParseData
    {
        [JsonPropertyName("entities")]
        public IEnumerable<ParsedEntity> Entities { get; set; }
    }
}