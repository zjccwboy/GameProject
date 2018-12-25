using System;

namespace H6Game.Base.Exceptions
{
    public class NetworkException : AggregateException
    {
        public NetworkException(string message) : base(message) { }

        public NetworkException(string message, Exception exception) : base(message, exception) { }
    }
}
