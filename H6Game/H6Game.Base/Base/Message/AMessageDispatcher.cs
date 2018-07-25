using H6Game.Message;

namespace H6Game.Base
{
    public abstract class AMessageDispatcher<Response> : IMessageDispatcher<Response> where Response : IResponse
    {
        public Session Session { get; private set; }
        public ANetChannel Channel { get; private set; }
        public Condition Conditions { get;} = new Condition();
        public int RpcId { get; private set; }
        public uint MessageId { get; private set; }

        public virtual void Receive(Session session, ANetChannel channel, Packet packet)
        {
            this.Session = session;
            this.Channel = channel;
            this.Conditions.IsCompress = packet.IsCompress;
            this.Conditions.IsEncrypt = packet.IsEncrypt;
            this.Conditions.IsRpc = packet.IsRpc;
            this.MessageId = packet.MessageId;
            this.RpcId = packet.RpcId;
            if (MessageDeserialize.TryGetMessage(packet, out Response response))
            {
                Dispatcher(response);
            }
            else
            {
                Dispatcher(packet.Data);
            }
        }

        public abstract void Dispatcher(Response response);
        public abstract void Dispatcher(byte[] bytes);
    }

    public class Condition
    {
        public bool IsRpc;
        public bool IsCompress;
        public bool IsEncrypt;
    }
}
