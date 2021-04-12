using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class TextResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
