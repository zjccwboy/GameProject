using System;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base
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
        private Kcp Kcp;
        private byte[] CacheBytes;
        private int MaxPSize = Kcp.IKCP_MTU_DEF - Kcp.IKCP_OVERHEAD;
        private uint LastCheckTime = TimeUitls.Now();

        /// <summary>
        /// 构造函数,Connect
        /// </summary>
        /// <param name="socket">Socket</param>
        /// <param name="netService">网络服务</param>
        /// <param name="connectConv">网络连接Conv</param>
        public KcpChannel(Socket socket, IPEndPoint endPoint, ANetService netService, int connectConv) : base(netService, connectConv)
        {
            this.RemoteEndPoint = endPoint;
            this.NetSocket = socket;
            RecvParser = new PacketParser();
            SendParser = new PacketParser();
        }

        public void InitKcp()
        {
            Kcp = new Kcp((uint)this.Id, this);
            Kcp.SetOutput(this.Output);
            Kcp.NoDelay(1, 10, 2, 1);  //fast
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
                {
                    return;
                }

                this.LastConnectTime = now;

                if (Connected)
                {
                    return;
                }

                ConnectSender.SendSYN(this.NetSocket, this.RemoteEndPoint);
            }
            catch (Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/StartConnecting", e);
            }
        }

        /// <summary>
        /// 处理KCP接收数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="lenght"></param>
        public override void HandleRecv(byte[] bytes, int offset, int lenght)
        {
            CacheBytes = bytes;
            this.LastRecvTime = this.TimeNow;
            this.Kcp.Input(bytes, offset, lenght);
        }

        /// <summary>
        /// 该方法并没有用
        /// </summary>
        public override void StartRecv()
        {
            SetKcpSendTime();
            while (true)
            {
                int n = Kcp.PeekSize();
                if (n == 0)
                {
                    //LogRecord.Log(LogLevel.Error, "StartRecv", $"解包失败:{this.RemoteEndPoint}");
                    return;
                }

                int count = this.Kcp.Recv(CacheBytes, 0, CacheBytes.Length);
                if (count <= 0)
                {
                    return;
                }

                RecvParser.WriteBuffer(CacheBytes, 0, count);

                while (true)
                {
                    try
                    {
                        Packet packet = new Packet();
                        if (!RecvParser.TryGetPacket(ref packet))
                            break;

                        this.LastRecvTime = TimeUitls.Now();
                        if (!packet.IsHeartbeat)
                        {
                            //LogRecord.Log(LogLevel.Error, "StartRecv", $"收到远程电脑:{this.RemoteEndPoint}");
                            if (packet.IsRpc)
                            {
                                if (RpcDictionarys.TryRemove(packet.RpcId, out Action<Packet> action))
                                {
                                    //执行RPC请求回调
                                    action(packet);
                                }
                                else
                                {
                                    OnReceive?.Invoke(packet);
                                }
                            }
                            else
                            {
                                OnReceive?.Invoke(packet);
                            }
                        }
                        else
                        {
                            LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/HandleRecv", $"接收到客户端:{this.RemoteEndPoint}心跳包.");
                        }
                    }
                    catch (Exception e)
                    {
                        DisConnect();
                        LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/StartRecv", e);
                        return;
                    }
                }
            }            
        }        

        /// <summary>
        /// 开始发送KCP数据包
        /// </summary>
        /// <returns></returns>
        public override void StartSend()
        {
            this.TimeNow = TimeUitls.Now();

            while (SendQueue.Count > 0)
            {
                var packet = SendQueue.Dequeue();
                this.SendParser.WriteBuffer(packet);
            }

            if (Connected)
            {
                while (this.SendParser.Buffer.DataSize > 0)
                {
                    var offset = this.SendParser.Buffer.FirstReadOffset;
                    var length = this.SendParser.Buffer.FirstDataSize;
                    length = length > MaxPSize ? MaxPSize : length;
                    Kcp.Send(this.SendParser.Buffer.First, offset, length);
                    this.SendParser.Buffer.UpdateRead(length);
                    if(length >= MaxPSize)
                    {
                        SetKcpSendTime();
                    }
                }
                SetKcpSendTime();
            }
        }


        /// <summary>
        /// 断开连接
        /// </summary>
        public override void DisConnect()
        {
            try
            {
                Connected = false;
                ConnectSender.SendFIN(this.NetSocket, this.RemoteEndPoint, this.Id);
                OnDisConnect?.Invoke(this);
            }
            catch { }
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
                this.LastSendTime = TimeUitls.Now();
            }
            catch(Exception e)
            {
                DisConnect();
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/Output", e);
            }
        }

        /// <summary>
        /// 设置KCP重传时间
        /// </summary>
        private void SetKcpSendTime()
        {    
            if (this.TimeNow >= this.LastCheckTime)
            {
                Kcp.Update(this.TimeNow);
                this.LastCheckTime = this.Kcp.Check(this.TimeNow);
            }
        }
    }
}
