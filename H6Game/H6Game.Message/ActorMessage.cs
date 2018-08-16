using H6Game.Entitys;
using ProtoBuf;

namespace H6Game.Message
{

    [MessageType(MessageType.PlayerActorMessage)]
    public class PlayerActorMessage : ActorMessage
    {
        public PlayerType PlayerType { get; set; }
    }

    [MessageType(MessageType.RoomActorMessage)]
    public class RoomActorMessage : ActorMessage
    {
        public RoomType RoomType { get; set; }
    }

    [MessageType(MessageType.SceneActorMessage)]
    public class SceneActorMessage : ActorMessage
    {
        public SceneType SceneType { get; set; }
    }

    [MessageType(MessageType.GameActorMessage)]
    public class GameActorMessage : ActorMessage
    {
        public GameType GameType { get; set; }
    }

    [MessageType(MessageType.None)]
    public abstract class ActorMessage : IMessage
    {

        [ProtoMember(1)]
        public string Id { get; set; }
    }
}
