using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Exceptions;
using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Actor
{
    public static class BaseActorComponentExtensions
    {
        private static Dictionary<int, Action<IActorMessage>> LocalCallActions { get; } = new Dictionary<int, Action<IActorMessage>>();

        /// <summary>
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="command">消息指令</param>
        internal static void SendMySelf(this BaseActorComponent actor, Network network, NetCommand command)
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
        internal static void SendActor<TActorMessage>(this BaseActorComponent actor, TActorMessage message, NetCommand command)
            where TActorMessage : IActorMessage
        {
            if (actor.IsLocalActor)
            {
                actor.ReceiveMessage(message);
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
        internal static async Task<TActorResponse> CallActor<TActorRequest, TActorResponse>(this BaseActorComponent actor, TActorRequest message, NetCommand command)
            where TActorRequest : IActorMessage where TActorResponse : IActorMessage
        {
            TActorResponse response = default;
            if (actor.IsLocalActor)
            {
                response = (TActorResponse)actor.ReceiveRpcMessage(message); 
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
