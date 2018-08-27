using H6Game.Entitys;


namespace H6Game.Base
{
    [Event(EventType.Dispose)]
    public class PlayerComponent : BaseComponent
    {
        public TAccount AccountInfo { get; private set; }

        public async void LoadAccountById(string objectId)
        {
            var rpository = Game.Scene.GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountById(objectId);
            if(this.AccountInfo != null)
            {
                Game.Actor.AddLocalAcotr(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                    ActorType = ActorType.Player,
                });
            }
        }

        public async void LoadAccountByOpenId(string openId)
        {
            var rpository = Game.Scene.GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountByOpenId(openId);
            if (this.AccountInfo != null)
            {
                Game.Actor.AddLocalAcotr(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                    ActorType = ActorType.Player,
                });
            }
        }

        public async void LoadAccountByPhoneNumber(string phoneNumber)
        {
            var rpository = Game.Scene.GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountByPhoneNumber(phoneNumber);
            if (this.AccountInfo != null)
            {
                Game.Actor.AddLocalAcotr(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                    ActorType = ActorType.Player,
                });
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
