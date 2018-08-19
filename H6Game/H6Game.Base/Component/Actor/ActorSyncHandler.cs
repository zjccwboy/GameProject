using H6Game.Message;
using MongoDB.Bson;

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
            //this.Log(LogLevel.Debug, "SyncCallHandler", 
            //    $"MessageId:{network.RecvPacket.MessageId} " +
            //    $"RpcId:{network.RecvPacket.RpcId} " +
            //    $"ActorId:{network.RecvPacket.ActorId} " +
            //    $"MsgTypeCode:{network.RecvPacket.MsgTypeCode} ");

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
            //this.Log(LogLevel.Debug, "SyncCallBackHandler", 
            //    $"ActorSyncMessage:{message.ToJson()} " +
            //    $"MessageId:{network.RecvPacket.MessageId} " +
            //    $"RpcId:{network.RecvPacket.RpcId} " +
            //    $"ActorId:{network.RecvPacket.ActorId} " +
            //    $"MsgTypeCode:{network.RecvPacket.MsgTypeCode} ");

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
