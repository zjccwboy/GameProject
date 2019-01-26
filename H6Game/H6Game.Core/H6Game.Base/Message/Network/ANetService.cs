using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using H6Game.Base.SyncContext;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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

        protected Network Network { get; }
        protected Socket Acceptor { get; set; }
        protected ANetChannel ClientChannel { get; set; }

        public ProtocalType ProtocalType { get; set; }
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