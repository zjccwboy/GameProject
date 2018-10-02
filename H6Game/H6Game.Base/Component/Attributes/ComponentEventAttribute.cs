using System;

namespace H6Game.Base
{
    public class ComponentEventAttribute : Attribute
    {
        public EventType EventType { get; }

        public ComponentEventAttribute(EventType eventType)
        {
            this.EventType = eventType;
        }
    }
}
