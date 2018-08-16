using H6Game.Entitys;
using ProtoBuf;

namespace H6Game.Message
{

    [MessageType(MessageType.PlayerActorMessage)]
    public class PlayerActorMessage : ActorMessage
    {

    }

    [MessageType(MessageType.RoomActorMessage)]
    public class RoomActorMessage : ActorMessage
    {

    }

    [MessageType(MessageType.SceneActorMessage)]
    public class SceneActorMessage : ActorMessage
    {

    }

    [MessageType(MessageType.GameActorMessage)]
    public class GameActorMessage : ActorMessage
    {

    }

    [MessageType(MessageType.None)]
    public abstract class ActorMessage : IMessage
    {

        [ProtoMember(1)]
        public string ObjectId { get; set; }
    }
}
