using System.Threading.Tasks;

namespace H6Game.Base
{
    public static class ActorMessageHelper
    {
        /// <summary>
        /// 发送当前Actor到指定的Network连接服务。
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="network">网络类</param>
        /// <param name="messageCmd">消息指令</param>
        public static void SendMySelf(this BaseActorEntityComponent actor, Network network, int messageCmd)
        {
            var syncMessage = new ActorSyncMessage
            {
                ObjectId = actor.ActorEntity.Id,
                ActorType = actor.ActorEntity.ActorType,
            };
            network.SendActor(syncMessage, messageCmd, actor.Id);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <typeparam name="TSender"></typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor<TSender>(this BaseActorEntityComponent actor, TSender data, int messageCmd) where TSender : class
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, string data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, int data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, uint data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, long data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, ulong data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, float data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, double data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, decimal data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, sbyte data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, bool data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, char data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, short data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 发送Actor消息
        /// </summary>
        /// <param name="actor">Actor类</param>
        /// <param name="data">发送数据</param>
        /// <param name="messageCmd">表示这条消息指令</param>
        public static void SendActor(this BaseActorEntityComponent actor, ushort data, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            actor.ActorEntity.Network.SendActor(data, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TRequest">发送数据类型</typeparam>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TRequest, TResponse>(this BaseActorEntityComponent actor, TRequest request, int messageCmd) where TRequest : class
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TRequest, TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, string request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, int request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, uint request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, long request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, ulong request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, float request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, double request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, decimal request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, byte request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, sbyte request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, char request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, short request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }

        /// <summary>
        /// 订阅一条Actor Rpc消息
        /// </summary>
        /// <typeparam name="TResponse">返回数据类型</typeparam>
        /// <param name="actor">Actor类</param>
        /// <param name="request">发送数据</param>
        /// <param name="messageCmd">消息指令</param>
        /// <returns>返回消息数据</returns>
        public static async Task<CallResult<TResponse>> CallActorAsync<TResponse>(this BaseActorEntityComponent actor, ushort request, int messageCmd)
        {
            if (actor.IsLocalActor)
                throw new ComponentException("LocalActor不能发送Actor消息，只有RemoteActor才能够跟远程的Actor通讯。");

            return await actor.ActorEntity.Network.CallActorAsync<TResponse>(request, messageCmd, actor.ActorEntity.ActorId);
        }
    }
}
