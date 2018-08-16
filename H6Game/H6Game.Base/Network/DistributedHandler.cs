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
                network.Broadcast(message, (int)MessageCMD.AddInServerCmd);
                this.Log(LogLevel.Debug, "Handler", $"广播分布式连接消息:{MessageCMD.AddInServerCmd} 消息内容:{message.ToJson()}");
            }
        }
    }

    [HandlerCMD(MessageCMD.GetOutServerCmd)]
    public class OutNetMessageSync : AHandler<string>
    {
        protected override void Handler(Network network, string message)
        {
            var inNetComponent = Game.Scene.GetComponent<InNetComponent>();
            network.RpcCallBack(inNetComponent.OutNetMessage);
            this.Log(LogLevel.Debug, "Handler", $"回发外网连接信息:{inNetComponent.OutNetMessage.ToJson()}");
        }
    }

    [HandlerCMD(MessageCMD.GetInServerCmd)]
    public class InNetMessageSync : AHandler<string>
    {
        protected override void Handler(Network network, string message)
        {
            var inNetComponent = Game.Scene.GetComponent<InNetComponent>();
            network.RpcCallBack(inNetComponent.InNetMessage);
            this.Log(LogLevel.Debug, "Handler", $"回发内网连接信息:{inNetComponent.OutNetMessage.ToJson()}");
        }
    }

}