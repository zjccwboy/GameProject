using System.Net;
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
        public Action<Network> OnServerDisconnect { get; set; }

        /// <summary>
        /// 连接断开回调发生在服务端
        /// </summary>
        public Action<Network> OnServerConnect { get; set; }

        /// <summary>
        /// 连接断开回调发生在客户端
        /// </summary>
        public Action<Network> OnClientDisconnect { get; set; }

        /// <summary>
        /// 连接断开回调发生在客户端
        /// </summary>
        public Action<Network> OnClientConnect { get; set; }

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

            this.NService.OnServerConnect = (c) => { this.OnServerConnect?.Invoke(c.Network); };
            this.NService.OnServerDisconnect = (c) => { this.OnServerDisconnect?.Invoke(c.Network); };
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

            this.NService.OnClientDisconnect = (c) => { this.OnClientDisconnect?.Invoke(c.Network); };
            this.NService.OnClientConnect = (c) => { OnClientConnect?.Invoke(c.Network); };
            this.ConnectChannel = this.NService.Connect();
        }
        
        public void Update()
        {
            this.NService.Update();
        }
        
        internal void Send<TSender>(ANetChannel channel, TSender data, int netCommand, int rpcId)
        {
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data);
        }

        internal void Send(ANetChannel channel, object data, int netCommand, int rpcId, Type type)
        {
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo(data, type);
        }

        internal void Send(ANetChannel channel, int netCommand, int rpcId)
        {
            var packet = channel.SendParser.Packet;
            SetHead(packet, netCommand, rpcId);
            packet.WriteTo();
        }

        internal void Subscribe<TRequest>(ANetChannel channel, TRequest data, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, data, netCommand, rpcId);
        }

        internal void Subscribe(ANetChannel channel, Action<Packet> notificationAction, int netCommand)
        {
            var rpcId = channel.RpcId;
            channel.AddRpcPacket(rpcId, notificationAction);
            Send(channel, netCommand, rpcId);
        }

        internal void Broadcast<TRequest>(TRequest data, int netCommand)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, data, netCommand, 0);
            }
        }

        internal void Broadcast(int netCommand)
        {
            var channels = this.NService.Channels.Values;
            foreach (var channel in channels)
            {
                Send(channel, netCommand, 0);
            }
        }

        private void SetHead(Packet packet, int netCommand, int rpcId)
        {
            packet.NetCommand = netCommand;
            packet.RpcId = rpcId;
        }

        public void Dispose()
        {
            this.NService.Dispose();
            this.NService = null;
        }
    }
}
