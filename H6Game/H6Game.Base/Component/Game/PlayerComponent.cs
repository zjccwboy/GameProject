using H6Game.Entitys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    [Event(EventType.Update | EventType.Dispose)]
    public class PlayerComponent : BaseComponent
    {
        public TAccount AccountEntity { get; private set; }

        public async void LoadAccountInfo(string objectId)
        {
            var rpository = Game.Scene.GetComponent<MongoDBComponent>().GetComponent<AccountRpositoryComponent>();
            this.AccountEntity = await rpository.GetAccountById(objectId);
            if(this.AccountEntity != null)
            {
                var actorComponent = Game.Scene.GetComponent<ActorComponent>();
                actorComponent.AddLocalEntity(new ActorInfoEntity
                {
                    ActorId = this.Id,
                    Id = this.AccountEntity.Id,
                });
            }
        }


        public override void Update()
        {
            base.Update();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
