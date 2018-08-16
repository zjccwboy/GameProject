using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.UpdateActorCmd, MessageCMD.RemoveActorCmd)]
    public class PlayerActorMessageHandler : AActorHandler<PlayerActorMessage>
    {
        protected override void Handler(Network network, PlayerActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.UpdateActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.UpdateActorCmd, MessageCMD.RemoveActorCmd)]
    public class RoomActorMessageHandler : AActorHandler<RoomActorMessage>
    {
        protected override void Handler(Network network, RoomActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.UpdateActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.UpdateActorCmd, MessageCMD.RemoveActorCmd)]
    public class SceneActorMessageHandler : AActorHandler<SceneActorMessage>
    {
        protected override void Handler(Network network, SceneActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.UpdateActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }

    [HandlerCMD(MessageCMD.AddActorCmd, MessageCMD.UpdateActorCmd, MessageCMD.RemoveActorCmd)]
    public class GameActorMessageHandler : AActorHandler<GameActorMessage>
    {
        protected override void Handler(Network network, GameActorMessage message)
        {
            var cmd = (MessageCMD)network.RecvPacket.MessageId;
            switch (cmd)
            {
                case MessageCMD.AddActorCmd:
                    break;
                case MessageCMD.UpdateActorCmd:
                    break;
                case MessageCMD.RemoveActorCmd:
                    break;
            }
        }
    }
}
