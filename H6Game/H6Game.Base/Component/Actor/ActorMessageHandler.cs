using H6Game.Entitys;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class PlayerActorMessageHandler : AActorHandler<PlayerActorMessage>
    {
        protected override void Handler(Network network, PlayerActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class RoomActorMessageHandler : AActorHandler<RoomActorMessage>
    {
        protected override void Handler(Network network, RoomActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class SceneActorMessageHandler : AActorHandler<SceneActorMessage>
    {
        protected override void Handler(Network network, SceneActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.RemoveActorCmd)]
    public class GameActorMessageHandler : AActorHandler<GameActorMessage>
    {
        private GameActorComponent Component { get; set; } 
        protected override void Handler(Network network, GameActorMessage message)
        {
            Component = Component ?? Game.Scene.GetComponent<GameActorComponent>();
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
