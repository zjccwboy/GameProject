using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Base;

namespace H6Game.BaseTest
{
    [NetMessageType(NetMessageTypeTest.TesterMessage)]
    [ProtoBuf.ProtoContract]
    public class TesterMessage : IMessage
    {
        [ProtoBuf.ProtoMember(0)]
        public string Message { get; set; }

        [ProtoBuf.ProtoMember(1)]
        public int TestId { get; set; }
    }
}
