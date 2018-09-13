using System;

namespace H6Game.Base
{
    public class ActorException : AggregateException
    {
        public ActorException(string message) : base(message) { }
    }
}
