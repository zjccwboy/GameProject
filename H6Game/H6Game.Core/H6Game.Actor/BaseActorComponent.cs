﻿using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Exceptions;
using H6Game.Base.Message;
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

            if(this.Parent != null)
            {
                this.Parent.Remove(this.Id);
                this.Parent = null;
            }

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

        internal override void SetRemote(Network network, int actorId)
        {
            this.ActorEntity.ActorId = actorId;
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
        public BaseActorComponent Parent { get; set; }
        internal abstract void SetRemote(Network network, int actorId);
        public abstract void ReceiveMessage(IActorMessage message, MSGCommand command);
        public abstract IActorMessage ReceiveRpcMessage(IActorMessage message, MSGCommand command);
    }
}
