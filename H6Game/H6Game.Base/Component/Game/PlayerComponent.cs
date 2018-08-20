using H6Game.Entitys;


namespace H6Game.Base
{
    [Event(EventType.Dispose)]
    public class PlayerComponent : BaseComponent
    {
        public TAccount AccountInfo { get; private set; }

        public async void LoadAccountById(string objectId)
        {
            var rpository = Game.Scene.GetComponent<MongoDBComponent>().GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountById(objectId);
            if(this.AccountInfo != null)
            {
                var actorComponent = Game.Scene.GetComponent<ActorComponent>();
                actorComponent.AddLocalEntity(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                });
            }
        }

        public async void LoadAccountByOpenId(string openId)
        {
            var rpository = Game.Scene.GetComponent<MongoDBComponent>().GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountByOpenId(openId);
            if (this.AccountInfo != null)
            {
                var actorComponent = Game.Scene.GetComponent<ActorComponent>();
                actorComponent.AddLocalEntity(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                });
            }
        }

        public async void LoadAccountByPhoneNumber(string phoneNumber)
        {
            var rpository = Game.Scene.GetComponent<MongoDBComponent>().GetComponent<AccountRpositoryComponent>();
            this.AccountInfo = await rpository.GetAccountByPhoneNumber(phoneNumber);
            if (this.AccountInfo != null)
            {
                var actorComponent = Game.Scene.GetComponent<ActorComponent>();
                actorComponent.AddLocalEntity(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountInfo.Id,
                });
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
