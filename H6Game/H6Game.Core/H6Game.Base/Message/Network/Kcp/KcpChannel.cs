using H6Game.Base.Logger;
using System;
using System.Net;
using System.Net.Sockets;

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
        private Kcp Kcp { get; set; }
        private Socket NetSocket { get; set; }
        private byte[] CacheBytes { get; set; }
        private int MaxPSize { get;} = Kcp.IKCP_MTU_DEF - Kcp.IKCP_OVERHEAD;
        private uint LastCheckTime { get; set; } = TimeUitls.Now();

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
            this.Kcp = new Kcp((uint)this.Id, this);
            this.Kcp.SetOutput(this.Output);
            this.Kcp.NoDelay(1, 10, 2, 1);  //fast
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
                Log.Error(e, LoggerBllType.System);
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

            while (this.SendParser.Buffer.DataSize > 0)
            {
                var offset = this.SendParser.Buffer.FirstReadOffset;
                var length = this.SendParser.Buffer.FirstDataSize;
                length = length > MaxPSize ? MaxPSize : length;
                this.Kcp.Send(this.SendParser.Buffer.First, offset, length);
                this.SendParser.Buffer.UpdateRead(length);
            }
            this.SetKcpSendTime();
        }

        /// <summary>
        /// 处理KCP接收数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="lenght"></param>
        public void HandleRecv(byte[] bytes, int offset, int lenght)
        {
            this.CacheBytes = bytes;
            this.Kcp.Input(bytes, offset, lenght);
        }

        /// <summary>
        /// 接收与解析数据包
        /// </summary>
        public override void StartRecv()
        {
            while (true)
            {
                int n = this.Kcp.PeekSize();
                if (n == 0)
                    return;

                int count = this.Kcp.Recv(CacheBytes, 0, CacheBytes.Length);
                if (count <= 0)
                    return;

                this.RecvParser.WriteBuffer(CacheBytes, 0, count);

                while (true)
                {
                    if (!this.RecvParser.TryRead())
                        return;

                    this.HandleReceive(this.RecvParser.Packet);
                    this.RecvParser.Packet.BodyStream.SetLength(0);
                    this.RecvParser.Packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
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

            this.OnDisConnect?.Invoke(this);
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
        }

        /// <summary>
        /// KCP发送回调函数
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="count"></param>
        /// <param name="user"></param>
        private void Output(byte[] bytes, int count, object user)
        {
            try
            {
                this.NetSocket.SendTo(bytes, 0, count, SocketFlags.None, this.RemoteEndPoint);
            }
            catch(Exception e)
            {
                Log.Error(e, LoggerBllType.System);
                this.Disconnect();
            }
        }

        /// <summary>
        /// 设置KCP重传时间
        /// </summary>
        private void SetKcpSendTime()
        {
            var now = TimeUitls.Now();
            this.LastSendTime = now;
            this.Kcp.Update(this.LastCheckTime);
            this.LastCheckTime = this.Kcp.Check(now);
        }
    }
}
