using System;

namespace H6Game.Base
{
    public class Condition
    {
        public bool IsRpc;
        public bool IsCompress;
        public bool IsEncrypt;
    }

    /// <summary>
    /// 消息分发处理类，所有消息处理应该继承该抽象类实现
    /// </summary>
    /// <typeparam name="Response"></typeparam>
    public abstract class AHandler<Message> : AHandler, IHandler<Message>
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public override Type ResponseType
        {
            get
            {
                return typeof(Message);
            }
        }

        public override void OnReceive(Network1 network)
        {
            try
            {
                if (network.RecvPacket.TryGetMessage(out Message message))
                {
                    Handler(network, message);
                    return;
                }

                throw new Exception("错误的消息分发.");
            }
            catch (Exception e)
            {
                this.Log(LogLevel.Error, "Receive", $"Packet:{network.RecvPacket.ToJson()}");
                this.Log(LogLevel.Error, "Receive", e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Handler(Network1 network, Message message);
    }

    public abstract class AHandler : IHandler
    {
        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public abstract Type ResponseType { get; }

        /// <summary>
        /// 接收处理
        /// </summary>
        /// <param name="network"></param>
        public void Receive(Network1 network)
        {
            OnReceive(network);
        }

        /// <summary>
        /// 接收处理接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="channel"></param>
        public abstract void OnReceive(Network1 network);
    }
}
