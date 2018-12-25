

namespace H6Game.Base.Message
{
    public interface IActorMessage : IMessage
    {
        int ActorId { get; set; }
    }

    public interface IMessage {}
}