﻿using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息订阅者类
    /// </summary>
    /// <typeparam name="Message"></typeparam>
    public abstract class NetSubscriber<Message> : ISubscriber<Message>
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public Type MessageType { get; } = typeof(Message);

        private int typeCode;
        /// <summary>
        /// 消息类型Code
        /// </summary>
        public int MsgTypeCode
        {
            get
            {
                if (typeCode <= 0)
                    typeCode = MessageCommandStorage.GetMsgCode(this.MessageType);

                return typeCode;
            }
        }

        public void Receive(Network network)
        {
            if (this.MsgTypeCode != network.RecvPacket.MsgTypeCode)
                return;

            if (!MessageSubscriberStorage.ExistSubscriberCmd(network.RecvPacket.NetCommand, this.MessageType))
                return;

            var message = network.RecvPacket.Read<Message>();
            Subscribe(network, message, network.RecvPacket.NetCommand);
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Subscribe(Network network, Message message, int netCommand);
    }

    /// <summary>
    /// 消息订阅者类
    /// </summary>
    public abstract class NetSubscriber : ISubscriber
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

            Subscribe(network, network.RecvPacket.NetCommand);
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Subscribe(Network network, int netCommand);
    }
}