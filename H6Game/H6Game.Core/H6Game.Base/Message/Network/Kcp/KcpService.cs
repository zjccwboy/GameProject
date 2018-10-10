using System;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// KCP通讯服务
    /// </summary>
    public class KcpService : ANetService
    {
        private readonly PacketParser ConnectParser = ParserStorage.GetParser(PacketParser.HeadSize);
        private readonly IPEndPoint EndPoint;
        private EndPoint ReuseEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private readonly byte[] ReuseRecvBytes = new byte[1400];

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endPoint">Ip/端口</param>
        /// <param name="session">会话类</param>
        /// <param name="network">网络类</param>
        /// <param name="serviceType">通讯服务类型</param>
        public KcpService(IPEndPoint endPoint, Session session, Network network, NetServiceType serviceType) : base(session, network)
        {
            this.ServiceType = serviceType;
            this.EndPoint = endPoint;
            this.ProtocalType = ProtocalType.Kcp;
        }
        
        /// <summary>
        /// 开始监听并接受连接请求
        /// </summary>
        /// <returns></returns>
        public override void Accept()
        {
            if (this.Acceptor != null)
                return;

            this.Acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if (this.ServiceType == NetServiceType.Server)
            {
#if WINDOWS
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                this.Acceptor.IOControl((int)SIO_UDP_CONNRESET, new[] { Convert.ToByte(false) }, null);
#endif
            }
            Acceptor.Bind(this.EndPoint);
        }

        /// <summary>
        /// 发送连接请求
        /// </summary>
        /// <returns></returns>
        public override ANetChannel Connect()
        {
            if(this.Acceptor == null)
            {
                this.Acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
#if WINDOWS
                uint IOC_IN = 0x80000000;
                uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                this.Acceptor.IOControl((int)SIO_UDP_CONNRESET, new[] { Convert.ToByte(false) }, null);
#endif
                this.ClientChannel = new KcpChannel(this.Acceptor, this.EndPoint, this, this.Network, 1000);
                this.ClientChannel.StartConnecting();
            }
            return this.ClientChannel;
        }

        /// <summary>
        /// 更新发送接收队列
        /// </summary>
        public override void Update()
        {
            if (this.ServiceType == NetServiceType.Client)
            {
                if(ClientChannel != null)
                    this.ClientChannel.StartConnecting();
            }

            foreach (var channel in this.Channels.Values)
                channel.StartSend();

            this.StartRecv();

            this.CheckHeadbeat();
        }

        /// <summary>
        /// 开始接收数据包
        /// </summary>
        public void StartRecv()
        {
            int recvCount = 0;
            try
            {
                if(this.Acceptor.Available == 0)
                    return;

                recvCount = this.Acceptor.ReceiveFrom(ReuseRecvBytes, SocketFlags.None, ref this.ReuseEndPoint);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
                return;
            }

            if (recvCount == PacketParser.HeadSize)
            {
                //握手处理
                ConnectParser.WriteBuffer(ReuseRecvBytes, 0, recvCount);
                var packet = this.ConnectParser.Packet;
                if (!ConnectParser.TryRead())
                {
                    Log.Error($"丢弃非法数据包:{this.Acceptor.RemoteEndPoint}.", LoggerBllType.System);
                    //丢弃非法数据包
                    ConnectParser.Buffer.Flush();
                    return;
                }
                if (packet.KcpProtocal == KcpNetProtocal.SYN)
                {
                    HandleSYN(this.Acceptor, this.ReuseEndPoint as IPEndPoint);
                }
                else if (packet.KcpProtocal == KcpNetProtocal.ACK)
                {
                    HandleACK(packet, this.Acceptor, this.ReuseEndPoint as IPEndPoint);
                }
                else if (packet.KcpProtocal == KcpNetProtocal.FIN)
                {
                    HandleFIN(packet, this.ReuseEndPoint as IPEndPoint);
                }
                this.ConnectParser.Clear();
            }
            else
            {
                uint connectConv = BitConverter.ToUInt32(ReuseRecvBytes, 0);
                if (!this.Channels.TryGetValue(connectConv, out ANetChannel channel))
                {
                    ConnectSender.SendFIN(this.ConnectParser.Packet, this.Acceptor, this.ReuseEndPoint, (int)connectConv);
                    return;
                }
                (channel as KcpChannel).HandleRecv(ReuseRecvBytes, 0, recvCount);
                channel.StartRecv();
            }

            this.StartRecv();
        }

        /// <summary>
        /// 处理客户端SYN连接请求
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="remoteEP"></param>
        private void HandleSYN(Socket socket, EndPoint remoteEP)
        {
            var conv = KcpConvIdCreator.CreateId();
            while (this.Channels.ContainsKey(conv))
            {
                conv = KcpConvIdCreator.CreateId();
            }
            ConnectSender.SendACK(this.ConnectParser.Packet, this.Acceptor, remoteEP, conv);
        }

        /// <summary>
        /// 处理连接请求ACK应答
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="socket"></param>
        /// <param name="remoteEP"></param>
        private void HandleACK(Packet packet, Socket socket, EndPoint remoteEP)
        {
            KcpChannel channel;
            if(this.ServiceType == NetServiceType.Client)
            {
                if (this.ClientChannel == null)
                    return;

                channel = this.ClientChannel as KcpChannel;
                channel.RemoteEndPoint = remoteEP as IPEndPoint;
                channel.Id = packet.NetCommand;
                channel.OnConnect = OnConnect;
                ConnectSender.SendACK(this.ConnectParser.Packet, this.Acceptor, remoteEP, packet.NetCommand);
            }
            else
            {
                channel = new KcpChannel(socket, remoteEP as IPEndPoint, this, this.Network, packet.NetCommand)
                {
                    OnConnect = OnAccept
                };
            }

            channel.InitKcp();
            channel.OnConnect?.Invoke(channel);
        }

        /// <summary>
        /// 处理连接断开FIN请求
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="remoteEP"></param>
        private void HandleFIN(Packet packet, EndPoint remoteEP)
        {
            if (this.Channels.TryGetValue(packet.NetCommand, out ANetChannel channel))
            {
                channel.Connected = false;
                channel.OnDisConnect?.Invoke(channel);
            }
        }   
    }
}
