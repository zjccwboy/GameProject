
using System.Net;
using System;
using System.Threading;
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
        private ANetService netService;
        private IPEndPoint endPoint;
        private ProtocalType protocalType;

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
            this.endPoint = endPoint;
            this.protocalType = protocalType;
        }

        /// <summary>
        /// 开始监听并接受客户端连接
        /// </summary>
        /// <param name="endPoint"></param>
        public bool Accept()
        {
            if(this.protocalType == ProtocalType.Tcp)
            {
                this.netService = new TcpService(this.endPoint, this, NetServiceType.Server);
            }
            else if(this.protocalType == ProtocalType.Kcp)
            {
                this.netService = new KcpService(this.endPoint, this, NetServiceType.Server);
            }

            this.netService.OnServerConnected = (c) => { this.OnServerConnected?.Invoke(c); };
            this.netService.OnServerDisconnected = (c) => { this.OnServerDisconnected?.Invoke(c); };

            return this.netService.Accept();
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public ANetChannel Connect()
        {
            if (this.protocalType == ProtocalType.Tcp)
            {
                this.netService = new TcpService(this.endPoint, this, NetServiceType.Client);
            }
            else if (this.protocalType == ProtocalType.Kcp)
            {
                this.netService = new KcpService(this.endPoint, this, NetServiceType.Client);
            }
            this.netService.OnClientDisconnected = (c) => { this.OnClientDisconnected?.Invoke(c); };
            this.netService.OnClientConnected = (c) => { OnClientConnected?.Invoke(c); };
            this.ConnectChannel = this.netService.Connect();
            return this.ConnectChannel;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public bool TryConnect(out ANetChannel netChannel)
        {
            if (this.protocalType == ProtocalType.Tcp)
            {
                this.netService = new TcpService(this.endPoint, this, NetServiceType.Client);
            }
            else if (this.protocalType == ProtocalType.Kcp)
            {
                this.netService = new KcpService(this.endPoint, this, NetServiceType.Client);
            }
            this.netService.OnClientDisconnected = (c) => { this.OnClientDisconnected?.Invoke(c); };
            this.netService.OnClientConnected = (c) => { OnClientConnected?.Invoke(c); };
            var channel = this.netService.Connect();
            var retry = 0;
            while (true)
            {
                Thread.Sleep(100);
                this.Update();
                if (retry > 10)//重试1秒钟
                {
                    netChannel = null;
                    return false;
                }

                if (channel.Connected)
                    break;

                retry++;
            }

            netChannel = channel;
            this.ConnectChannel = channel;
            return channel.Connected;
        }

        public void Update()
        {
            try
            {
                OneThreadSynchronizationContext.Instance.Update();
                this.netService.Update();
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
            if (!channel.Connected)
                return;

            this.netService.Enqueue(new SendTask
            {
                Channel = channel,
                Packet = packet,
            });
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
            channel.AddPacket(packet, notificationAction);
            this.netService.Enqueue(new SendTask
            {
                Channel = channel,
                Packet = packet,
            });
        }

        /// <summary>
        /// 给所有的Channel广播一条消息
        /// </summary>
        /// <param name="packet"></param>
        public void Broadcast(Packet packet)
        {
            var channels = this.netService.Channels.Values;
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
            var channels = this.netService.Channels.Values.ToList();
            foreach(var channel in channels)
            {
                if (channel.Connected)
                    channel.DisConnect();
            }
            this.netService = null;
        }
    }
}
