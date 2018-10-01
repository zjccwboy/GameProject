﻿using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// 网络通讯服务抽象类
    /// </summary>
    public abstract class ANetService : IDisposable
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
        /// 最后一次检测心跳的时间
        /// </summary>
        private uint LastCheckHeadbeatTime = TimeUitls.Now();

        private const uint KcpHeartbeatTime = 20 * 1000;
        private const uint TcpHeartbeatTime = 6 * 1000;
        /// <summary>
        /// 心跳超时时长，服务端6秒,客户端20秒
        /// </summary>
        public uint HeartbeatTime
        {
            get
            {
                if (ProtocalType == ProtocalType.Kcp)
                {
                    return KcpHeartbeatTime;
                }
                else if(ProtocalType == ProtocalType.Tcp)
                {
                    return TcpHeartbeatTime;
                }
                return 99999 * 1000;
            }
        }

        /// <summary>
        /// 通讯协议类型
        /// </summary>
        protected ProtocalType ProtocalType { get; set; }

        /// <summary>
        /// 接受连接请求Socket
        /// </summary>
        protected Socket Acceptor;

        /// <summary>
        /// 网络服务类型
        /// </summary>
        protected NetServiceType ServiceType;

        /// <summary>
        /// 客户端连接ANetChannel
        /// </summary>
        protected ANetChannel ClientChannel;

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
        public abstract void Update();

        /// <summary>
        /// 心跳检测
        /// </summary>
        protected void CheckHeadbeat()
        {
            var now = TimeUitls.Now();
            if (this.ServiceType == NetServiceType.Client)
            {
                if (this.ClientChannel == null)
                    return;

                if (!this.ClientChannel.Connected)
                    return;

                var  timeSendSpan = now - this.ClientChannel.LastSendTime;
                if (timeSendSpan > HeartbeatTime)
                {
                    this.ClientChannel.LastSendTime = TimeUitls.Now();
                    this.ClientChannel.SendParser.Packet.IsHeartbeat = true;
                    this.ClientChannel.SendParser.Packet.WriteTo(null);
                    //this.Log(LogLevel.Info, "CheckHeadbeat", $"发送心跳包到服务端:{this.ClientChannel.RemoteEndPoint}.");
                }
            }
            else if (this.ServiceType == NetServiceType.Server)
            {
                var lastCheckSpan = now - this.LastCheckHeadbeatTime;
                if (lastCheckSpan < HeartbeatTime)
                    return;
                LastCheckHeadbeatTime = now;

                var channels = this.Channels.Values;
                foreach (var channel in channels)
                {
                    var timeSpan = now - channel.LastRecvTime;
                    if (timeSpan > HeartbeatTime + 3000) //允许3秒网络延迟
                    {
                        Log.Info($"客户端:{channel.RemoteEndPoint}连接超时，心跳检测断开，心跳时长{timeSpan}.", LoggerBllType.System);
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
            if (Channels.TryRemove(channel.Id, out ANetChannel value))
            {
                Log.Info($"客户端:{channel.RemoteEndPoint}连接断开.", LoggerBllType.System);
                OnServerDisconnected?.Invoke(channel);
            }
        }

        /// <summary>
        /// 处理连接断开(客户端)
        /// </summary>
        /// <param name="channel"></param>
        protected void HandleDisConnectOnClient(ANetChannel channel)
        {
            if (Channels.TryRemove(channel.Id, out ANetChannel value))
            {
                Log.Info($"与服务端{channel.RemoteEndPoint}连接断开.", LoggerBllType.System);
                OnClientDisconnected?.Invoke(value);
            }
        }

        public void Dispose()
        {
            foreach(var channel in Channels.Values)
            {
                channel.Dispose();
            }
        }
    }
}