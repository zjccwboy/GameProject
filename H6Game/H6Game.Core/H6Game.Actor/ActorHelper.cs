﻿using H6Game.Base;
using System.Threading.Tasks;

namespace H6Game.Actor
{
    public static class ActorHelper
    {
        /// <summary>
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="messageCmd">消息指令</param>
        public static void SendMySelf(this BaseActorEntityComponent actor, Network network, int messageCmd)
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
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="messageCmd">消息指令</param>
        public static void SendMySelfMessage<TMessage>(this BaseActorEntityComponent actor, Network network, TMessage message, int messageCmd) where TMessage : class
        {
            if (!actor.IsLocalActor)
                throw new ComponentException("非LocalActor不能发送ActorMySelf消息。");

            network.Send(message, messageCmd);
        }
    }
}
