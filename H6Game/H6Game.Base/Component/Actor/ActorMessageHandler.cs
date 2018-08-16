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
        private ActorComponent Component { get; set; }
        protected override void Handler(Network network, ActorMessage message)
        {
            Component = Component ?? Game.Scene.GetComponent<ActorComponent>();
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
                    Component.AddNetEntity(entity);
                    break;

                case MessageCMD.RemoveActorCmd:
                    Component.RemoveFromNet(message.ObjectId);
                    break;
            }
        }
    }
}
