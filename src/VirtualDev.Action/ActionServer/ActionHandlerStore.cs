using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualDev.Action.ActionServer
{
    public class ActionHandlerTypeStore : IActionHandlerTypeStore
    {
        private readonly ILogger<ActionHandlerTypeStore> logger;
        private Dictionary<string, Type> registeredActionHandlers = new Dictionary<string, Type>();

        public ActionHandlerTypeStore(ILogger<ActionHandlerTypeStore> logger)
        {
            this.logger = logger;
            RegisterActionHandlersViaReflection();
        }

        public Type GetHandlerTypeForAction(string actionName)
        {
            registeredActionHandlers.TryGetValue(actionName, out Type result);
            return result;
        }

        private void RegisterActionHandlersViaReflection()
        {
            var actionHandlerType = typeof(IAsyncActionHandler);
            var implementingTypes = Thread.GetDomain()
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => actionHandlerType.IsAssignableFrom(type));

            var namedHandlers = implementingTypes.Select(type => new
            {
                ActionName = type.GetCustomAttributes(typeof(RasaActionAttribute), false).Cast<RasaActionAttribute>().SingleOrDefault()?.GetName(),
                Type = type
            }).Where(kvp => kvp.ActionName != null).ToArray();

            foreach (var namedHandler in namedHandlers)
            {
                if (registeredActionHandlers.TryAdd(namedHandler.ActionName, namedHandler.Type))
                {
                    logger.LogInformation("Rasa action registration '{ActionName}' will be handled by {ActionHandler}", namedHandler.ActionName, namedHandler.Type.FullName);
                }
                else
                {
                    logger.LogError("Collision of action names occurred. Action name '{ActionName}' was already registered", namedHandler.ActionName);
                }
            }
        }
    }
}
