using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models.Responses
{
    public class TextResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
