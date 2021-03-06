﻿using System.Net;
using System;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum NetServiceType
    {
        None,
        Client,
        Server
    }

    /// <summary>
    /// 协议类型
    /// </summary>
    public enum ProtocalType
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// TCP
        /// </summary>
        Tcp,
        /// <summary>
        /// UDP Kcp
        /// </summary>
        Kcp,
        /// <summary>
        /// WebSocket
        /// </summary>
        Wcp,
    }

    /// <summary>
    /// 通讯会话接口类
    /// </summary>
    public class Session : IDisposable
    {
        public ANetService NService;
        private IPEndPoint EPoint { get; }
        private ProtocalType PType { get; }
        private string HttpPrefixed { get; }
        private Network Network { get; }

        /// <summary>
        /// 客户端连接管道
        /// </summary>
        public ANetChannel ConnectChannel { get; set; }

        /// <summary>
        /// 连接断开回调发生在服务端
        /// </summary>
        public Action<Network> OnServerDisconnected { get; set; }

        /// <summary>
        /// 连接成功回调发生在服务端
        /// </summary>
        public Action<Network> OnServerConnected { get; set; }

        /// <summary>
        /// 连接断开回调发生在客户端
        /// </summary>
        public Action<Network> OnClientDisconnected { get; set; }

        /// <summary>
        /// 连接成功回调发生在客户端
        /// </summary>
        public Action<Network> OnClientConnected { get; set; }

        public Session(IPEndPoint endPoint, Network network, ProtocalType protocalType)
        {
            this.EPoint = endPoint;
            this.PType = protocalType;
            this.Network = network;
        }

        public Session(string httpPrefixed, Network network, ProtocalType protocalType)
        {
            this.HttpPrefixed = httpPrefixed;
            this.PType = protocalType;
            this.Network = network;
        }

        /// <summary>
        /// 开始监听并接受客户端连接
        /// </summary>
        /// <param name="endPoint"></param>
        public bool Accept()
        {
            if(this.PType == ProtocalType.Tcp)
            {
                this.NService = new TcpService(this.EPoint, this, this.Network, NetServiceType.Server);
            }
            else if(this.PType == ProtocalType.Kcp)
            {
                this.NService = new KcpService(this.EPoint, this, this.Network, NetServiceType.Server);
            }
            else if (this.PType == ProtocalType.Wcp)
            {
                this.NService = new WcpService(this.HttpPrefixed, this, this.Network, NetServiceType.Server);
            }

            this.NService.OnServerConnected = (c) => { this.OnServerConnected?.Invoke(c.Network); };
            this.NService.OnServerDisconnected = (c) => { this.OnServerDisconnected?.Invoke(c.Network); };
            this.NService.Accept();
            return true;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public void Connect()
        {
            if (this.PType == ProtocalType.Tcp)
            {
                this.NService = new TcpService(this.EPoint, this, this.Network, NetServiceType.Client);
            }
            else if (this.PType == ProtocalType.Kcp)
            {
                this.NService = new KcpService(this.EPoint, this, this.Network, NetServiceType.Client);
            }
            else if(this.PType == ProtocalType.Wcp)
            {
                this.NService = new WcpService(this.HttpPrefixed, this, this.Network, NetServiceType.Client);
            }

            this.NService.OnClientDisconnected = (c) => { this.OnClientDisconnected?.Invoke(c.Network); };
            this.NService.OnClientConnected = (c) => { OnClientConnected?.Invoke(c.Network); };
            this.ConnectChannel = this.NService.Connect();
        }
        
        public void Update()
        {
            this.NService.Update();
        }
        
        internal void Send<TSender>(ANetChannel channel, TSender data, ushort netCommand, int rpcId)
        {
            channel.Send(data, netCommand, rpcId);
        }

        internal void Send(ANetChannel channel, object data, ushort netCommand, int rpcId, Type type)
        {
            channel.Send(data, netCommand, rpcId, type);
        }

        internal void Send(ANetChannel channel, ushort netCommand, int rpcId)
        {
            channel.Send(netCommand, rpcId);
        }

        internal void Subscribe(ANetChannel channel, object data, Action<Packet> notificationAction, ushort netCommand)
        {
            channel.Subscribe(data, notificationAction, netCommand);
        }

        internal void Subscribe<TRequest>(ANetChannel channel, TRequest data, Action<Packet> notificationAction, ushort netCommand)
        {
            channel.Subscribe(data, notificationAction, netCommand);
        }

        internal void Subscribe(ANetChannel channel, Action<Packet> notificationAction, ushort netCommand)
        {
            channel.Subscribe(notificationAction, netCommand);
        }

        internal void Broadcast<TRequest>(TRequest data, ushort netCommand)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                channel.Send(data, netCommand, 0);
            }
        }

        internal void Broadcast(ushort netCommand)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                channel.Send(netCommand, 0);
            }
        }

        public void Dispose()
        {
            this.NService.Dispose();
            this.NService = null;
        }
    }
}
