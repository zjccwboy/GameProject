using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Base;
using H6Game.Base.Message;

namespace H6Game.BaseTest
{
    [NetMessageType(NetMessageTypeTest.TesterMessage)]
    [ProtoBuf.ProtoContract]
    public class TesterMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public string Message { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public int TestId { get; set; }
    }
}
