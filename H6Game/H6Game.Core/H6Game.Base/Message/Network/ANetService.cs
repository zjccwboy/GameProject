﻿using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using H6Game.Base.SyncContext;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 网络通讯服务抽象类
    /// </summary>
    public abstract class ANetService : SynchronizationThreadContextObject, IDisposable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="session">会话类</param>
        /// <param name="network">网络类</param>
        public ANetService(Session session, Network network)
        {
            this.Session = session;
            this.Network = network;
        }

        private uint LastCheckHeadbeatTime = TimeUitls.Now();
        private const uint KcpHeartbeatTime = 20 * 1000;
        private const uint TcpHeartbeatTime = 4 * 1000;
        private const uint WcpHeartbeatTime = 10 * 1000;
        /// <summary>
        /// 心跳超时时长，TCP 4秒,KCP 20秒,WebSocket 10秒
        /// </summary>
        public uint HeartbeatTime
        {
            get
            {
                if (this.ProtocalType == ProtocalType.Kcp)
                {
                    return KcpHeartbeatTime;
                }
                else if(this.ProtocalType == ProtocalType.Tcp)
                {
                    return TcpHeartbeatTime;
                }
                else if(this.ProtocalType == ProtocalType.Wcp)
                {
                    return WcpHeartbeatTime;
                }
                throw new NetworkException("协议类型不存在。");
            }
        }

        protected Network Network { get; }
        protected ProtocalType ProtocalType { get; set; }
        protected Socket Acceptor { get; set; }
        protected ANetChannel ClientChannel { get; set; }

        public NetServiceType ServiceType { get; set; }
        public ConcurrentDictionary<long, ANetChannel> Channels { get; } = new ConcurrentDictionary<long, ANetChannel>();
        public Session Session { get;private set; }
        public Action<ANetChannel> OnServerDisconnected { get; set; }
        public Action<ANetChannel> OnServerConnected { get; set; }
        public Action<ANetChannel> OnClientDisconnected { get; set; }
        public Action<ANetChannel> OnClientConnected { get; set; }

        public abstract void Update();
        public abstract void Accept();
        public abstract ANetChannel Connect();

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
                    this.ClientChannel.SendHeartbeat();
            }
            else if (this.ServiceType == NetServiceType.Server)
            {
                var lastCheckSpan = now - this.LastCheckHeadbeatTime;
                if (lastCheckSpan < HeartbeatTime / 2)
                    return;
                LastCheckHeadbeatTime = now;

                var channels = this.Channels.Values;
                foreach (var channel in channels)
                {
                    var timeSpan = now - channel.LastReceivedTime;
                    if (timeSpan > HeartbeatTime + 2000) //允许2秒钟网络延迟
                    {
                        Log.Debug($"{this.ProtocalType}客户端:{channel.RemoteEndPoint}连接超时，心跳检测断开，心跳时长{timeSpan}.", LoggerBllType.Network);
                        channel.Disconnect();
                    }
                }
            }
        }

        protected void AddChannel(ANetChannel channel)
        {
            Channels.TryAdd(channel.Id, channel);
        }

        protected void OnAccept(ANetChannel channel)
        {
            if (this.ProtocalType == ProtocalType.Wcp)
            {
                var wchannel = channel as WcpChannel;
                Log.Debug($"{this.ProtocalType}监听Prefied:{wchannel.HttpPrefixed}接受客户端:{channel.RemoteEndPoint}连接成功.", LoggerBllType.Network);
            }
            else
            {
                var loacalPort = channel.LocalEndPoint == null ? 0 : channel.LocalEndPoint.Port;
                Log.Debug($"{this.ProtocalType}监听端口:{loacalPort}接受客户端:{channel.RemoteEndPoint}连接成功.", LoggerBllType.Network);
            }
            channel.Connected = true;
            AddChannel(channel);
            channel.OnDisconnected = OnServerDisConnect;
            channel.OnReceive = (p) => { channel.Network.Dispatching(p); };
            OnServerConnected?.Invoke(channel);
        }

        protected void OnConnect(ANetChannel channel)
        {
            if(this.ProtocalType == ProtocalType.Wcp)
            {
                var wchannel = channel as WcpChannel;
                Log.Debug($"{this.ProtocalType}连接服务端:{wchannel.HttpPrefixed}成功.", LoggerBllType.Network);
            }
            else
            {
                var loacalPort = channel.LocalEndPoint == null ? 0 : channel.LocalEndPoint.Port;
                Log.Debug($"{this.ProtocalType}端口:{loacalPort}连接服务端:{channel.RemoteEndPoint}成功.", LoggerBllType.Network);
            }

            channel.Connected = true;
            this.AddChannel(channel);
            channel.OnDisconnected = OnClientDisConnect;
            channel.OnReceive = (p) => { channel.Network.Dispatching(p); };
            this.OnClientConnected?.Invoke(channel);
        }

        protected void OnServerDisConnect(ANetChannel channel)
        {
            if (Channels.TryRemove(channel.Id, out ANetChannel value))
            {
                if (this.ProtocalType == ProtocalType.Wcp)
                {
                    var wchannel = channel as WcpChannel;
                    Log.Debug($"{this.ProtocalType}监听Prefixed:{wchannel.HttpPrefixed}与客户端:{channel.RemoteEndPoint}连接断开.", LoggerBllType.Network);
                }
                else
                {
                    var loacalPort = channel.LocalEndPoint == null ? 0 : channel.LocalEndPoint.Port;
                    Log.Debug($"{this.ProtocalType}监听端口:{loacalPort}与客户端:{channel.RemoteEndPoint}连接断开.", LoggerBllType.Network);
                }
                OnServerDisconnected?.Invoke(channel);
            }
        }

        protected void OnClientDisConnect(ANetChannel channel)
        {
            if (Channels.TryRemove(channel.Id, out ANetChannel value))
            {
                if (this.ProtocalType == ProtocalType.Wcp)
                {
                    var wchannel = channel as WcpChannel;
                    Log.Debug($"{this.ProtocalType}与服务端:{wchannel.HttpPrefixed}连接断开.", LoggerBllType.Network);
                }
                else
                {
                    var loacalPort = channel.LocalEndPoint == null ? 0 : channel.LocalEndPoint.Port;
                    Log.Debug($"{this.ProtocalType}端口:{loacalPort}与服务端:{channel.RemoteEndPoint}连接断开.", LoggerBllType.Network);
                }
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