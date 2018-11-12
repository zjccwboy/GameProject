using H6Game.Base;
using H6Game.Hotfix.Enums;


namespace H6Game.TestKcpServerBenckmarkCore
{
    [NetCommand(1024)]
    public class TestSubscriber : NetSubscriber<TestMessage>
    {
        protected override void Subscribe(TestMessage message, int command, int rpcId)
        {
            throw new System.NotImplementedException();
        }

        protected override void Subscribe(Network network, TestMessage message, int messageCmd)
        {
            network.Response(message);
        }
    }

    [NetCommand(1024)]
    public class TestSubscriberInt : NetSubscriber<int>
    {
        protected override void Subscribe(int message, int command, int rpcId)
        {
            throw new System.NotImplementedException();
        }

        protected override void Subscribe(Network network, int message, int messageCmd)
        {
            network.Response(message);
        }
    }

    [ProtoBuf.ProtoContract]
    [NetMessageType(NetMessageType.TestGServerTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int Actor { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
    }
}
