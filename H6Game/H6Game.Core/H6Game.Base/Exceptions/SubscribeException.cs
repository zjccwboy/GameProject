using System;

namespace H6Game.Base
{
    public class SubscribeException : AggregateException
    {
        public SubscribeException(string message) : base(message) { }
    }
}
