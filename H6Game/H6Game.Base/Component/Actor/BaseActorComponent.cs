using H6Game.Hotfix.Entities;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Inner;

namespace H6Game.Base
{
    public abstract class BaseActorComponent<TEntity> : BaseActorEntityComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }

        public override void Dispose()
        {
            this.EntityInfo = null;
            this.ActorEntity.Id = null;
            this.ActorEntity.ActorInfo = null;
            this.ActorEntity.Network = null;

            base.Dispose();
        }

        public void SetLocal(TEntity entityInfo)
        {
            this.ActorEntity.ActorId = this.Id;
            this.EntityInfo = entityInfo;
            this.ActorEntity.Id = entityInfo.Id;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.ActorInfo = entityInfo;
            this.IsLocalActor = true;
        }

        public void SetRemote(string objectId, Network network)
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

        public void SendMySelf(Network network, int messageCmd)
        {
            var syncMessage = new ActorSyncMessage
            {
                ObjectId = this.ActorEntity.Id,
                ActorType = this.ActorEntity.ActorType,
            };
            network.SendActor(syncMessage, messageCmd, this.Id);
        }
    }
}
