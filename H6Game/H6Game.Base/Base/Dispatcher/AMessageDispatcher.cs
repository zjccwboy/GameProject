using H6Game.Message;
using System;

namespace H6Game.Base
{
    public class Condition
    {
        public bool IsRpc;
        public bool IsCompress;
        public bool IsEncrypt;
    }

    public abstract class AMessageDispatcher<Response> : IDispatcher<Response>
    {
        public Session Session { get; private set; }
        public ANetChannel Channel { get; private set; }
        public Condition Conditions { get;} = new Condition();
        public int RpcId { get; private set; }
        public uint MessageId { get; private set; }

        public Type ResponseType
        {
            get
            {
                return typeof(Response);
            }
        }

        public virtual void Receive(Session session, ANetChannel channel, Packet packet)
        {
            this.Session = session;
            this.Channel = channel;
            this.Conditions.IsCompress = packet.IsCompress;
            this.Conditions.IsEncrypt = packet.IsEncrypt;
            this.Conditions.IsRpc = packet.IsRpc;
            this.MessageId = packet.MessageId;
            this.RpcId = packet.RpcId;
            if (DispatcherFactory.TryGetResponse(packet.MessageId, packet.Data, out Response response))
            {
                Dispatcher(response);
            }
            this.Session = null;
            this.Channel = null;
        }

        public abstract void Dispatcher(Response response);
    }
}
