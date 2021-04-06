using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace VirtualDev.Action.ActionServer
{
    public static class Extensions
    {
        public static void AddRasaActionServer(this IServiceCollection services, Action<RasaActionServerOptions> rasaServerOptions)
        {
            services.Configure(rasaServerOptions);
            services.AddSingleton<IActionHandlerTypeStore, ActionHandlerTypeStore>();
        }

        public static void UseRasaActionServer(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<RasaActionServerMiddleware>();
        }
    }
}
