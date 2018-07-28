using H6Game.Message;
using H6Game.Message.InNetMessage;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddOneServer)]
    public class DistributedDispatcher : ADispatcher<DistributedMessage>
    {
        protected override void Dispatcher(DistributedMessage response, int messageId)
        {
            SinglePool.Get<NetMapComponent>().Add(response);
            SinglePool.Get<NetMapComponent>().AddMaping(this.Channel, response);
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"新连接服务消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");

            var connections = SinglePool.Get<NetMapComponent>().ConnectEntities;
            //更新中心服务连接列表
            SinglePool.Get<InNetComponent>().BroadcastConnections(this.Session, connections, (int)MessageCMD.UpdateInNetonnections);
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{connections.ConvertToJson()}");
        }
    }

    [MessageCMD((int)MessageCMD.UpdateInNetonnections)]
    public class NetonnectionsDispatcher : ADispatcher<List<DistributedMessage>>
    {
        protected override void Dispatcher(List<DistributedMessage> response, int messageId)
        {
            //更新从服务连接列表
            SinglePool.Get<InNetComponent>().UpdateConnections(response);

            //更新从服务映射列表
            SinglePool.Get<NetMapComponent>().UpdateMapping(response);

            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/NetonnectionsDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");
        }
    }
}