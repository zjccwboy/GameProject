using System;

namespace H6Game.Base
{
    public class EventAttribute : Attribute
    {
        public EventType EventType { get; }

        public EventAttribute(EventType eventType)
        {
            this.EventType = eventType;
        }
    }
}
