using H6Game.Message;
using H6Game.Message.InNetMessage;
using System.Collections.Generic;

namespace H6Game.Base.Base.Message
{
    [MessageCMD((int)MessageCMD.AddOneServer, (int)MessageCMD.DeleteOneServer)]
    public class DistributedDispatcher : AMessageDispatcher<DistributedMessage>
    {
        protected override void Dispatcher(DistributedMessage response, int messageId)
        {
            switch ((MessageCMD)MessageId)
            {
                case MessageCMD.AddOneServer:
                    SinglePool.Get<NetMapComponent>().Add(response);
                    break;
                case MessageCMD.DeleteOneServer:
                    SinglePool.Get<NetMapComponent>().Remove(response);
                    break;
            }

            var connections = SinglePool.Get<NetMapComponent>().ConnectEntities;

            //更新中心服务连接列表
            SinglePool.Get<InNetComponent>().BroadcastConnections(this.Session, connections);
#if SERVER
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/DistributedDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");
#endif     
        }
    }

    [MessageCMD((int)MessageCMD.UpdateInNetonnections)]
    public class NetonnectionsDispatcher : AMessageDispatcher<List<DistributedMessage>>
    {
        protected override void Dispatcher(List<DistributedMessage> response, int messageId)
        {
            //更新从服务连接列表
            SinglePool.Get<InNetComponent>().UpdateConnections(response);

            //更新从服务映射列表
            SinglePool.Get<NetMapComponent>().UpdateMapping(response);
#if SERVER
            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/NetonnectionsDispatcher", $"分布式分发消息:{this.MessageId} 消息内容:{response.ConvertToJson()}");
#endif
        }
    }
}