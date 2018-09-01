using H6Game.Message;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class ActorAddOrRemoveHandler : AActorHandler<ActorSyncMessage>
    {
        protected override void Handler(Network network, ActorSyncMessage message)
        {
            //this.Log(LogLevel.Debug, "ActorAddOrRemoveHandler",
            //    $"ActorSyncMessage:{message.ToJson()} " +
            //    $"MessageId:{network.RecvPacket.MessageId} " +
            //    $"RpcId:{network.RecvPacket.RpcId} " +
            //    $"ActorId:{network.RecvPacket.ActorId} " +
            //    $"MsgTypeCode:{network.RecvPacket.MsgTypeCode} ");

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
                    Game.Actor.AddRemoteActor(entity);
                    break;

                case MessageCMD.RemoveActorCmd:
                    Game.Actor.RemoveActor(message.ActorType, message.ObjectId);
                    break;
            }
        }
    }


    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class SyncCallHandler : AHandler
    {
        protected override void Handler(Network network)
        {
            //this.Log(LogLevel.Debug, "SyncCallHandler", 
            //    $"MessageId:{network.RecvPacket.MessageId} " +
            //    $"RpcId:{network.RecvPacket.RpcId} " +
            //    $"ActorId:{network.RecvPacket.ActorId} " +
            //    $"MsgTypeCode:{network.RecvPacket.MsgTypeCode} ");

            var components = Game.Scene.GetComponents<ActorComponent>();
            foreach(var component in components)
            {
                var actorComponent = component as ActorComponent;
                var entites = actorComponent.LocalEntitys;
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
    }

    [HandlerCMD(MessageCMD.SyncActorInfoCmd)]
    public class SyncCallBackHandler : AHandler<ActorSyncMessage>
    {
        protected override void Handler(Network network, ActorSyncMessage message)
        {
            //this.Log(LogLevel.Debug, "SyncCallBackHandler", 
            //    $"ActorSyncMessage:{message.ToJson()} " +
            //    $"MessageId:{network.RecvPacket.MessageId} " +
            //    $"RpcId:{network.RecvPacket.RpcId} " +
            //    $"ActorId:{network.RecvPacket.ActorId} " +
            //    $"MsgTypeCode:{network.RecvPacket.MsgTypeCode} ");

            var entity = new ActorInfoEntity
            {
                ActorId = network.RecvPacket.ActorId,
                Network = network,
                Id = message.ObjectId,
            };
            Game.Actor.AddRemoteActor(entity);
        }
    }
}
