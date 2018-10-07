using H6Game.Base;

namespace H6Game.Actor
{
    public abstract class BaseActorComponent<TEntity> : BaseActorComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }
        public ActorPoolComponent ActorPool { get; } = Game.Scene.GetComponent<ActorPoolComponent>();

        public override void Dispose()
        {
            if (IsLocalActor)
                ActorPool.RemoveLocal(this);
            else
                ActorPool.RemoveRemote(this);

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

            this.ActorPool.AddLocal(this);
        }

        public override void SetRemote(Network network, string objectId, int actorId)
        {
            this.ActorEntity.ActorId = actorId;
            this.ActorEntity.Id = objectId;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.Network = network;

            this.ActorPool.AddRemote(this);
        }
    }

    public abstract class BaseActorComponent : BaseActorEntity
    {
        /// <summary>
        /// 该组件拥有的成员，例如用户进入了房间，那么房间组件就拥有了该用户，设计时需要理解清楚需求的
        /// 主从关系，要不会造成逻辑混乱。
        /// </summary>
        public ComponentEntity MemberEntities { get; } = new ActorMemberEntities();
        public ActorEntity ActorEntity { get; } = new ActorEntity();
        public bool IsLocalActor { get { return this.ActorEntity.ActorId == this.Id; } }
        public abstract ActorType ActorType { get;}
        public abstract void SetRemote(Network network, string objectId, int actorId);
    }

    public class ActorMemberEntities : ComponentEntity
    {

    }
}
