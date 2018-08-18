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
        /// 构造函数
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        public ANetChannel(ANetService netService)
        {
            this.NetService = netService;
            Id = ChannelIdCreator.CreateId();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="netService">网络通讯服务对象</param>
        /// <param name="conv">KCP连接确认号Conv</param>
        public ANetChannel(ANetService netService, int conv)
        {
            this.NetService = netService;
            Id = conv;
        }

        /// <summary>
        /// 通讯管道Id标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Socket
        /// </summary>
        public Socket NetSocket { get; protected set; }

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
        /// 消息处理分发类
        /// </summary>
        public NetworkDispatcher Dispatcher { get; set; }

        /// <summary>
        /// RPC字典
        /// </summary>
        protected readonly ConcurrentDictionary<int, Action<Packet>> RpcDictionarys = new ConcurrentDictionary<int, Action<Packet>>();

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
        public uint LastRecvTime { get; set; } = TimeUitls.Now();

        /// <summary>
        /// 最后发送时间
        /// </summary>
        public uint LastSendTime { get; set; } = TimeUitls.Now();

        /// <summary>
        /// 当前操作时间
        /// </summary>
        public uint TimeNow = TimeUitls.Now();

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
        /// <param name="packet"></param>
        /// <param name="recvAction"></param>
        public void AddRpcPacket(Packet packet, Action<Packet> recvAction)
        {
            RpcDictionarys.TryAdd(packet.RpcId, recvAction);
        }

        public void Dispose()
        {
            DisConnect();
        }
    }
}