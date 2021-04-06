using System.Threading.Tasks;
using VirtualDev.Action.ActionServer.Models;

namespace VirtualDev.Action.ActionServer
{
    public interface IAsyncActionHandler
    {
        Task<RasaActionOutput> HandleAsync(RasaActionInput actionRequest);
    }
}
