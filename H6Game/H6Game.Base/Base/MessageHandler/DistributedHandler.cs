using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD(MessageCMD.AddInServer)]
    public class DistributedHandler : AHandler<NetEndPointMessage>
    {
        protected override void Dispatcher(NetEndPointMessage message, int messageId)
        {          
            var inNetComponent = SinglePool.Get<InNetComponent>();

            if (inNetComponent.InNetMapManager.Existed(message))
                return;

            inNetComponent.AddSession(message);
            if (inNetComponent.IsCenterServer)
            {
                this.BroadcastConnection(message, (int)MessageCMD.AddInServer);
                LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"广播分布式连接消息:{MessageCMD.AddInServer} 消息内容:{message.ConvertToJson()}");
            }
        }

        private void BroadcastConnection(NetEndPointMessage message, int messageCmd)
        {
            var bytes = message.ConvertToBytes();
            var packet = new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            };
            this.Session.Broadcast(packet);
        }
    }

    [MessageCMD(MessageCMD.GetOutServer)]
    public class OutNetMessageSync : AHandler<string>
    {
        protected override void Dispatcher(string message, int messageId)
        {
            var inNetComponent = SinglePool.Get<InNetComponent>();
            this.CallBack(inNetComponent.OutNetMessage.ConvertToBytes());
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/OutNetMessageTrans", $"回发外网连接信息:{inNetComponent.OutNetMessage.ConvertToJson()}");
        }
    }

}