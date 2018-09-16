using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;

namespace H6Game.Base
{
    public class PlayerComponent : BaseActorComponent<TAccount>
    {
        public override ActorType ActorType => ActorType.Player;
    }
}
