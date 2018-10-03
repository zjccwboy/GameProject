

using H6Game.Base;

namespace H6Game.Actor
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

            Game.Scene.GetComponent<ActorPoolComponent>().AddLocal(this);
        }

        public override void SetRemote(string objectId, Network network)
        {
            this.ActorEntity.ActorId = network.RecvPacket.ActorId;
            this.ActorEntity.Id = objectId;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.Network = network;
            this.IsLocalActor = false;

            Game.Scene.GetComponent<ActorPoolComponent>().AddRemote(this);
        }
    }

    public abstract class BaseActorEntityComponent : BaseActorEntity
    {
        public ActorEntity ActorEntity { get; } = new ActorEntity();
        public bool IsLocalActor { get; protected set; }
        public abstract ActorType ActorType { get;}
        public abstract void SetRemote(string objectId, Network network);
    }
}
