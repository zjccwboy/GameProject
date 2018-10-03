using System;

namespace H6Game.Base
{
    public class PacketParserException : AggregateException
    {
        public PacketParserException(string message) : base(message) { }
    }
}
