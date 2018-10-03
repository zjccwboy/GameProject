using System;

namespace H6Game.Base
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ComponentEventAttribute : Attribute
    {
        public EventType EventType { get; }

        public ComponentEventAttribute(EventType eventType)
        {
            this.EventType = eventType;
        }
    }
}
