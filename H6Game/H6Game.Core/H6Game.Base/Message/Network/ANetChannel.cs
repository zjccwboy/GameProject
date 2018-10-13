using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// 通讯管道抽象类
    /// </summary>
    public abstract class ANetChannel : IDisposable
    {
        /// <summary>
        /// 构造函数,TCP Connect
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        public ANetChannel(ANetService netService, Network network)
        {
            this.NetService = netService;
            Id = ChannelIdCreator.CreateId();
            this.Network = network;
            this.Network.Channel = this;
        }

        /// <summary>
        /// 构造函数,TCP Accept
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        public ANetChannel(ANetService netService)
        {
            this.NetService = netService;
            Id = ChannelIdCreator.CreateId();
            this.Network = new Network(this);
            this.Network.Channel = this;
        }

        /// <summary>
        /// 构造函数,KCP Connect
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        /// <param name="conv">KCP连接确认号Conv</param>
        public ANetChannel(ANetService netService, Network network, int conv)
        {
            this.NetService = netService;
            Id = conv;
            this.Network = network;
            this.Network.Channel = this;
        }

        /// <summary>
        /// 构造函数,KCP Accept
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        /// <param name="conv">KCP连接确认号Conv</param>
        public ANetChannel(ANetService netService, int conv)
        {
            this.NetService = netService;
            Id = conv;
            this.Network = new Network(this);
            this.Network.Channel = this;
        }

        protected ConcurrentDictionary<int, Action<Packet>> RpcActions { get; } = new ConcurrentDictionary<int, Action<Packet>>();

        public int Id { get; set; }
        public Session Session { get { return this.NetService.Session; } }
        public uint LastConnectTime { get; protected set; } = 0;
        public const uint ReConnectInterval = 3000;
        public Network Network { get; }

        private int rpcId;
        public int RpcId { get { if (rpcId == int.MaxValue) rpcId = 0; return ++rpcId; } }
        public PacketParser RecvParser { get; protected set; }
        public PacketParser SendParser { get; protected set; }
        public ANetService NetService { get; }
        public IPEndPoint RemoteEndPoint { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
        public uint LastReceivedTime { get; protected set; } = TimeUitls.Now();
        public uint LastSendTime { get; protected set; } = TimeUitls.Now();
        public Action<Packet> OnReceive { get; set; }
        public Action<ANetChannel, SocketError> OnError { get; set; }
        public Action<ANetChannel> OnConnect { get; set; }
        public Action<ANetChannel> OnDisConnect { get; set; }
        public bool Connected { get; set; }
        public abstract void StartConnecting();
        public abstract void DisConnect();
        public abstract void StartSend();
        public abstract void StartRecv();

        public void AddRpcPacket(int rpcId, Action<Packet> recvAction)
        {
            this.RpcActions[rpcId] = recvAction;
        }

        public void SendHeartbeat()
        {
            //Log.Debug($"发送心跳包->{this.RemoteEndPoint}.", LoggerBllType.System);
            this.LastSendTime = TimeUitls.Now();
            this.SendParser.Packet.IsHeartbeat = true;
            this.SendParser.Packet.WriteBuffer();
        }

        protected void HandleReceive(Packet packet)
        {
            LastReceivedTime = TimeUitls.Now();
            if (packet.IsHeartbeat)
            {
                //Log.Debug($"接收到客户端:{this.RemoteEndPoint}心跳包.", LoggerBllType.System);
                return;
            }

            if (this.NetService.ServiceType == NetServiceType.Server)
            {
                OnReceive?.Invoke(packet);
                return;
            }

            if (packet.IsRpc)
            {
                if (RpcActions.TryRemove(packet.RpcId, out Action<Packet> action))
                    action(packet);
            }
            else
            {
                OnReceive?.Invoke(packet);
            }
        }

        public void Dispose()
        {
            DisConnect();
        }
    }
}