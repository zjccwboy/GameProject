using System;

namespace H6Game.Base.Exceptions
{
    public class ControllerException : AggregateException
    {
        public ControllerException(string message) : base(message) { }
    }
}
