using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddInServer, (int)MessageCMD.DeleteServer)]
    public class DistributedHandler : AHandler<NetEndPointMessage>
    {
        protected override void Dispatcher(NetEndPointMessage message, int messageId)
        {          
            var inNetComponent = SinglePool.Get<InNetComponent>();
            List<NetEndPointMessage> connections = null;
            if (messageId == (int)MessageCMD.AddInServer)
            {
                if (inNetComponent.InNetMapManager.Existed(message))
                    return;

                inNetComponent.AddSession(message);
                if (inNetComponent.IsCenterServer)
                {
                    this.BroadcastConnection(message, (int)MessageCMD.AddInServer);
                    LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{MessageCMD.AddInServer} 消息内容:{connections.ConvertToJson()}");
                }
            }
            else if(messageId == (int)MessageCMD.DeleteServer)
            {
                if (!inNetComponent.InNetMapManager.Existed(message))
                    return;

                inNetComponent.RemoveSession(message);
                inNetComponent.InNetMapManager.Remove(message);
                if (inNetComponent.IsCenterServer)
                {
                    this.BroadcastConnection(message, (int)MessageCMD.DeleteServer);
                    LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{MessageCMD.DeleteServer} 消息内容:{connections.ConvertToJson()}");
                }
            }
            else
            {
                throw new Exception("内网分布式消息分发错误.");
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

    [MessageCMD((int)MessageCMD.GetOutServer)]
    public class OutNetMessageSync : AHandler<string>
    {
        protected override void Dispatcher(string message, int messageId)
        {
            var inNetComponent = SinglePool.Get<InNetComponent>();
            this.CallBack(inNetComponent.OutNetMessage.ConvertToBytes());
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/OutNetMessageTrans", $"同步外网连接:{inNetComponent.OutNetMessage.ConvertToJson()}");
        }
    }

}