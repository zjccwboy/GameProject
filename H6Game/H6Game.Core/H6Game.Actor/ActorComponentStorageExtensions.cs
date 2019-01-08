using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Actor
{
    public static class ActorComponentStorageExtensions
    {
        /// <summary>
        /// 添加Actor
        /// </summary>
        /// <typeparam name="TActor"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="current"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static TActor AddActor<TActor, TEntity>(this ActorComponentStorage current, TEntity entity)
            where TEntity : BaseEntity where TActor : BaseActorComponent<TEntity>
        {
            var actor = Game.Scene.AddComponent<TActor>();
            actor.SetLocal(entity);
            return actor;
        }

        /// <summary>
        /// 添加Actor
        /// </summary>
        /// <param name="current"></param>
        /// <param name="network"></param>
        /// <param name="actorId"></param>
        /// <param name="actorType"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public static BaseActorComponent AddActor(this ActorComponentStorage current, Network network, ActorSyncMessage message)
        {
            var type = current.GetActorType(message.ActorType);
            var actor = Game.Scene.AddComponent(type) as BaseActorComponent;
            actor.SetRemote(network, message.ObjectId, message.ActorId);
            return actor;
        }

        /// <summary>
        /// 删除Acotr
        /// </summary>
        /// <param name="current"></param>
        /// <param name="actorId"></param>
        public static void Remove(this ActorComponentStorage current, int actorId)
        {
            if(Game.Scene.GetComponent(actorId, out BaseActorComponent actor))
            {
                actor.Dispose();
            }
        }

        /// <summary>
        /// 获取Acotr
        /// </summary>
        /// <typeparam name="TActor"></typeparam>
        /// <param name="current"></param>
        /// <param name="actorId"></param>
        /// <returns></returns>
        public static TActor GetActor<TActor>(this ActorComponentStorage current, int actorId) where TActor : BaseActorComponent
        {
            return Game.Scene.GetComponent<TActor>(actorId);
        }

        /// <summary>
        /// 从当前Actor发送消息给另一个Actor
        /// </summary>
        /// <typeparam name="TActorMessage"></typeparam>
        /// <param name="actor"></param>
        /// <param name="message"></param>
        /// <param name="command"></param>
        public static void SendActor<TActorMessage>(this ActorComponentStorage current, TActorMessage message, NetCommand command)
            where TActorMessage : IActorMessage
        {
            var actor = current.GetActor(message.ActorId);
            if(actor != null)
            {
                actor.SendActor(message, command);
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
        public static async Task<TActorResponse> CallActor<TActorRequest, TActorResponse>(this ActorComponentStorage current, TActorRequest message, NetCommand command)
            where TActorRequest : IActorMessage where TActorResponse : IActorMessage
        {
            var actor = current.GetActor(message.ActorId);
            if (actor != null)
            {
                return default;
            }
            return await actor.CallActor<TActorRequest, TActorResponse>(message, command);
        }
    }
}
