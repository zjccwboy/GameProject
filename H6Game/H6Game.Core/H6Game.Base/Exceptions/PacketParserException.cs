using System;

namespace H6Game.Base.Exceptions
{
    public class PacketParserException : AggregateException
    {
        public PacketParserException(string message) : base(message) { }
    }
}
