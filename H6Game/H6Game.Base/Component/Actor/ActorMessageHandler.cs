using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class ActorMessageHandler : AActorHandler<ActorMessage>
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
                    component.AddNetEntity(entity);
                    break;

                case MessageCMD.RemoveActorCmd:
                    component.RemoveFromNet(message.ObjectId);
                    break;
            }
        }
    }
}
