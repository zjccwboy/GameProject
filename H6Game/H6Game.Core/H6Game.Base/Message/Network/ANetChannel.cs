using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

        /// <summary>
        /// 通讯管道Id标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 会话类
        /// </summary>
        public Session Session { get { return this.NetService.Session; } }

        /// <summary>
        /// 最后连接时间
        /// </summary>
        public uint LastConnectTime { get; protected set; } = 0;

        /// <summary>
        /// 3秒重连
        /// </summary>
        public const uint ReConnectInterval = 3000;

        /// <summary>
        /// 网络类
        /// </summary>
        public Network Network { get; }

        /// <summary>
        /// RPC字典
        /// </summary>
        protected readonly ConcurrentDictionary<int, Action<Packet>> RpcActions = new ConcurrentDictionary<int, Action<Packet>>();

        private int rpcId;
        /// <summary>
        /// RPC请求Id生成器
        /// </summary>
        public int RpcId
        {
            get
            {
                Interlocked.Increment(ref rpcId);
                Interlocked.CompareExchange(ref rpcId, 1, int.MaxValue);
                return rpcId;
            }
        }

        /// <summary>
        /// 接收包缓冲区解析器
        /// </summary>
        public PacketParser RecvParser { get; set; }

        /// <summary>
        /// 发送包缓冲区解析器
        /// </summary>
        public PacketParser SendParser { get; set; }

        /// <summary>
        /// 网络服务类
        /// </summary>
        public ANetService NetService { get; set; }

        /// <summary>
        /// 远程IP端口
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; set; }

        /// <summary>
        /// 本地IP端口
        /// </summary>
        public IPEndPoint LocalEndPoint { get; set; }

        /// <summary>
        /// 接收最后一个数据包时间
        /// </summary>
        public uint LastReceivedTime { get; set; } = TimeUitls.Now();

        /// <summary>
        /// 最后发送时间
        /// </summary>
        public uint LastSendTime { get; set; } = TimeUitls.Now();

        /// <summary>
        /// 接收回调事件
        /// </summary>
        public Action<Packet> OnReceive;

        /// <summary>
        /// 错误回调事件
        /// </summary>
        public Action<ANetChannel, SocketError> OnError;

        /// <summary>
        /// 连接成功回调
        /// </summary>
        public Action<ANetChannel> OnConnect;

        /// <summary>
        /// 连接断开回调
        /// </summary>
        public Action<ANetChannel> OnDisConnect;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Connected { get; set; }

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        public abstract void StartConnecting();

        /// <summary>
        /// 断开连接
        /// </summary>
        public abstract void DisConnect();

        /// <summary>
        /// 开始发送
        /// </summary>
        public abstract void StartSend();

        /// <summary>
        /// 开始接收数据
        /// </summary>
        /// <returns></returns>
        public abstract void StartRecv();

        /// <summary>
        /// 添加一个发送数据包到发送缓冲区队列中
        /// </summary>
        /// <param name="rpcId"></param>
        /// <param name="recvAction"></param>
        public void AddRpcPacket(int rpcId, Action<Packet> recvAction)
        {
            this.RpcActions[rpcId] = recvAction;
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        public void SendHeartbeat()
        {
            Log.Debug($"发送心跳包->{this.RemoteEndPoint}.", LoggerBllType.System);
            this.LastSendTime = TimeUitls.Now();
            this.SendParser.Packet.IsHeartbeat = true;
            this.SendParser.Packet.WriteBuffer();
        }

        protected void HandleReceive(Packet packet)
        {
            LastReceivedTime = TimeUitls.Now();
            if (packet.IsHeartbeat)
            {
                Log.Debug($"接收到客户端:{this.RemoteEndPoint}心跳包.", LoggerBllType.System);
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