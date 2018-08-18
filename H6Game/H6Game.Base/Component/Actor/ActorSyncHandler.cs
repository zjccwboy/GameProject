using H6Game.Message;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class ActorAddOrRemoveHandler : AActorHandler<ActorMessage>
    {
        protected override void Handler(Network network, ActorMessage message)
        {
            var component = Game.Scene.GetComponent<ActorComponent>();
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    var entity = new ActorInfoEntity
                    {
                        ActorId = network.RecvPacket.ActorId,
                        Network = network,
                        Id = message.ObjectId,
                    };
                    component.AddNetEntity(entity, network.Channel.Id);
                    break;

                case MessageCMD.RemoveActorCmd:
                    component.RemoveFromNet(message.ObjectId, network.Channel.Id);
                    break;
            }
        }
    }


    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class ActorSyncCallHandler : AActorHandler
    {
        protected override void Handler(Network network)
        {
            var component = Game.Scene.GetComponent<ActorComponent>();
            var entites = component.LocalEntitys;
            foreach (var entity in entites)
            {
                var syncMessage = new ActorMessage
                {
                    ObjectId = entity.Id,
                };
                network.RpcCallBack(syncMessage);
            }
        }
    }

    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class ActorSyncCallBackHandler : AActorHandler<ActorMessage>
    {
        protected override void Handler(Network network, ActorMessage message)
        {
            var component = Game.Scene.GetComponent<ActorComponent>();
            var entity = new ActorInfoEntity
            {
                ActorId = network.RecvPacket.ActorId,
                Network = network,
                Id = message.ObjectId,
            };
            component.AddNetEntity(entity, network.Channel.Id);
        }
    }
}
