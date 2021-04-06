using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using VirtualDev.Action.ActionServer.Models;

namespace VirtualDev.Action.ActionServer
{
    internal class RasaActionServerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RasaActionServerMiddleware> logger;
        private readonly IServiceProvider serviceProvider;

        public RasaActionServerMiddleware(RequestDelegate next, ILogger<RasaActionServerMiddleware> logger, IServiceProvider serviceProvider)
        {
            this.next = next;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext httpContext, IOptions<RasaActionServerOptions> options, IActionHandlerTypeStore actionHandlerTypeStore)
        {
            if (httpContext.Request.Path == options.Value.WebhookUrl)
            {
                try
                {
                    logger.LogDebug("Incoming Rasa action detected");
                    var actionRequest = await httpContext.Request.ReadFromJsonAsync<RasaActionInput>();

                    var actionHandlerType = actionHandlerTypeStore.GetHandlerTypeForAction(actionRequest.NextAction);
                    if (actionHandlerType != null)
                    {
                        var actionHandler = (IAsyncActionHandler)ActivatorUtilities.CreateInstance(serviceProvider, actionHandlerType);
                        var actionResponse = await actionHandler.HandleAsync(actionRequest);
                        await httpContext.Response.WriteAsJsonAsync(actionResponse);
                    }
                    else
                    {
                        logger.LogError("Unable to locate action handler for action '{ActionName}'", actionRequest.NextAction);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                await next.Invoke(httpContext);
            }
        }
    }
}