using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Actor
{
    public abstract class BaseActorComponent<TEntity> : BaseActorComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }
        private ActorComponentStorage ActorPool { get; } = Game.Scene.GetComponent<ActorComponentStorage>();

        public override void Dispose()
        {
            ActorPool.Remove(this);

            this.EntityInfo = null;
            this.ActorEntity.Id = null;
            this.ActorEntity.ActorInfo = null;
            this.ActorEntity.Network = null;
            this.Members.Clear();

            base.Dispose();
        }

        internal void SetLocal(TEntity entityInfo)
        {
            this.ActorEntity.ActorId = this.Id;
            this.EntityInfo = entityInfo;
            this.ActorEntity.Id = entityInfo.Id;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.ActorInfo = entityInfo;

            this.ActorPool.AddActor(this);
        }

        internal override void SetRemote(Network network, string objectId, int actorId)
        {
            this.ActorEntity.ActorId = actorId;
            this.ActorEntity.Id = objectId;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.Network = network;

            this.ActorPool.AddActor(this);
        }
    }

    public abstract class BaseActorComponent : BaseActor
    {
        /// <summary>
        /// 该组件拥有的成员，例如用户进入了房间，那么房间组件就拥有了该用户，设计时需要理解清楚需求的
        /// 主从关系，要不会造成逻辑混乱。
        /// </summary>
        public ActorMemberStorage Members { get; } = new ActorMemberStorage();
        public ActorEntity ActorEntity { get; } = new ActorEntity();
        public bool IsLocalActor { get { return this.ActorEntity.ActorInfo != null; } }
        public abstract ActorType ActorType { get;}
        internal abstract void SetRemote(Network network, string objectId, int actorId);
    }

    public static class BaseActorExtensions
    {
        private static Dictionary<int, Action<IActorMessage>> LocalCallActions { get; } = new Dictionary<int, Action<IActorMessage>>();

        /// <summary>
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="command">消息指令</param>
        public static void SendMySelf<TEnum>(this BaseActorComponent actor, Network network, TEnum command) where TEnum : Enum
        {
            if (!actor.IsLocalActor)
                throw new ComponentException("非LocalActor不能发送ActorMySelf消息。");

            var syncMessage = new ActorSyncMessage
            {
                ActorId = actor.Id,
                ObjectId = actor.ActorEntity.Id,
                ActorType = actor.ActorEntity.ActorType,
            };
            network.Send(syncMessage, command);
        }

        /// <summary>
        /// 从当前Actor发送消息给另一个Actor
        /// </summary>
        /// <typeparam name="TActorMessage"></typeparam>
        /// <param name="actor"></param>
        /// <param name="message"></param>
        /// <param name="command"></param>
        /// <param name="rpcId"></param>
        public static void SendActor <TActorMessage>(this BaseActorComponent actor, TActorMessage message, int command, int rpcId)
            where TActorMessage : IActorMessage
        {
            if (actor.IsLocalActor)
            {
                if (!MessageSubscriberStorage.TryGetSubscribers(command, out HashSet<ISubscriber> subscribers))
                    return;

                if(LocalCallActions.TryGetValue(rpcId, out Action<IActorMessage> action))
                {
                    action(message);
                    return;
                }

                var type = message.GetType();
                foreach (var subscriber in subscribers)
                {
                    if (type != subscriber.MessageType)
                        continue;

                    subscriber.Notify(message, command, rpcId);
                    break;
                }
            }
            else
            {
                var network = actor.ActorEntity.Network;
                network.Send(message, command);
            }
        }

        /// <summary>
        /// 请求一条Actor消息
        /// </summary>
        /// <typeparam name="TActorRequest"></typeparam>
        /// <typeparam name="TActorResponse"></typeparam>
        /// <param name="actor"></param>
        /// <param name="message"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<TActorResponse> CallActor<TActorRequest,TActorResponse>(this BaseActorComponent actor, TActorRequest message, int command) 
            where TActorRequest : IActorMessage where TActorResponse : IActorMessage
        {
            TActorResponse response = default;
            if (actor.IsLocalActor)
            {
                if (!MessageSubscriberStorage.TryGetSubscribers(command, out HashSet<ISubscriber> subscribers))
                    return response;

                var type = message.GetType();
                foreach (var subscriber in subscribers)
                {
                    if (type != subscriber.MessageType)
                        continue;

                    var rpcId = ActorLocalRpcIdCreator.RpcId;
                    while (LocalCallActions.ContainsKey(rpcId))
                    {
                        rpcId = ActorLocalRpcIdCreator.RpcId;
                    }
                    LocalCallActions[rpcId] = a => { response = (TActorResponse)a; };
                    subscriber.Notify(message, command, rpcId);
                    break;
                }
            }
            else
            {
                var network = actor.ActorEntity.Network;
                response = await network.CallMessageAsync<TActorRequest, TActorResponse>(message, command);
            }
            return response;
        }

        /// <summary>
        /// 添加一个成员
        /// </summary>
        /// <param name="current"></param>
        /// <param name="member"></param>
        public static void AddMember(this BaseActorComponent current, BaseActorComponent member)
        {
            current.Members.AddComponent(member);
        }

        /// <summary>
        /// 获取一个类型成员集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="current"></param>
        /// <returns></returns>
        public static HashSet<BaseActorComponent> GetMembers<T>(this BaseActorComponent current) where T : BaseActorComponent
        {
            return current.Members.GetComponents<T>();
        }

        /// <summary>
        /// 获取一个组件集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static HashSet<BaseActorComponent> GetMembers(this BaseActorComponent current, Type type)
        {
            return current.Members.GetComponents(type);
        }

        /// <summary>
        /// 获取一个成员
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="current"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetMember<T>(this BaseActorComponent current, int id) where T : BaseActorComponent
        {
            return current.Members.GetComponent<T>(id);
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool Remove<T>(this BaseActorComponent current, T component) where T : BaseActorComponent
        {
            return current.Members.Remove(component);
        }

        /// <summary>
        /// 删除一个组件
        /// </summary>
        /// <param name="current"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Remove(this BaseActorComponent current, int id)
        {
            return current.Members.Remove(id);
        }
    }
}
