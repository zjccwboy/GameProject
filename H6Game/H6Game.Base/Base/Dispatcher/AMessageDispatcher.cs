﻿using System;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class Condition
    {
        public bool IsRpc;
        public bool IsCompress;
        public bool IsEncrypt;
    }

    public abstract class AMessageDispatcher<Response> : IDispatcher<Response>
    {
        /// <summary>
        /// 网络会话管理对象
        /// </summary>
        protected Session Session { get; private set; }

        /// <summary>
        /// 网络连接管道
        /// </summary>
        protected ANetChannel Channel { get; private set; }

        /// <summary>
        /// 返回的当前消息条件
        /// </summary>
        protected Condition Conditions { get;} = new Condition();

        /// <summary>
        /// RpcId
        /// </summary>
        protected int RpcId { get; private set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        protected int MessageId { get; private set; }

        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public Type ResponseType
        {
            get
            {
                return typeof(Response);
            }
        }

        /// <summary>
        /// 接收处理接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="channel"></param>
        /// <param name="packet"></param>
        public virtual void Receive(Session session, ANetChannel channel, Packet packet)
        {
            this.Session = session;
            this.Channel = channel;
            this.Conditions.IsCompress = packet.IsCompress;
            this.Conditions.IsEncrypt = packet.IsEncrypt;
            this.Conditions.IsRpc = packet.IsRpc;
            this.MessageId = packet.MessageId;
            this.RpcId = packet.RpcId;
            if (DispatcherFactory.TryGetResponse(packet.MessageId, packet.Data, out Response response))
            {
                Dispatcher(response);
            }
            this.Session = null;
            this.Channel = null;
        }

        /// <summary>
        /// 应答消息
        /// </summary>
        /// <param name="bytes"></param>
        protected virtual void CallBack(byte[] bytes)
        {
            Send(bytes, this.MessageId, this.RpcId, this.Conditions.IsCompress, this.Conditions.IsEncrypt);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        protected virtual void Send(byte[] bytes, int messageCmd, int rpcId = 0, bool isCompress = false, bool isEncrypt = false)
        {
            this.Session.Notice(this.Channel, new Packet
            {
                IsCompress = isCompress,
                IsEncrypt = isEncrypt,
                RpcId = rpcId,
                MessageId = messageCmd,
                Data = bytes,
            });
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <returns></returns>
        protected virtual Task<T> CallMessage<T>(byte[] bytes, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        {
            var tcs = new TaskCompletionSource<T>();
            var send = new Packet
            {
                IsCompress = isCompress,
                IsEncrypt = isEncrypt,
                MessageId = messageCmd,
                Data = bytes,
            };

            this.Session.Subscribe(this.Channel, send , (p)=> 
            {
                if (!DispatcherFactory.TryGetResponse(p.MessageId, p.Data, out T response))
                {
                    tcs.TrySetResult(default(T));
                }
                tcs.TrySetResult(response);
            });
            return tcs.Task;
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Dispatcher(Response response);
    }
}
