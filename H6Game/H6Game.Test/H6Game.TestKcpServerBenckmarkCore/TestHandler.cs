﻿using H6Game.Base;
using H6Game.Hotfix.Messages;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;


namespace H6Game.TestKcpServerBenckmarkCore
{
    [HandlerCMD(1024)]
    public class TestHandler : AMessageHandler<TestMessage>
    {
        protected override void Handler(Network network, TestMessage message)
        {
            network.Response(message);
        }
    }

    [HandlerCMD(1024)]
    public class TestHandlerInt : AMessageHandler<int>
    {
        protected override void Handler(Network network, int message)
        {
            network.Response(message);
        }
    }

    [ProtoBuf.ProtoContract]
    [MessageType(MessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
