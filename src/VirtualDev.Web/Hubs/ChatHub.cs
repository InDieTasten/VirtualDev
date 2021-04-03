using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using VirtualDev.Web.BotConnector;

namespace VirtualDev.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IRasaBotConnector botConnector;

        private string CallerUserName => Context.User.FindFirst("urn:github:name")?.Value ?? "anonymous";

        public ChatHub(IRasaBotConnector botConnector)
        {
            this.botConnector = botConnector;
        }

        public async Task SendMessage(string message)
        {
            // add to chat history
            await Clients.Caller.SendAsync("ReceiveMessage", CallerUserName, message);

            // processing by bot
            var botResponses = await botConnector.SendMessageAsync(new RasaRequest
            {
                ConversationId = CallerUserName,
                Message = message
            });

            foreach (var botResponse in botResponses)
            {
                if (botResponse.Type == RasaResponseTypes.Text)
                    await Clients.Caller.SendAsync("ReceiveMessage", "VirtualDev", botResponse.Message);
                if (botResponse.Type == RasaResponseTypes.Image)
                    await Clients.Caller.SendAsync("ReceiveMessage", "VirtualDev", botResponse.ImageUrl);
            }
        }
    }
}
