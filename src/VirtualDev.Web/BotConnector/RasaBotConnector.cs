using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace VirtualDev.Web.BotConnector
{
    public class RasaBotConnector : IRasaBotConnector
    {
        private readonly HttpClient _client;
        public RasaBotConnector(HttpClient client)
        {
            _client = client;
        }
        public async Task<RasaResponse[]> SendMessageAsync(RasaRequest userMessage)
        {
            var response = await _client.PostAsJsonAsync(string.Empty, userMessage);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<RasaResponse[]>(await response.Content.ReadAsStringAsync());
        }
    }
}
