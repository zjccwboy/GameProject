

namespace H6Game.Base
{
    public interface IActorMessage : IMessage
    {
        int ActorId { get; set; }
    }
    public interface IMessage {}
}