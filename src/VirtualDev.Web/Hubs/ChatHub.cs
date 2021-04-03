using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace VirtualDev.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private string CallerUserName => Context.User.FindFirst("urn:github:name")?.Value ?? "anonymous";

        public async Task SendMessage(string message)
        {
            // add to chat history
            await Clients.Caller.SendAsync("ReceiveMessage", CallerUserName, message);

            // processing by bot
            string replyMessage = message;

            // reply only to the sending user
            await Clients.Caller.SendAsync("ReceiveMessage", "VirtualDev", replyMessage);
        }
    }
}
