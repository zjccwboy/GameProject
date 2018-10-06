using System.Net;
using System;
using System.Collections.Generic;

namespace H6Game.Base
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
        Tcp,
        Kcp,
    }

    /// <summary>
    /// 通讯会话接口类
    /// </summary>
    public class Session : IDisposable
    {
        public ANetService NService;
        private IPEndPoint EPoint { get; }
        private ProtocalType PType { get; }

        /// <summary>
        /// 客户端连接管道
        /// </summary>
        public ANetChannel ConnectChannel { get; set; }

        /// <summary>
        /// 连接断开回调发生在服务端
        /// </summary>
        public Action<ANetChannel> OnServerDisconnected { get; set; }

        /// <summary>
        /// 连接断开回调发生在服务端
        /// </summary>
        public Action<ANetChannel> OnServerConnected { get; set; }

        /// <summary>
        /// 连接断开回调发生在客户端
        /// </summary>
        public Action<ANetChannel> OnClientDisconnected { get; set; }

        /// <summary>
        /// 连接断开回调发生在客户端
        /// </summary>
        public Action<ANetChannel> OnClientConnected { get; set; }

        public Session(IPEndPoint endPoint, ProtocalType protocalType)
        {
            this.EPoint = endPoint;
            this.PType = protocalType;
        }

        /// <summary>
        /// 开始监听并接受客户端连接
        /// </summary>
        /// <param name="endPoint"></param>
        public bool Accept()
        {
            if(this.PType == ProtocalType.Tcp)
            {
                this.NService = new TcpService(this.EPoint, this, NetServiceType.Server);
            }
            else if(this.PType == ProtocalType.Kcp)
            {
                this.NService = new KcpService(this.EPoint, this, NetServiceType.Server);
            }

            this.NService.OnServerConnected = (c) => { this.OnServerConnected?.Invoke(c); };
            this.NService.OnServerDisconnected = (c) => { this.OnServerDisconnected?.Invoke(c); };

            return this.NService.Accept();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public ANetChannel Connect()
        {
            if (this.PType == ProtocalType.Tcp)
            {
                this.NService = new TcpService(this.EPoint, this, NetServiceType.Client);
            }
            else if (this.PType == ProtocalType.Kcp)
            {
                this.NService = new KcpService(this.EPoint, this, NetServiceType.Client);
            }
            this.NService.OnClientDisconnected = (c) => { this.OnClientDisconnected?.Invoke(c); };
            this.NService.OnClientConnected = (c) => { OnClientConnected?.Invoke(c); };
            this.ConnectChannel = this.NService.Connect();
            return this.ConnectChannel;
        }
        
        public void Update()
        {
            this.NService.Update();
        }
        
        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send<TRequest>(ANetChannel channel, TRequest data, int netCommand, int rpcId) where TRequest : class
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, object data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, int data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo();
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, string data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }


        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, uint data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, bool data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, long data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, ulong data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, float data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, double data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, decimal data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, byte data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, sbyte data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, char data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, short data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Send(ANetChannel channel, ushort data, int netCommand, int rpcId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        private void SetHead(Packet packet, int netCommand, int rpcId)
        {
            packet.NetCommand = netCommand;
            packet.RpcId = rpcId;
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe<TRequest>(ANetChannel channel, TRequest data, Action<Packet> notificationAction, int netCommand) where TRequest : class
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, string data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, int data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, uint data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, bool data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, long data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, ulong data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, float data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, double data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, decimal data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, byte data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, sbyte data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, char data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, short data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="netCommand"></param>
        internal void Subscribe(ANetChannel channel, ushort data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast<TRequest>(TRequest data, int netCommand, int rpcId) where TRequest : class
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(string data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(int data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(uint data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(bool data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(float data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(double data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(decimal data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(long data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(ulong data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(byte data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(sbyte data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(short data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(ushort data, int netCommand, int rpcId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast<TRequest>(IEnumerable<ANetChannel> channels ,TRequest data, int netCommand, int rpcId)
            where TRequest : class
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, string data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, int data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, uint data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, bool data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, long data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, ulong data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, float data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, double data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, decimal data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, byte data, int netCommand)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, sbyte data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, char data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, short data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="netCommand"></param>
        /// <param name="rpcId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, ushort data, int netCommand, int rpcId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, rpcId);
            }
        }

        public void Dispose()
        {
            this.NService.Dispose();
            this.NService = null;
        }
    }
}
