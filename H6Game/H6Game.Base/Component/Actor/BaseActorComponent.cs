using H6Game.Entities;
using H6Game.Entities.Enums;
using H6Game.Message;

namespace H6Game.Base
{
    public abstract class BaseActorComponent<TEntity> : BaseActorEntityComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }

        public override void Dispose()
        {
            if (this.IsLocalActor)
            {
                Game.Actor.GetActorPool(this.ActorType).RemoveLocal(this.ActorEntity);
            }
            else
            {
                Game.Actor.GetActorPool(this.ActorType).RemoveRemote(this.ActorEntity);
            }

            this.EntityInfo = null;
            this.ActorEntity.ActorId = 0;
            this.ActorEntity.Id = null;
            this.ActorEntity.ActorInfo = null;
            this.ActorEntity.Network = null;

            base.Dispose();
        }

        public void AddLocal(TEntity entityInfo)
        {
            this.EntityInfo = entityInfo;
            this.ActorEntity.ActorId = this.Id;
            this.ActorEntity.Id = entityInfo.Id;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.ActorInfo = entityInfo;
            this.IsLocalActor = true;

            Game.Actor.GetActorPool(this.ActorType).AddLocal(this);
        }

        public void AddRemote(string objectId, Network network)
        {
            this.ActorEntity.ActorId = network.RecvPacket.ActorId;
            this.ActorEntity.Id = objectId;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.Network = network;
            this.IsLocalActor = false;
        }
    }

    public abstract class BaseActorEntityComponent : BaseComponent
    {
        public ActorEntity ActorEntity { get; } = new ActorEntity();
        public abstract ActorType ActorType { get;}
        public bool IsLocalActor { get; protected set; }
        public void SendLocalActorInfoMessage(Network network)
        {
            network.RecvPacket.ActorId = this.Id;
            var syncMessage = new ActorSyncMessage
            {
                ObjectId = this.ActorEntity.Id,
                ActorType = this.ActorEntity.ActorType,
            };
            network.RpcCallBack(syncMessage);
        }
    }
}
