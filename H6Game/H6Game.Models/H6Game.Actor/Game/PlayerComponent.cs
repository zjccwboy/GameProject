

using H6Game.Hotfix.Entities;

namespace H6Game.Actor
{
    public class PlayerComponent : BaseActorComponent<TAccount>
    {
        public override ActorType ActorType => ActorType.Player;
    }
}
