﻿using System;
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
        private Kcp Kcp { get; set; }
        private byte[] CacheBytes { get; set; }
        private int MaxPSize { get;} = Kcp.IKCP_MTU_DEF - Kcp.IKCP_OVERHEAD;
        private uint LastCheckTime { get; set; } = TimeUitls.Now();
        private Socket NetSocket { get; set; }

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
        }

        public void InitKcp()
        {
            Kcp = new Kcp((uint)this.Id, this);
            Kcp.SetOutput(this.Output);
            Kcp.NoDelay(1, 10, 2, 1);  //fast
            //this.SendParser = this.SendParser ?? new PacketParser(1400);
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

                if (Connected)
                    return;

                this.SendParser = this.SendParser ?? new PacketParser(1400);
                ConnectSender.SendSYN(this.SendParser.Packet, this.NetSocket, this.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
            }
        }

        /// <summary>
        /// 处理KCP接收数据
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="lenght"></param>
        public void HandleRecv(byte[] bytes, int offset, int lenght)
        {
            CacheBytes = bytes;
            this.LastRecvTime = TimeUitls.Now();
            this.Kcp.Input(bytes, offset, lenght);
        }

        /// <summary>
        /// 接收与解析数据包
        /// </summary>
        public override void StartRecv()
        {
            this.TimeNow = TimeUitls.Now();
            while (true)
            {
                int n = Kcp.PeekSize();
                if (n == 0)
                    return;

                int count = this.Kcp.Recv(CacheBytes, 0, CacheBytes.Length);
                if (count <= 0)
                    return;

                this.RecvParser = this.RecvParser ?? new PacketParser(1400);
                RecvParser.WriteBuffer(CacheBytes, 0, count);

                while (true)
                {
                    try
                    {
                        var packet = this.RecvParser.Packet;
                        if (!RecvParser.TryRead())
                            break;

                        if (!packet.IsHeartbeat)
                        {
                            if (packet.IsRpc)
                            {
                                if (RpcDictionary.TryRemove(packet.RpcId, out Action<Packet> action))
                                    action(packet);
                                else
                                    OnReceive?.Invoke(packet);
                            }
                            else
                            {
                                OnReceive?.Invoke(packet);
                            }
                        }
                        else
                        {
                            Log.Debug($"接收到客户端:{this.RemoteEndPoint}心跳包.", LoggerBllType.System);
                        }

                        packet.BodyStream.SetLength(0);
                        packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
                    }
                    catch (Exception e)
                    {
                        DisConnect();
                        Log.Error(e, LoggerBllType.System);
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
            if (this.SendParser == null)
                return;

            if (!this.Connected)
                return;

            this.TimeNow = TimeUitls.Now();
            while (this.SendParser.Buffer.DataSize > 0)
            {
                this.LastSendTime = this.TimeNow;
                var offset = this.SendParser.Buffer.FirstReadOffset;
                var length = this.SendParser.Buffer.FirstDataSize;
                length = length > MaxPSize ? MaxPSize : length;
                Kcp.Send(this.SendParser.Buffer.First, offset, length);
                this.SendParser.Buffer.UpdateRead(length);
            }
            SetKcpSendTime();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public override void DisConnect()
        {
            try
            {
                if (!this.Connected)
                    return;

                this.SendParser.Clear();
                this.RecvParser.Clear();
                
                Connected = false;
                ConnectSender.SendFIN(this.SendParser.Packet, this.NetSocket, this.RemoteEndPoint, this.Id);
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
            }
            catch(Exception e)
            {
                DisConnect();
                Log.Error(e, LoggerBllType.System);
            }
        }

        /// <summary>
        /// 设置KCP重传时间
        /// </summary>
        private void SetKcpSendTime()
        {
            Kcp.Update(this.LastCheckTime);
            this.LastCheckTime = this.Kcp.Check(this.TimeNow);
        }
    }
}
