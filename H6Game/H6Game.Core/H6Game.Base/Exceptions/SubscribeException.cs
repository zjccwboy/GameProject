using System;

namespace H6Game.Base.Exceptions
{
    public class SubscribeException : AggregateException
    {
        public SubscribeException(string message) : base(message) { }
    }
}
