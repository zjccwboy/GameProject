using H6Game.Message;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class ActorAddOrRemoveHandler : AActorHandler<ActorSyncMessage>
    {
        protected override void Handler(Network network, ActorSyncMessage message)
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
    public class SyncCallHandler : AHandler
    {
        protected override void Handler(Network network)
        {
            var component = Game.Scene.GetComponent<ActorComponent>();
            var entites = component.LocalEntitys;
            var count = 0;
            foreach (var entity in entites)
            {
                var syncMessage = new ActorSyncMessage
                {
                    ObjectId = entity.Id,
                };
                network.RpcCallBack(syncMessage);
                count++;
                if (count >= 100)
                {
                    count = 0;
                    network.Session.Update();
                }
            }
        }
    }

    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class SyncCallBackHandler : AHandler<ActorSyncMessage>
    {
        protected override void Handler(Network network, ActorSyncMessage message)
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
