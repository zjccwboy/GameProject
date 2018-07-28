using H6Game.Message;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddOneServer)]
    public class DistributedDispatcher : ADispatcher<NetEndPointMessage>
    {
        protected override void Dispatcher(NetEndPointMessage response, int messageId)
        {
            SinglePool.Get<InNetMapComponent>().Add(response);
            SinglePool.Get<InNetMapComponent>().AddMaping(this.Channel, response);
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"新连接服务消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");

            var connections = SinglePool.Get<InNetMapComponent>().ConnectEntities;
            //更新中心服务连接列表
            SinglePool.Get<InNetComponent>().BroadcastConnections(this.Session, connections, (int)MessageCMD.UpdateInNetConnections);
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{connections.ConvertToJson()}");
        }
    }

    [MessageCMD((int)MessageCMD.UpdateInNetConnections)]
    public class NetonnectionsDispatcher : ADispatcher<List<NetEndPointMessage>>
    {
        protected override void Dispatcher(List<NetEndPointMessage> response, int messageId)
        {
            //更新从服务连接列表
            SinglePool.Get<InNetComponent>().UpdateConnections(response);

            //更新从服务映射列表
            SinglePool.Get<InNetMapComponent>().UpdateMapping(response);

            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/NetonnectionsDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");
        }
    }
}