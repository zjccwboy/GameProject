using H6Game.Base;
using H6Game.Hotfix.Messages;
using H6Game.Hotfix.Messages.Enums;


namespace H6Game.TestKcpServerBenckmarkCore
{
    [SubscriberCMD(1024)]
    public class TestSubscriber : AMsgSubscriber<TestMessage>
    {
        protected override void Subscribe(Network network, TestMessage message, int messageCmd)
        {
            network.Response(message);
        }
    }

    [SubscriberCMD(1024)]
    public class TestSubscriberInt : AMsgSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, int messageCmd)
        {
            network.Response(message);
        }
    }

    [ProtoBuf.ProtoContract]
    [MessageType(OutMessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
