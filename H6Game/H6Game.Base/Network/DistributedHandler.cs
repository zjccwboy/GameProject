using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [HandlerCMD(MessageCMD.AddInServer)]
    public class DistributedHandler : AHandler<NetEndPointMessage>
    {
        protected override void Handler(NetEndPointMessage message)
        {          
            var inNetComponent = SinglePool.Get<InNetComponent>();

            if (inNetComponent.InNetMapManager.Existed(message))
                return;

            inNetComponent.AddSession(message);
            if (inNetComponent.IsCenterServer)
            {
                this.BroadcastConnection(message, (int)MessageCMD.AddInServer);
                this.Log(LogLevel.Debug, "DistributedDispatcher", $"广播分布式连接消息:{MessageCMD.AddInServer} 消息内容:{message.ToJson()}");
            }
        }

        private void BroadcastConnection(NetEndPointMessage message, int messageCmd)
        {
            this.Session.Broadcast(message, messageCmd);
        }
    }

    [HandlerCMD(MessageCMD.GetOutServer)]
    public class OutNetMessageSync : AHandler<string>
    {
        protected override void Handler(string message)
        {
            var inNetComponent = SinglePool.Get<InNetComponent>();
            this.Session.Send(this.Channel, message, this.Packet.MessageId, this.Packet.RpcId);
            this.Log(LogLevel.Debug, "{OutNetMessageTrans", $"回发外网连接信息:{inNetComponent.OutNetMessage.ToJson()}");
        }
    }

}