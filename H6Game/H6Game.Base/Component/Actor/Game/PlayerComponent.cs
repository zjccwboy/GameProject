using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{
    public class PlayerComponent : BaseActorComponent<TAccount>
    {
        public override ActorType ActorType => ActorType.Player;
    }
}
