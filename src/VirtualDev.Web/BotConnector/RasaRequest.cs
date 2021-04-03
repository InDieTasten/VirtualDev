using System.Text.Json.Serialization;

namespace VirtualDev.Web.BotConnector
{
    public class RasaRequest
    {
        /// <summary>
        /// "sender ID of the user sending the message."
        /// Also usable as conversation id.
        /// </summary>
        [JsonPropertyName("sender")]
        public string ConversationId { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
