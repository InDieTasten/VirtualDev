using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class RasaActionInput
    {
        [JsonPropertyName("next_action")]
        public string NextAction { get; set; }
        [JsonPropertyName("sender_id")]
        public string ConversationId { get; set; }
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("tracker")]
        public Tracker Tracker { get; set; }
    }
}
