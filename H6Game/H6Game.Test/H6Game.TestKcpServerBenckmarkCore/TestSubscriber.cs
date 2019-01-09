using H6Game.Base;
using H6Game.Base.Message;
using H6Game.Hotfix.Enums;


namespace H6Game.TestKcpServerBenckmarkCore
{
    [NetCommand(1024)]
    public class TestSubscriber : NetSubscriber<TestMessage>
    {
        protected override void Subscribe(Network network, TestMessage message, ushort messageCmd)
        {
            network.Response(message);
        }
    }

    [NetCommand(1024)]
    public class TestSubscriberInt : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, ushort messageCmd)
        {
            network.Response(message);
        }
    }

    [ProtoBuf.ProtoContract]
    [NetMessageType(MessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
