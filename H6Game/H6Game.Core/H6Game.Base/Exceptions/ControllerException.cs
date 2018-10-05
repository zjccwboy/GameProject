using System;

namespace H6Game.Base
{
    public class ControllerException : AggregateException
    {
        public ControllerException(string message) : base(message) { }
    }
}
