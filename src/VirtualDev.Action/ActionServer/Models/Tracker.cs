using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualDev.Action.ActionServer.Models
{
    public class Tracker
    {
        [JsonPropertyName("sender_id")]
        public string ConversationId { get; set; }
        [JsonPropertyName("slots")]
        public Dictionary<string,string> Slots { get; set; }
        [JsonPropertyName("events")]
        public dynamic[] Events { get; set; }
    }
}
