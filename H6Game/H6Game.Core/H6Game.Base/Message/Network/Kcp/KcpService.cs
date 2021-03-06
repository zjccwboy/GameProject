﻿using H6Game.Base.Logger;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base.Message
{
    /// <summary>
    /// KCP通讯服务
    /// </summary>
    public class KcpService : ANetService
    {
        private EndPoint ReuseEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private PacketParser ConnectParser { get; } = ParserStorage.GetParser(PacketParser.HeadSize);
        private IPEndPoint EndPoint { get; set; }
        private byte[] ReuseRecvBytes { get; } = new byte[1400];
        protected HashSet<int> Updates { get; } = new HashSet<int>();

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
            {
                if (this.Updates.Contains(channel.Id))
                {
                    channel.Update();
                }
                channel.CheckHeadbeat();
            }
            this.StartRecv();
        }

        public void AddUpdate(int channelId)
        {
            this.Updates.Add(channelId);
        }

        public void RemoveUpdate(int channelId)
        {
            this.Updates.Remove(channelId);
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
                Log.Warn(e, LoggerBllType.Network);
                return;
            }

            if (recvCount == PacketParser.HeadSize)
            {
                //握手处理
                ConnectParser.WriteBuffer(ReuseRecvBytes, 0, recvCount);
                var packet = this.ConnectParser.Packet;
                if (!ConnectParser.TryRead())
                {
                    Log.Error($"丢弃非法数据包:{this.Acceptor.RemoteEndPoint}.", LoggerBllType.Network);
                    ConnectParser.Flush();
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
                this.ConnectParser.Flush();
            }
            else
            {
                uint connectConv = BitConverter.ToUInt32(ReuseRecvBytes, 0);
                if (!this.Channels.TryGetValue(connectConv, out ANetChannel channel))
                    return;

                var kChannel = channel as KcpChannel;
                kChannel.Input(ReuseRecvBytes, recvCount);
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
            var conv = packet.RpcId;
            KcpChannel channel;
            if(this.ServiceType == NetServiceType.Client)
            {
                if (this.ClientChannel == null)
                    return;

                channel = this.ClientChannel as KcpChannel;
                channel.RemoteEndPoint = remoteEP as IPEndPoint;
                channel.Id = packet.RpcId;
                channel.OnConnected = OnConnect;
                ConnectSender.SendACK(this.ConnectParser.Packet, this.Acceptor, remoteEP, conv);
            }
            else
            {
                channel = new KcpChannel(socket, remoteEP as IPEndPoint, this, conv)
                {
                    OnConnected = OnAccept
                };
            }

            channel.InitKcp();
            channel.OnConnected?.Invoke(channel);
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
                channel.OnDisconnected?.Invoke(channel);
            }
        }   
    }
}
