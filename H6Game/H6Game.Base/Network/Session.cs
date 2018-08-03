using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private ANetService NService;
        private IPEndPoint EPoint;
        private ProtocalType PType;

        /// <summary>
        /// 客户端连接管道
        /// </summary>
        public ANetChannel ConnectChannel { get; private set; }

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
            try
            {
                OneThreadSynchronizationContext.Instance.Update();
                this.NService.Update();
            }
            catch(Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/Update", e);
            }
        }

        /// <summary>
        /// 通知消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="packet"></param>
        public void Notice(ANetChannel channel, Packet packet)
        {
            channel.SendQueue.Enqueue(packet);
        }

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="packet"></param>
        /// <param name="notificationAction"></param>
        public void Subscribe(ANetChannel channel, Packet packet, Action<Packet> notificationAction)
        {
            if (!channel.Connected)
                return;

            packet.RpcId = channel.RpcId;
            channel.AddRpcPacket(packet, notificationAction);
            channel.SendQueue.Enqueue(packet);
        }

        /// <summary>
        /// 给所有的Channel广播一条消息
        /// </summary>
        /// <param name="packet"></param>
        public void Broadcast(Packet packet)
        {
            var channels = this.NService.Channels.Values;
            foreach(var channel in channels)
                Notice(channel, packet);
        }

        /// <summary>
        /// 给所有的Channel广播一条消息
        /// </summary>
        /// <param name="packet"></param>
        public void Broadcast(IEnumerable<ANetChannel> channels, Packet packet)
        {
            foreach (var channel in channels)
                Notice(channel, packet);
        }

        public void Dispose()
        {
            var channels = this.NService.Channels.Values.ToList();
            foreach(var channel in channels)
            {
                if (channel.Connected)
                    channel.DisConnect();
            }
            this.NService = null;
        }
    }
}
