﻿using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发处理类，所有消息处理应该继承该抽象类实现
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    public abstract class AHandler<Message> : IHandler<Message>
    {
        private Type MsgType;
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public Type MessageType
        {
            get
            {
                MsgType = MsgType ?? typeof(Message);
                return MsgType;
            }
        }

        private int typeCode;
        /// <summary>
        /// 消息类型Code
        /// </summary>
        public int MsgTypeCode
        {
            get
            {
                if (typeCode <= 0)
                    typeCode = HandlerMSGFactory.GetTypeCode(this.MessageType);

                return typeCode;
            }
        }

        public void Receive(Network network)
        {
            if (this.MsgTypeCode != network.RecvPacket.MsgTypeCode)
                return;

            if (!network.RecvPacket.IsValidMessage(this.MessageType))
                return;

            var message = network.RecvPacket.GetMessage<Message>();
            Handler(network, message);
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Handler(Network network, Message message);
    }

    public abstract class AHandler : IHandler
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
                throw new Exception("消息分发错误，无类型的AHandler继承类中不应该存在MessageTypeCode.");

            Handler(network);
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Handler(Network network);
    }
}
