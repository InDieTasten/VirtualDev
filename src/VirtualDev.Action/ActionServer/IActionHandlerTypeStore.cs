using System;

namespace VirtualDev.Action.ActionServer
{
    public interface IActionHandlerTypeStore
    {
        Type GetHandlerTypeForAction(string actionName);
    }
}