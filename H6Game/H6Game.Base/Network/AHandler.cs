using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发处理类，所有消息处理应该继承该抽象类实现
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    public abstract class AHandler<Message> : AHandler, IHandler<Message>
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public new Type MessageType
        {
            get
            {
                return typeof(Message);
            }
        }

        public override void OnReceive(Network network)
        {
            if (network.RecvPacket.TryGetMessage(out Message message))
            {
                Handler(network, message);
                return;
            }

            throw new Exception("错误的消息分发.");
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
            OnReceive(network);
        }

        /// <summary>
        /// 接收处理接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="channel"></param>
        public abstract void OnReceive(Network network);
    }
}
