using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using H6Game.Base.SyncContext;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 通讯管道抽象类
    /// </summary>
    public abstract class ANetChannel : SynchronizationThreadContextObject,IDisposable
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

        private uint LastCheckHeadbeatTime = TimeUitls.Now();
        private const uint KcpHeartbeatTime = 20 * 1000;
        private const uint TcpHeartbeatTime = 4 * 1000;
        private const uint WcpHeartbeatTime = 10 * 1000;
        public const uint ReConnectInterval = 3000;

        public Network Network { get; }

        private int rpcId;
        public int RpcId { get { if (rpcId == 0xFFFFFF) rpcId = 0; return ++rpcId; } }
        public PacketParser RecvParser { get; protected set; }
        public PacketParser SendParser { get; protected set; }
        public ANetService NetService { get; }
        public IPEndPoint RemoteEndPoint { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }
        public uint LastReceivedTime { get; protected set; } = TimeUitls.Now();
        public uint LastSendTime { get; protected set; } = TimeUitls.Now();
        public Action<Packet> OnReceive { get; set; }
        public Action<ANetChannel, SocketError> OnError { get; set; }
        public Action<ANetChannel> OnConnected { get; set; }
        public Action<ANetChannel> OnDisconnected { get; set; }
        public bool Connected { get; set; }
        public abstract void StartConnecting();
        public abstract void Disconnect();
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

        /// <summary>
        /// 心跳检测
        /// </summary>
        public void CheckHeadbeat()
        {
            var now = TimeUitls.Now();
            if (this.NetService.ServiceType == NetServiceType.Client)
            {

                if (!this.Connected)
                    return;

                var timeSendSpan = now - this.LastSendTime;
                if (timeSendSpan > HeartbeatTime)
                    this.SendHeartbeat();
            }
            else if (this.NetService.ServiceType == NetServiceType.Server)
            {
                var lastCheckSpan = now - this.LastCheckHeadbeatTime;
                if (lastCheckSpan < HeartbeatTime / 2)
                    return;
                LastCheckHeadbeatTime = now;

                var timeSpan = now - this.LastReceivedTime;
                if (timeSpan > HeartbeatTime + 2000) //允许2秒钟网络延迟
                {
                    Log.Debug($"{this.NetService.ProtocalType}客户端:{this.RemoteEndPoint}连接超时，心跳检测断开，心跳时长{timeSpan}.", LoggerBllType.Network);
                    this.Disconnect();
                }
            }
        }

        /// <summary>
        /// 心跳超时时长，TCP 4秒,KCP 20秒,WebSocket 10秒
        /// </summary>
        private uint HeartbeatTime
        {
            get
            {
                if (this.NetService.ProtocalType == ProtocalType.Kcp)
                {
                    return KcpHeartbeatTime;
                }
                else if (this.NetService.ProtocalType == ProtocalType.Tcp)
                {
                    return TcpHeartbeatTime;
                }
                else if (this.NetService.ProtocalType == ProtocalType.Wcp)
                {
                    return WcpHeartbeatTime;
                }
                throw new NetworkException("协议类型不存在。");
            }
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
            Disconnect();
        }
    }
}