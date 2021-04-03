using System.Threading.Tasks;

namespace VirtualDev.Web.BotConnector
{
    public interface IRasaBotConnector
    {
        Task<RasaResponse[]> SendMessageAsync(RasaRequest userMessage);
    }
}
