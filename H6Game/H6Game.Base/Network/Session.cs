using System.Net;
using System;
using System.Collections.Generic;
using System.Threading;

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
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send<T>(ANetChannel channel, T data, int messageCmd, int rpcId, int actorId) where T : class
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }


        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo();
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, string data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, int data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, uint data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, bool data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, long data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, ulong data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, float data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, double data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, decimal data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, byte data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, sbyte data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, char data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, short data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Send(ANetChannel channel, ushort data, int messageCmd, int rpcId, int actorId)
        {
            channel.SendParser = channel.SendParser ?? new PacketParser();
            var packet = channel.SendParser.Packet;
            SetHead(packet, messageCmd, rpcId, actorId);
            packet.WriteTo(data);
        }

        private void SetHead(Packet packet, int messageCmd, int rpcId, int actorId)
        {
            packet.MessageId = messageCmd;
            packet.RpcId = rpcId;
            packet.ActorId = actorId;
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe<T>(ANetChannel channel, T data, Action<Packet> notificationAction, int messageCmd, int actorId) where T:class
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, string data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, int data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name=""></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, uint data, Action<Packet> notificationAction, int messageCmd,int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, bool data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, long data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, ulong data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, float data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        internal void Subscribe(ANetChannel channel, double data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, decimal data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, byte data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, sbyte data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, char data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, short data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="data"></param>
        /// <param name="notificationAction"></param>
        /// <param name="messageCmd"></param>
        /// <param name="actorId"></param>
        internal void Subscribe(ANetChannel channel, ushort data, Action<Packet> notificationAction, int messageCmd, int actorId)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, messageCmd, rpcId, actorId);
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast<T>(T data, int messageCmd, int rpcId, int actorId)
            where T : class
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(string data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(int data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(uint data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(bool data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(float data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(double data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(decimal data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(long data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(ulong data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(byte data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(sbyte data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(short data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给所有客户端广播一条消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(ushort data, int messageCmd, int rpcId, int actorId)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast<T>(IEnumerable<ANetChannel> channels ,T data, int messageCmd, int rpcId, int actorId)
            where T : class
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, string data, int messageCmd, int rpcId, int actorId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, int data, int messageCmd, int rpcId, int actorId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, uint data, int messageCmd, int rpcId, int actorId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, bool data, int messageCmd, int rpcId, int actorId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, long data, int messageCmd, int rpcId, int actorId)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, ulong data
            , int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, float data
            , int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, double data
            , int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, decimal data, int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, byte data, int messageCmd, int rpcId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, sbyte data
            , int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, char data
            , int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, short data, int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        /// <summary>
        /// 给一组客户端广播一条消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channels"></param>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcId"></param>
        /// <param name="actorId"></param>
        internal void Broadcast(IEnumerable<ANetChannel> channels, ushort data, int messageCmd, int rpcId = 0, int actorId = 0)
        {
            foreach (var channel in channels)
            {
                Send(channel, data, messageCmd, rpcId, actorId);
            }
        }

        public void Dispose()
        {
            this.NService.Dispose();
            this.NService = null;
        }
    }
}
