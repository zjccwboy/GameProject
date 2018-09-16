using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;

namespace H6Game.Base
{

    public class GameComponent : BaseActorComponent<TGame>
    {
        public override ActorType ActorType => ActorType.Game;
    }
}
