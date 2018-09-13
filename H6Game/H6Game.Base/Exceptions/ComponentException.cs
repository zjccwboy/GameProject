using System;

namespace H6Game.Base
{
    public class ComponentException : AggregateException
    {
        public ComponentException(string message) : base(message) { }
    }
}
