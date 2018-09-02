using H6Game.Message;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddInServerCmd)]
    public class DistributedHandler : AHandler<NetEndPointMessage>
    {
        protected override void Handler(Network network, NetEndPointMessage message)
        {
            //this.Log(LogLevel.Debug, "Handler", $"收到:{network.Channel.RemoteEndPoint} 消息CMD:{(int)MessageCMD.AddInServerCmd} 内容:{message.ToJson()}");

            var inNetComponent = Game.Scene.GetComponent<InnerComponent>();

            if (inNetComponent.InNetMapManager.Existed(message))
                return;

            inNetComponent.AddSession(message);
            if (inNetComponent.IsCenterServer)
            {
                inNetComponent.InNetMapManager.Add(network.Channel, message);

                network.Broadcast(message, (int)MessageCMD.AddInServerCmd);
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
            //this.Log(LogLevel.Debug, "OutNetMessageSync", $"收到:{network.Channel.RemoteEndPoint} 消息CMD:{(int)MessageCMD.AddInServerCmd}");
            var inNetComponent = Game.Scene.GetComponent<InnerComponent>();
            network.RpcCallBack(inNetComponent.OutNetMessage);
        }
    }

    [HandlerCMD(MessageCMD.GetInServerCmd)]
    public class InNetMessageSync : AHandler
    {
        protected override void Handler(Network network)
        {
            //this.Log(LogLevel.Debug, "InNetMessageSync", $"收到:{network.Channel.RemoteEndPoint} 消息CMD:{(int)MessageCMD.AddInServerCmd}");

            var inNetComponent = Game.Scene.GetComponent<InnerComponent>();
            network.RpcCallBack(inNetComponent.InNetMessage);
        }
    }

}