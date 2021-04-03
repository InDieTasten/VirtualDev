#nullable enable
using System.Text.Json.Serialization;

namespace VirtualDev.Web.BotConnector
{
    public class RasaResponse
    {
        [JsonPropertyName("text")]
        public string? Message { get; set; }
        [JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        public RasaResponseTypes Type
        {
            get
            {
                if (Message != null)
                {
                    return RasaResponseTypes.Text;
                } else if (ImageUrl != null)
                {
                    return RasaResponseTypes.Image;
                } else
                {
                    return RasaResponseTypes.Unknown;
                }
            }
        }
}
}
