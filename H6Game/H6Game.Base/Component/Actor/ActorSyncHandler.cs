using H6Game.Entities.Enums;
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
            var actorType = message.ActorType;
            switch (actorType)
            {
                case ActorType.Player:
                    using(var component = Game.Scene.GetComponent<PlayerComponent>())
                    {
                        if (cmd == MessageCMD.AddActorCmd)
                        {
                            component.AddRemote(message.ObjectId, network);
                        }
                        else if (cmd == MessageCMD.RemoveActorCmd)
                        {
                            component.Remove(message.ObjectId);
                        }
                    }
                    break;
                case ActorType.Room:
                    using (var component = Game.Scene.GetComponent<RoomComponent>())
                    {
                        if (cmd == MessageCMD.AddActorCmd)
                        {
                            component.AddRemote(message.ObjectId, network);
                        }
                        else if (cmd == MessageCMD.RemoveActorCmd)
                        {
                            component.Remove(message.ObjectId);
                        }
                    }
                    break;
                case ActorType.Game:
                    using (var component = Game.Scene.GetComponent<GameComponent>())
                    {
                        if (cmd == MessageCMD.AddActorCmd)
                        {
                            component.AddRemote(message.ObjectId, network);
                        }
                        else if (cmd == MessageCMD.RemoveActorCmd)
                        {
                            component.Remove(message.ObjectId);
                        }
                    }
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
                        ActorType = entity.ActorType,
                    };
                    network.RpcCallBack(syncMessage);
                    count++;
                    if (count >= 100)
                    {
                        network.Channel.StartSend();
                        count = 0;
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

            var entity = new ActorEntity
            {
                ActorId = network.RecvPacket.ActorId,
                Network = network,
                Id = message.ObjectId,
                ActorType = message.ActorType,
            };
            Game.Actor.AddRemoteActor(entity);
        }
    }
}
