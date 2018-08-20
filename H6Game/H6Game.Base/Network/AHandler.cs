using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发处理类，所有消息处理应该继承该抽象类实现
    /// </summary>
    /// <typeparam name="Message"></typeparam>
    public abstract class AHandler<Message> : IHandler<Message>
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
                    typeCode = HandlerMsgPool.GetTypeCode(this.MessageType);

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
                return;

            Handler(network);
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Handler(Network network);
    }
}
