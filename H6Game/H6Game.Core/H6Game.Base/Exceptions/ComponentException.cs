using System;

namespace H6Game.Base.Exceptions
{
    public class ComponentException : AggregateException
    {
        public ComponentException(string message) : base(message) { }
    }
}
