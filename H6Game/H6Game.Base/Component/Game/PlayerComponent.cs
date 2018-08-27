using H6Game.Entitys;


namespace H6Game.Base
{
    [Event(EventType.Dispose)]
    public class PlayerComponent : BaseComponent
    {
        public TAccount AccountInfo { get; private set; }

        public void Add(TAccount accountInfo)
        {
            this.AccountInfo = accountInfo;
            Game.Actor.AddLocalAcotr(new ActorInfoEntity
            {
                ActorId = this.Id,
                Id = this.AccountInfo.Id,
                ActorType = ActorType.Player,
            });
        }
    }
}
