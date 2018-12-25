using System;

namespace H6Game.Base.Exceptions
{
    public class ActorException : AggregateException
    {
        public ActorException(string message) : base(message) { }
    }
}
