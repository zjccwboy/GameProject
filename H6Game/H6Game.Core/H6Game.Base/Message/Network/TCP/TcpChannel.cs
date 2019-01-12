using System;
using System.Net.Sockets;
using System.Net;
using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using H6Game.Base.SyncContext;

namespace H6Game.Base.Message
{
    /// <summary>
    /// TCP通讯管道
    /// </summary>
    public class TcpChannel : ANetChannel
    {
        private SocketAsyncEventArgs InArgs { get; set; }
        private SocketAsyncEventArgs OutArgs { get; set; }
        private Socket NetSocket { get; set; }

        /// <summary>
        /// 发送状态机
        /// </summary>
        private bool IsSending { get; set; }

        /// <summary>
        /// 接收状态机
        /// </summary>
        private bool IsReceiving { get; set; }

        /// <summary>
        /// 构造函数,Connect
        /// </summary>
        /// <param name="endPoint">Ip/端口</param>
        /// <param name="netService">通讯网络服务对象</param>
        /// <param name="network">网络类</param>
        public TcpChannel(IPEndPoint endPoint, ANetService netService, Network network) : base(netService, network)
        {
            this.RemoteEndPoint = endPoint;
            this.RecvParser = ParserStorage.GetParser();
            this.SendParser = ParserStorage.GetParser();
        }

        /// <summary>
        /// 构造函数,Accept
        /// </summary>
        /// <param name="endPoint">IP/端口</param>
        /// <param name="socket">TCP socket类</param>
        /// <param name="netService">通讯网络服务对象</param>
        public TcpChannel(IPEndPoint endPoint, Socket socket, ANetService netService) : base(netService)
        {
            this.LocalEndPoint = endPoint;
            this.RecvParser = ParserStorage.GetParser();
            this.SendParser = ParserStorage.GetParser();
            this.NetSocket = socket;
            this.InArgs = new SocketAsyncEventArgs();
            this.OutArgs = new SocketAsyncEventArgs();
            this.InArgs.Completed += OnComplete;
            this.OutArgs.Completed += OnComplete;
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    SynchronizationThreadContext.Instance.Post(this.OnConnectComplete, e);
                    break;
                case SocketAsyncOperation.Receive:
                    SynchronizationThreadContext.Instance.Post(this.OnRecvComplete, e);
                    break;
                case SocketAsyncOperation.Send:
                    SynchronizationThreadContext.Instance.Post(this.OnSendComplete, e);
                    break;
                case SocketAsyncOperation.Disconnect:
                    SynchronizationThreadContext.Instance.Post(this.OnDisconnectComplete, e);
                    break;
                default:
                    throw new NetworkException($"socket error: {e.LastOperation}");
            }
        }

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        public override void StartConnecting()
        {
            try
            {
                if (this.Connected)
                    return;

                var now = TimeUitls.Now();
                if (now - this.LastConnectTime < ANetChannel.ReConnectInterval)
                    return;

                this.LastConnectTime = now;

                if (this.NetSocket == null)
                {
                    this.NetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        NoDelay = true
                    };

                    this.InArgs = new SocketAsyncEventArgs();
                    this.OutArgs = new SocketAsyncEventArgs();
                    this.InArgs.Completed += OnComplete;
                    this.OutArgs.Completed += OnComplete;
                }

                this.OutArgs.RemoteEndPoint = this.RemoteEndPoint;
                if (this.NetSocket.ConnectAsync(this.OutArgs))
                    return;

                OnConnectComplete(this.OutArgs);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
            }
        }

        private void OnConnectComplete(object o)
        {
            if (this.NetSocket == null)
                return;

            if (!this.NetSocket.Connected)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
            if (e.SocketError != SocketError.Success)
            {
                this.OnError?.Invoke(this, SocketError.SocketError);
                return;
            }

            this.LocalEndPoint = this.NetSocket.LocalEndPoint as IPEndPoint;
            e.RemoteEndPoint = null;
            this.LastConnectTime = TimeUitls.Now();
            this.Connected = true;
            OnConnected?.Invoke(this);
        }

        /// <summary>
        /// 发送缓冲区队列中的数据(合并发送)
        /// </summary>
        /// <returns></returns>
        public override void StartSend()
        {
            if (!this.Connected)
            {
                this.IsSending = false;
                return;
            }

            if (this.SendParser.Buffer.DataSize <= 0)
            {
                this.IsSending = false;
                return;
            }

            if (this.IsSending)
                return;

            this.IsSending = true;

            this.OutArgs.SetBuffer(SendParser.Buffer.First, SendParser.Buffer.FirstReadOffset, SendParser.Buffer.FirstDataSize);
            try
            {
                if (this.NetSocket.SendAsync(this.OutArgs))
                    return;
            }
            catch (Exception e)
            {
                this.IsSending = false;
                Log.Error(e, LoggerBllType.System);
                this.Disconnect();
                return;
            }
            this.OnSendComplete(this.OutArgs);
        }

        private void OnSendComplete(object o)
        {
            this.LastSendTime = TimeUitls.Now();
            this.IsSending = false;

            if (this.NetSocket == null)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

            if (e.SocketError != SocketError.Success)
            {
                this.Disconnect();
                return;
            }

            this.SendParser.Buffer.UpdateRead(e.BytesTransferred);
            if (this.SendParser.Buffer.DataSize <= 0)
                return;

            this.StartSend();
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        public override void StartRecv()
        {
            if (!this.Connected)
            {
                this.IsReceiving = false;
                return;
            }

            if (this.IsReceiving)
                return;

            this.IsReceiving = true;

            try
            {
                this.InArgs.SetBuffer(RecvParser.Buffer.Last, RecvParser.Buffer.LastWriteOffset, RecvParser.Buffer.LastCapacity);
                if (this.NetSocket.ReceiveAsync(this.InArgs))
                    return;

                this.OnRecvComplete(this.InArgs);
            }
            catch (Exception e)
            {
                this.IsReceiving = false;
                Log.Error(e, LoggerBllType.System);
                Disconnect();
            }
        }

        private void OnRecvComplete(object o)
        {
            this.IsReceiving = false;
            if (this.NetSocket == null)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

            if (e.SocketError != SocketError.Success)
            {
                this.Disconnect();
                return;
            }

            if (e.BytesTransferred == 0)
            {
                this.Disconnect();
                return;
            }

            this.RecvParser.Buffer.UpdateWrite(e.BytesTransferred);
            while (true)
            {
                if (!this.RecvParser.TryRead())
                    break;

                this.HandleReceive(this.RecvParser.Packet);
                this.RecvParser.Packet.BodyStream.SetLength(0);
                this.RecvParser.Packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
        }

        public override void Disconnect()
        {
            if (!this.Connected)
                return;

            if (this.NetSocket == null)
                return;

            this.Connected = false;

            this.OnDisconnected?.Invoke(this);

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

            this.NetSocket.Close();
            this.NetSocket.Dispose();
            this.NetSocket = null;

            this.InArgs.Dispose();
            this.OutArgs.Dispose();
        }

        private void OnDisconnectComplete(object o)
        {
            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
            this.OnDisconnected?.Invoke(this);
        }
    }
}
