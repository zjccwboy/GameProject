using H6Game.Entities;
using H6Game.Entities.Enums;

namespace H6Game.Base
{

    public class GameComponent : BaseActorComponent<TGame>
    {
        public override ActorType ActorType => ActorType.Game;
    }
}
