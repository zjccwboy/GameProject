using H6Game.Message;
using MongoDB.Bson;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddInServerCmd)]
    public class DistributedHandler : AHandler<NetEndPointMessage>
    {
        protected override void Handler(Network network, NetEndPointMessage message)
        {          
            var inNetComponent = Game.Scene.GetComponent<InNetComponent>();

            if (inNetComponent.InNetMapManager.Existed(message))
                return;

            inNetComponent.AddSession(message);
            if (inNetComponent.IsCenterServer)
            {
                inNetComponent.InNetMapManager.Add(network.Channel, message);

                network.Broadcast(message, (int)MessageCMD.AddInServerCmd);
                this.Log(LogLevel.Debug, "Handler", $"广播分布式连接消息:{MessageCMD.AddInServerCmd} 消息内容:{message.ToJson()}");

                foreach(var entity in inNetComponent.InNetMapManager.Entities)
                {
                    network.RpcCallBack(entity);
                }
            }
        }
    }

    [HandlerCMD(MessageCMD.GetOutServerCmd)]
    public class OutNetMessageSync : AHandler
    {
        protected override void Handler(Network network)
        {
            var inNetComponent = Game.Scene.GetComponent<InNetComponent>();
            network.RpcCallBack(inNetComponent.OutNetMessage);
            this.Log(LogLevel.Debug, "Handler", $"回发外网连接信息:{inNetComponent.OutNetMessage.ToJson()}");
        }
    }

    [HandlerCMD(MessageCMD.GetInServerCmd)]
    public class InNetMessageSync : AHandler
    {
        protected override void Handler(Network network)
        {
            var inNetComponent = Game.Scene.GetComponent<InNetComponent>();
            network.RpcCallBack(inNetComponent.InNetMessage);
            this.Log(LogLevel.Debug, "Handler", $"回发内网连接信息:{inNetComponent.OutNetMessage.ToJson()}");
        }
    }

}