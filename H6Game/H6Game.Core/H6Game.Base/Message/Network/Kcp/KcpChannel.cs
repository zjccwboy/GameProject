using H6Game.Base.Component;
using H6Game.Base.Logger;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace H6Game.Base.Message
{
    /// <summary>
    /// KCP网络连接协议
    /// </summary>
    public static class KcpNetProtocal
    {
        /// <summary>
        /// 连接请求
        /// </summary>
        public const byte SYN = 1;
        /// <summary>
        /// 连接请求应答
        /// </summary>
        public const byte ACK = 2;
        /// <summary>
        /// 断开连接请求
        /// </summary>
        public const byte FIN = 3;
    }

    /// <summary>
    /// KCP通讯管道
    /// </summary>
    public class KcpChannel : ANetChannel
    {
        private IntPtr Kcp { get; set; }
        private Socket NetSocket { get; set; }
        private byte[] CacheBytes { get; set; }
        private int MaxPSize { get; } = KCP.IKCP_MTU_DEF - KCP.IKCP_OVERHEAD;
        private uint LastCheckTime { get; set; } = TimeUitls.Now();
        private static KcpService Service { get; set; }
        private const uint UpdateInterval = 10;
        private event KcpOutput KcpOutputEvent = KcpOutput;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="netService">网络服务</param>
        /// <param name="network">网络类</param>
        /// <param name="connectConv">网络连接Conv</param>
        public KcpChannel(Socket socket, IPEndPoint endPoint, ANetService netService, Network network, int connectConv)
            : base(netService, network, connectConv)
        {
            this.RemoteEndPoint = endPoint;
            this.NetSocket = socket;
            this.RecvParser = ParserStorage.GetParser(1400);
            this.SendParser = ParserStorage.GetParser(1400);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="netService">网络服务</param>
        /// <param name="connectConv">网络连接Conv</param>
        public KcpChannel(Socket socket, IPEndPoint endPoint, ANetService netService, int connectConv)
            : base(netService, connectConv)
        {
            this.RemoteEndPoint = endPoint;
            this.NetSocket = socket;
            this.RecvParser = ParserStorage.GetParser(1400);
            this.SendParser = ParserStorage.GetParser(1400);
        }

        public void InitKcp()
        {
            this.CacheBytes = new byte[1400];
            this.Kcp = KCP.KcpCreate((uint)this.Id, new IntPtr(this.Id));
            KCP.KcpSetoutput(this.Kcp, KcpOutputEvent);
            KCP.KcpNodelay(this.Kcp, 1, 10, 1, 1);
            KCP.KcpWndsize(this.Kcp, 256, 256);
            KCP.KcpSetmtu(this.Kcp, 470);
            Service = this.NetService as KcpService;
        }

        public static int KcpOutput(IntPtr buf, int len, IntPtr kcp, IntPtr user)
        {
            var channelId = user.ToInt32();
            var channel = Service.Channels[channelId] as KcpChannel;
            channel.Output(buf, len, user);
            return len;
        }

        /// <summary>
        /// 模拟TCP三次握手连接服务端
        /// </summary>
        /// <returns></returns>
        public override void StartConnecting()
        {
            try
            {
                var now = TimeUitls.Now();
                if (now - this.LastConnectTime < ANetChannel.ReConnectInterval)
                    return;

                this.LastConnectTime = now;

                if (this.Connected)
                    return;

                ConnectSender.SendSYN(this.SendParser.Packet, this.NetSocket, this.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Log.Warn(e, LoggerBllType.Network);
            }
        }

        public override void Update()
        {
            this.StartSend();

            var now = TimeUitls.Now();
            if (now > this.LastCheckTime)
            {
                KCP.KcpUpdate(this.Kcp, now);
                Service.RemoveUpdate(this.Id);
                this.LastCheckTime = KCP.KcpCheck(this.Kcp, now);
            }
        }

        /// <summary>
        /// 开始发送KCP数据包
        /// </summary>
        /// <returns></returns>
        public override void StartSend()
        {
            if (!this.Connected)
                return;
            var now = TimeUitls.Now();
            while (this.SendParser.Buffer.DataSize > 0)
            {
                var offset = this.SendParser.Buffer.FirstReadOffset;
                var length = this.SendParser.Buffer.FirstDataSize;
                length = length > MaxPSize ? MaxPSize : length;
                KCP.KcpSend(this.Kcp, this.SendParser.Buffer.First, offset, length);
                this.SendParser.Buffer.UpdateRead(length);
            }
        }

        /// <summary>
        /// 处理KCP接收数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="lenght"></param>
        public void Input(byte[] bytes, int lenght)
        {
            KCP.KcpInput(this.Kcp, bytes, lenght);
            Service.AddUpdate(this.Id);
        }

        /// <summary>
        /// 接收与解析数据包
        /// </summary>
        public override void StartRecv()
        {
            while (true)
            {
                int n = KCP.KcpPeeksize(this.Kcp);
                if (n < 0)
                    return;

                int count = KCP.KcpRecv(this.Kcp, this.CacheBytes, n);
                if (count <= 0)
                    return;

                this.RecvParser.WriteBuffer(this.CacheBytes, 0, count);
                while (true)
                {
                    if (!this.RecvParser.TryRead())
                        return;

                    this.HandleReceive(this.RecvParser.Packet);
                    this.RecvParser.Packet.BodyStream.SetLength(0);
                    this.RecvParser.Packet.BodyStream.Seek(0, SeekOrigin.Begin);
                }
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public override void Disconnect()
        {
            if (!this.Connected)
                return;

            this.Connected = false;

            this.OnDisconnected?.Invoke(this);
            ConnectSender.SendFIN(this.SendParser.Packet, this.NetSocket, this.RemoteEndPoint, this.Id);

            //服务端连接断开把缓冲区丢进池
            if (this.NetService.ServiceType == NetServiceType.Server)
            {
                ParserStorage.Push(this.SendParser);
                ParserStorage.Push(this.RecvParser);
            }
            else
            {
                this.SendParser.Clear();
                this.RecvParser.Clear();
            }

            KCP.KcpRelease(this.Kcp);
            this.Kcp = IntPtr.Zero;
        }

        protected override void SetHead(Packet packet, ushort netCommand, int rpcId)
        {
            Service.AddUpdate(this.Id);
            base.SetHead(packet, netCommand, rpcId);
        }

        /// <summary>
        /// KCP发送回调函数
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="count"></param>
        /// <param name="user"></param>
        public void Output(IntPtr bytes, int count, IntPtr user)
        {
            try
            {
                this.LastSendTime = TimeUitls.Now();
                var buffer = this.SendParser.Packet.BodyStream.GetBuffer();
                Marshal.Copy(bytes, buffer, 0, count);
                this.NetSocket.SendTo(buffer, count, SocketFlags.None, this.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Log.Warn(e, LoggerBllType.Network);
                this.Disconnect();
            }
        }
    }
}
