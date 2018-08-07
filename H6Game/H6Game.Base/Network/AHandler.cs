using System;
using System.Threading.Tasks;
using H6Game.Base;

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

        public override void OnReceive(Session session, ANetChannel channel)
        {
            try
            {
                var packet = channel.RecvParser.Packet;
                if (packet.TryGetMessage(out Message message))
                {
                    Handler(message);
                    return;
                }

                throw new Exception("错误的消息分发.");
            }
            catch (Exception e)
            {
                this.Log(LogLevel.Error, "Receive", $"Packet:{this.Packet.ToJson()}");
                this.Log(LogLevel.Error, "Receive", e.ToString());
                throw e;
            }
        }

        /// <summary>
        /// 消息分发接口
        /// </summary>
        /// <param name="response"></param>
        protected abstract void Handler(Message message);
    }

    public abstract class AHandler : IHandler
    {
        /// <summary>
        /// 网络会话管理对象
        /// </summary>
        protected Session Session { get; set; }

        /// <summary>
        /// 网络连接管道
        /// </summary>
        protected ANetChannel Channel { get; set; }

        /// <summary>
        /// 数据包
        /// </summary>
        protected Packet Packet { get; set; }

        /// <summary>
        /// 返回数据约定类型
        /// </summary>
        public virtual Type ResponseType { get; }

        public void Receive(Session session, ANetChannel channel)
        {
            var packet = channel.RecvParser.Packet;
            this.Session = session;
            this.Channel = channel;
            this.Packet = channel.RecvParser.Packet;

            OnReceive(session, channel);

            this.Session = null;
            this.Channel = null;
        }


        /// <summary>
        /// 接收处理接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="channel"></param>
        public abstract void OnReceive(Session session, ANetChannel channel);
    }
}
