using System;

namespace VirtualDev.Action.ActionServer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RasaActionAttribute : Attribute
    {
        readonly string name;

        public RasaActionAttribute(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}