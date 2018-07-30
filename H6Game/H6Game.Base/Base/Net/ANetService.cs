using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// 网络通讯服务抽象类
    /// </summary>
    public abstract class ANetService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session"></param>
        public ANetService(Session session)
        {
            Session = session;
        }

        /// <summary>
        /// 发送列表
        /// </summary>
        private readonly HashSet<ANetChannel> senders = new HashSet<ANetChannel>();

        /// <summary>
        /// 最后一次检测心跳的时间
        /// </summary>
        private uint LastCheckTime = TimeUitls.Now();

        /// <summary>
        /// 心跳超时时长，服务端6秒,客户端20秒
        /// </summary>
#if SERVER
        public static uint HeartbeatTime = 1000 * 4;
#else
        public static uint HeartbeatTime = 1000 * 20;
#endif

        /// <summary>
        /// 接受连接请求Socket
        /// </summary>
        protected Socket acceptor;

        /// <summary>
        /// 网络服务类型
        /// </summary>
        protected NetServiceType ServiceType;

        /// <summary>
        /// 客户端连接ANetChannel
        /// </summary>
        protected ANetChannel ClientChannel;

        /// <summary>
        /// 发送包队列
        /// </summary>
        protected readonly ConcurrentQueue<SendTask> SendQueue = new ConcurrentQueue<SendTask>();

        /// <summary>
        /// 连接通道池
        /// </summary>
        public readonly ConcurrentDictionary<long, ANetChannel> Channels = new ConcurrentDictionary<long, ANetChannel>();

        /// <summary>
        /// 消息会话
        /// </summary>
        public Session Session { get;private set; }

        /// <summary>
        /// 监听并接受Socket连接
        /// </summary>
        /// <returns></returns>
        public abstract bool Accept();

        /// <summary>
        /// 发送连接请求与创建连接
        /// </summary>
        /// <returns></returns>
        public abstract ANetChannel Connect();

        /// <summary>
        /// 连接断开回调在服务端发生
        /// </summary>
        public Action<ANetChannel> OnServerDisconnected { get; set; }

        /// <summary>
        /// 连接成功回调在服务端发生
        /// </summary>
        public Action<ANetChannel> OnServerConnected { get; set; }

        /// <summary>
        /// 连接断开回调在客户端发生
        /// </summary>
        public Action<ANetChannel> OnClientDisconnected { get; set; }

        /// <summary>
        /// 连接成功回调在客户端发生
        /// </summary>
        public Action<ANetChannel> OnClientConnected { get; set; }

        /// <summary>
        /// 更新发送接收队列
        /// </summary>
        public virtual void Update()
        {
            if(this.ServiceType == NetServiceType.Client && ClientChannel != null)
            {                
                ClientChannel.StartConnecting();
            }
            HandleSend();
            HandleReceive();
        }

        /// <summary>
        /// 插入一个数据包到发送队列中
        /// </summary>
        /// <param name="sendTask"></param>
        public void Enqueue(SendTask sendTask)
        {
            this.SendQueue.Enqueue(sendTask);
        }

        /// <summary>
        /// 处理数据发送回调函数
        /// </summary>
        protected void HandleSend()
        {
            try
            {
                while (!this.SendQueue.IsEmpty)
                {
                    if(this.SendQueue.TryPeek(out SendTask send))
                    {
                        if (send.Channel.Connected)
                        {
                            this.SendQueue.TryDequeue(out SendTask sendTask);
                            sendTask.Channel.TimeNow = TimeUitls.Now();
                            sendTask.WriteToBuffer();
                            this.senders.Add(sendTask.Channel);
                        }
                    }
                }

                foreach (var channel in this.senders)
                {
                    channel.StartSend();
                }
                this.senders.Clear();

                this.CheckHeadbeat();
            }
            catch (Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/HandleSend", e);
            }
        }

        /// <summary>
        /// 所有管道接收数据
        /// </summary>
        private void HandleReceive()
        {
            var channels = Channels.Values;
            foreach (var channel in channels)
            {
                channel.StartRecv();
            }
        }

        /// <summary>
        /// 心跳检测
        /// </summary>
        private void CheckHeadbeat()
        {
            var now = TimeUitls.Now();
            var lastCheckSpan = now - this.LastCheckTime;
            if (lastCheckSpan < HeartbeatTime)
                return;
            LastCheckTime = now;

            if (this.ServiceType == NetServiceType.Client)
            {
                if (this.ClientChannel == null)
                    return;

                if (!this.ClientChannel.Connected)
                    return;

                var  timeSendSpan = now - this.ClientChannel.LastSendTime;
                if (timeSendSpan > HeartbeatTime - 200)
                {
                    this.Session.Notice(this.ClientChannel, new Packet
                    {
                        IsHeartbeat = true
                    });
                    LogRecord.Log(LogLevel.Info, $"{this.GetType()}/CheckHeadbeat", $"发送心跳包到服务端:{this.ClientChannel.RemoteEndPoint}.");
                }
            }
            else if (this.ServiceType == NetServiceType.Server)
            {
                var channels = this.Channels.Values;
                foreach (var channel in channels)
                {
                    if (channel == null)
                        continue;

                    if (!channel.Connected)
                        continue;

                    var timeSpan = now - channel.LastRecvTime;
                    if (timeSpan > HeartbeatTime)
                    {
                        LogRecord.Log(LogLevel.Info, $"{this.GetType()}/CheckHeadbeat", $"客户端:{channel.RemoteEndPoint}连接超时，心跳检测断开，心跳时长{timeSpan}.");
                        channel.DisConnect();
                    }
                }
            }
        }

        /// <summary>
        /// 添加一个通讯管道到连接对象池中
        /// </summary>
        /// <param name="channel"></param>
        protected void AddChannel(ANetChannel channel)
        {
            Channels.TryAdd(channel.Id, channel);
        }

        /// <summary>
        /// 处理连接断开(服务端)
        /// </summary>
        /// <param name="channel"></param>
        protected void HandleDisConnectOnServer(ANetChannel channel)
        {
            try
            {
                if (Channels.TryRemove(channel.Id, out ANetChannel value))
                {
                    OnServerDisconnected?.Invoke(channel);
                    LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleDisConnectOnServer", $"客户端:{channel.RemoteEndPoint}连接断开.");
                }
            }
            catch (Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/HandleDisConnectOnServer", e);
            }
        }

        /// <summary>
        /// 处理连接断开(客户端)
        /// </summary>
        /// <param name="channel"></param>
        protected void HandleDisConnectOnClient(ANetChannel channel)
        {
            try
            {
                if (Channels.TryRemove(channel.Id, out ANetChannel value))
                {
                    OnClientDisconnected?.Invoke(value);
                    LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleDisConnectOnClient", $"与服务端{channel.RemoteEndPoint}连接断开.");
                }
            }
            catch (Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/HandleDisConnectOnClient", e);
            }
        }
    }
}