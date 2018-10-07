using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{

    public class GameComponent : BaseActorComponent<TGame>
    {
        public override ActorType ActorType => ActorType.Game;
    }
}
