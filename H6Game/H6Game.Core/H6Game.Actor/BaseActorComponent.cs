﻿using H6Game.Base;
using System;
using System.Collections.Generic;

namespace H6Game.Actor
{
    public abstract class BaseActorComponent<TEntity> : BaseActorComponent where TEntity : BaseEntity
    {
        public TEntity EntityInfo { get; protected set; }
        private ActorComponentStorage ActorPool { get; } = Game.Scene.GetComponent<ActorComponentStorage>();

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

            this.ActorPool.AddLocal(this);
        }

        internal override void SetRemote(Network network, string objectId, int actorId)
        {
            this.ActorEntity.ActorId = actorId;
            this.ActorEntity.Id = objectId;
            this.ActorEntity.ActorType = this.ActorType;
            this.ActorEntity.Network = network;

            this.ActorPool.AddRemote(this);
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
        /// <summary>
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="messageCmd">消息指令</param>
        public static void SendMySelf<TEnum>(this BaseActorComponent actor, Network network, TEnum messageCmd) where TEnum : Enum
        {
            if (!actor.IsLocalActor)
                throw new ComponentException("非LocalActor不能发送ActorMySelf消息。");

            var syncMessage = new ActorSyncMessage
            {
                ActorId = actor.Id,
                ObjectId = actor.ActorEntity.Id,
                ActorType = actor.ActorEntity.ActorType,
            };
            network.Send(syncMessage, messageCmd);
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
