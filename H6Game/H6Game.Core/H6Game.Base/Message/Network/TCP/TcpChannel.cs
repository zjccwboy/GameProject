using System;
using System.Net.Sockets;
using System.Net;

namespace H6Game.Base
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
        public TcpChannel(IPEndPoint endPoint, ANetService netService) : base(netService)
        {
            this.RemoteEndPoint = endPoint;
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
            this.NetSocket = socket;
            this.InArgs = new SocketAsyncEventArgs();
            this.OutArgs = new SocketAsyncEventArgs();
            this.InArgs.Completed += OnComplete;
            this.OutArgs.Completed += OnComplete;
        }

        /// <summary>
        /// 开始连接
        /// </summary>
        /// <returns></returns>
        public override void StartConnecting()
        {
            try
            {
                if (Connected)
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

        /// <summary>
        /// 发送缓冲区队列中的数据(合并发送)
        /// </summary>
        /// <returns></returns>
        public override void StartSend()
        {
            if (!Connected)
                return;

            if (IsSending)
                return;

            if (this.SendParser == null)
                return;

            SendData();
        }

        private void SendData()
        {
            IsSending = true;

            if (!Connected)
            {
                IsSending = false;
                return;
            }

            if (SendParser.Buffer.DataSize <= 0)
            {
                IsSending = false;
                return;
            }

            this.OutArgs.SetBuffer(SendParser.Buffer.First, SendParser.Buffer.FirstReadOffset, SendParser.Buffer.FirstDataSize);
            try
            {
                if (this.NetSocket.SendAsync(this.OutArgs))
                    return;
            }
            catch (Exception e)
            {
                IsSending = false;
                Log.Error(e, LoggerBllType.System);
                DisConnect();
                return;
            }
            OnSendComplete(this.OutArgs);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        public override void StartRecv()
        {
            try
            {
                if (IsReceiving)
                    return;

                IsReceiving = true;

                if (!Connected)
                {
                    IsReceiving = false;
                    return;
                }

                this.RecvParser = this.RecvParser ?? new PacketParser();

                this.InArgs.SetBuffer(RecvParser.Buffer.Last, RecvParser.Buffer.LastWriteOffset, RecvParser.Buffer.LastCapacity);
                if (this.NetSocket.ReceiveAsync(this.InArgs))
                {
                    return;
                }
                OnRecvComplete(this.InArgs);
            }
            catch (Exception e)
            {
                IsReceiving = false;
                Log.Error(e, LoggerBllType.System);
                DisConnect();
            }
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

                Connected = false;
                if (NetSocket == null)
                    return;

                OnDisConnect?.Invoke(this);
            }
            catch { }

            try
            {
                SendParser.Clear();
                RecvParser.Clear();

                NetSocket.Close();
                NetSocket.Dispose();
                NetSocket = null;
            }
            catch { }

            try
            {
                this.InArgs.Dispose();
                this.OutArgs.Dispose();
            }
            catch { }
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ThreadCallbackContext.Instance.Post(this.OnConnectComplete, e);
                    break;
                case SocketAsyncOperation.Receive:
                    ThreadCallbackContext.Instance.Post(this.OnRecvComplete, e);
                    break;
                case SocketAsyncOperation.Send:
                    ThreadCallbackContext.Instance.Post(this.OnSendComplete, e);
                    break;
                case SocketAsyncOperation.Disconnect:
                    ThreadCallbackContext.Instance.Post(this.OnDisconnectComplete, e);
                    break;
                default:
                    throw new NetworkException($"socket error: {e.LastOperation}");
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

            this.LastConnectTime = TimeUitls.Now();
            e.RemoteEndPoint = null;
            this.Connected = true;
            OnConnect?.Invoke(this);
        }

        private void OnDisconnectComplete(object o)
        {
            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
            this.OnDisConnect?.Invoke(this);
        }

        private void OnRecvComplete(object o)
        {
            IsReceiving = false;
            if (this.NetSocket == null)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

            if (e.SocketError != SocketError.Success)
            {
                DisConnect();
                return;
            }

            if (e.BytesTransferred == 0)
            {
                DisConnect();
                return;
            }

            RecvParser.Buffer.UpdateWrite(e.BytesTransferred);
            while (true)
            {
                try
                {
                    var packet = this.RecvParser.Packet;
                    if (!RecvParser.TryRead())
                        break;

                    LastRecvTime = TimeUitls.Now();
                    if (!packet.IsHeartbeat)
                    {
                        if (packet.IsRpc)
                        {
                            if (RpcDictionary.TryRemove(packet.RpcId, out Action<Packet> action))
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
                        Log.Debug($"接收到客户端:{this.RemoteEndPoint}心跳包.", LoggerBllType.System);
                    }

                    packet.BodyStream.SetLength(0);
                    packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
                }
                catch (Exception ex)
                {
                    DisConnect();
                    Log.Error(ex, LoggerBllType.System);
                    return;
                }

            }
            this.StartRecv();
        }

        private void OnSendComplete(object o)
        {
            this.LastSendTime = TimeUitls.Now();

            IsSending = false;

            if (this.NetSocket == null)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

            if (e.SocketError != SocketError.Success)
            {
                DisConnect();
                return;
            }

            this.SendParser.Buffer.UpdateRead(e.BytesTransferred);
            if (this.SendParser.Buffer.DataSize <= 0)
                return;

            this.SendData();
        }
    }
}
