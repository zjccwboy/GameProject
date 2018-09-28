﻿using System;

namespace H6Game.Base
{
    /// <summary>
    /// Actor消息订阅者类
    /// </summary>
    /// <typeparam name="Message"></typeparam>
    public abstract class AActorSubscriber<Message> : IActorSubscriber<Message>
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public Type MessageType { get; } = typeof(Message);

        private int typeCode;
        public int MsgTypeCode
        {
            get
            {
                if(typeCode <=0)
                    typeCode = HandlerMsgPool.GetTypeCode(this.MessageType);

                return typeCode;
            }
        }

        public void Receive(Network network)
        {
            if (network.RecvPacket.ActorId <= 0)
                throw new NetworkException($"Actor消息分发错误ActorId:{network.RecvPacket.ActorId}");

            if (this.MsgTypeCode != network.RecvPacket.MsgTypeCode)
                return;

            if (!network.RecvPacket.IsValidMessage(this.MessageType))
                return;

            var message = network.RecvPacket.GetMessage<Message>();
            Subscribe(network, message);
        }

        /// <summary>
        /// 消息订阅接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Subscribe(Network network, Message message);
    }

    /// <summary>
    /// Actor消息订阅者类
    /// </summary>
    public abstract class AActorSubscriber : IActorSubscriber
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public Type MessageType { get { return null; } }

        /// <summary>
        /// 接收处理
        /// </summary>
        /// <param name="network"></param>
        public void Receive(Network network)
        {
            if (network.RecvPacket.MsgTypeCode > 0)
                return;

            if (network.RecvPacket.ActorId <= 0)
                throw new NetworkException($"Actor消息分发错误ActorId:{network.RecvPacket.ActorId}");

            Subscribe(network);
        }

        /// <summary>
        /// 消息订阅接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Subscribe(Network network);
    }
}