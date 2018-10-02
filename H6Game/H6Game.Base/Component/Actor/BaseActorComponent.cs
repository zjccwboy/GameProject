
using System.Threading.Tasks;

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
    }

    public static class ActorHelper
    {
        public static void SendMySelf(this BaseActorEntityComponent actor, Network network, int messageCmd)
        {
            var syncMessage = new ActorSyncMessage
            {
                ObjectId = actor.ActorEntity.Id,
                ActorType = actor.ActorEntity.ActorType,
            };
            network.SendActor(syncMessage, messageCmd, actor.Id);
        }

        public static void SendActor<TSender>(this BaseActorEntityComponent actor, TSender data, int messageCmd) where TSender : class
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        public static async Task<CallResult<TResponse>> CallActor<TRequest, TResponse>(this BaseActorEntityComponent actor, TRequest request, int messageCmd) where TRequest : class
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TRequest, TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }
    }
}
