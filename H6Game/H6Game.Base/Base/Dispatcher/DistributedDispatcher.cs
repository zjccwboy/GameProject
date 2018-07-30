using H6Game.Message;
using System;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddInServer, (int)MessageCMD.AddOutServer, (int)MessageCMD.DeleteServer, (int)MessageCMD.DeleteOutServer)]
    public class DistributedDispatcher : ADispatcher<NetEndPointMessage>
    {
        protected override void Dispatcher(NetEndPointMessage response, int messageId)
        {
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"新连接服务消息:{(MessageCMD)this.MessageId} 消息内容:{response.ConvertToJson()}");

            var inNetComponent = SinglePool.Get<InNetComponent>();
            List<NetEndPointMessage> connections = null;
            if (messageId == (int)MessageCMD.AddInServer)
            {
                if (inNetComponent.InNetMapManager.Existed(this.Channel, response))
                    return;

                inNetComponent.InNetMapManager.Add(response);
                inNetComponent.InNetMapManager.AddChannelMaping(this.Channel, response);

                connections = inNetComponent.InNetMapManager.ConnectEntities;
                //广播更新内网监听连接映射表
                inNetComponent.BroadcastConnections(connections, (int)MessageCMD.UpdateInNetConnections);
                LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{(MessageCMD)(int)MessageCMD.UpdateInNetConnections} 消息内容:{connections.ConvertToJson()}");
            }
            else if(messageId == (int)MessageCMD.AddOutServer)
            {
                if (inNetComponent.OutNetMapManager.Existed(this.Channel, response))
                    return;

                inNetComponent.OutNetMapManager.Add(response);
                inNetComponent.OutNetMapManager.AddChannelMaping(this.Channel, response);

                connections = inNetComponent.OutNetMapManager.ConnectEntities;
                //广播更新外网监听连接映射表
                inNetComponent.BroadcastConnections(connections, (int)MessageCMD.UpdateOutNetConnections);
                LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{(MessageCMD)(int)MessageCMD.UpdateOutNetConnections} 消息内容:{connections.ConvertToJson()}");

            }
            else if(messageId == (int)MessageCMD.DeleteServer)
            {
                if (!inNetComponent.InNetMapManager.Existed(this.Channel, response))
                    return;

                inNetComponent.InNetMapManager.Remove(response);
                connections = inNetComponent.InNetMapManager.ConnectEntities;
                //广播更新内网监听连接映射表
                inNetComponent.BroadcastConnections(connections, (int)MessageCMD.UpdateInNetConnections);
                LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{(MessageCMD)(int)MessageCMD.UpdateInNetConnections} 消息内容:{connections.ConvertToJson()}");
            }
            else if(messageId == (int)MessageCMD.DeleteOutServer)
            {
                if (!inNetComponent.OutNetMapManager.Existed(this.Channel, response))
                    return;

                inNetComponent.OutNetMapManager.Remove(response);
                connections = inNetComponent.OutNetMapManager.ConnectEntities;
                //广播更新外网监听连接映射表
                inNetComponent.BroadcastConnections(connections, (int)MessageCMD.UpdateOutNetConnections);
                LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{(MessageCMD)(int)MessageCMD.UpdateOutNetConnections} 消息内容:{connections.ConvertToJson()}");
            }
            else
            {
                throw new Exception("内网分布式消息分发错误.");
            }
        }
    }

    [MessageCMD((int)MessageCMD.UpdateInNetConnections, (int)MessageCMD.UpdateOutNetConnections)]
    public class NetonnectionsDispatcher : ADispatcher<List<NetEndPointMessage>>
    {
        protected override void Dispatcher(List<NetEndPointMessage> response, int messageId)
        {
            var inNetComponent = SinglePool.Get<InNetComponent>();
            if(messageId == (int)MessageCMD.UpdateInNetConnections)
            {
                //更新从服务连接列表
                inNetComponent.UpdateConnections(response);

                //更新内网监听映射列表
                inNetComponent.InNetMapManager.UpdateMapping(response);
            }
            else if(messageId == (int)MessageCMD.UpdateOutNetConnections)
            {
                //更新外网监听映射列表
                inNetComponent.OutNetMapManager.UpdateMapping(response);
            }
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/NetonnectionsDispatcher", $"分布式分发消息:{(MessageCMD)messageId} 消息内容:{response.ConvertToJson()}");
        }
    }
}