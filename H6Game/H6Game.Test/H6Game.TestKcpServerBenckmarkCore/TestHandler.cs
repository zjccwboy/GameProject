using H6Game.Base;
using H6Game.Hotfix.Messages;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Enums;


namespace H6Game.TestKcpServerBenckmarkCore
{
    [HandlerCMD(1024)]
    public class TestSubscriber : AMsgSubscriber<TestMessage>
    {
        protected override void Subscribe(Network network, TestMessage message)
        {
            network.Response(message);
        }
    }

    [HandlerCMD(1024)]
    public class TestSubscriberInt : AMsgSubscriber<int>
    {
        protected override void Subscribe(Network network, int message)
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
