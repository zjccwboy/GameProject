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
        private SocketAsyncEventArgs inArgs;
        private SocketAsyncEventArgs outArgs;

        /// <summary>
        /// 发送状态机
        /// </summary>
        private bool isSending;

        /// <summary>
        /// 接收状态机
        /// </summary>
        private bool isReceiving;

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
            this.inArgs = new SocketAsyncEventArgs();
            this.outArgs = new SocketAsyncEventArgs();
            this.inArgs.Completed += OnComplete;
            this.outArgs.Completed += OnComplete;
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
                    this.NetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    this.NetSocket.NoDelay = true;

                    this.inArgs = new SocketAsyncEventArgs();
                    this.outArgs = new SocketAsyncEventArgs();
                    this.inArgs.Completed += OnComplete;
                    this.outArgs.Completed += OnComplete;
                }

                this.outArgs.RemoteEndPoint = this.RemoteEndPoint;
                if (this.NetSocket.ConnectAsync(this.outArgs))
                    return;

                OnConnectComplete(this.outArgs);
            }
            catch (Exception e)
            {
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/StartConnecting", e);
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

            if (isSending)
                return;

            while (SendQueue.Count > 0)
            {
                var packet = SendQueue.Dequeue();
                SendParser.WriteBuffer(packet);
            }

            SendData();
        }

        private void SendData()
        {
            isSending = true;

            if (!Connected)
            {
                isSending = false;
                return;
            }

            if (SendParser.Buffer.DataSize <= 0)
            {
                isSending = false;
                return;
            }

            this.outArgs.SetBuffer(SendParser.Buffer.First, SendParser.Buffer.FirstReadOffset, SendParser.Buffer.FirstDataSize);
            try
            {
                if (this.NetSocket.SendAsync(this.outArgs))
                    return;
            }
            catch (Exception e)
            {
                isSending = false;
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/StartSend", e);
                DisConnect();
                return;
            }
            OnSendComplete(this.outArgs);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        public override void StartRecv()
        {
            try
            {
                if (isReceiving)
                    return;

                isReceiving = true;

                if (!Connected)
                {
                    isReceiving = false;
                    return;
                }

                this.inArgs.SetBuffer(RecvParser.Buffer.Last, RecvParser.Buffer.LastWriteOffset, RecvParser.Buffer.LastCapacity);
                if (this.NetSocket.ReceiveAsync(this.inArgs))
                {
                    return;
                }
                OnRecvComplete(this.inArgs);
            }
            catch (Exception e)
            {
                isReceiving = false;
                LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/StartRecv", e);
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

                this.inArgs.Dispose();
                this.outArgs.Dispose();
            }
            catch { }
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    OneThreadSynchronizationContext.Instance.Post(this.OnConnectComplete, e);
                    break;
                case SocketAsyncOperation.Receive:
                    OneThreadSynchronizationContext.Instance.Post(this.OnRecvComplete, e);
                    break;
                case SocketAsyncOperation.Send:
                    OneThreadSynchronizationContext.Instance.Post(this.OnSendComplete, e);
                    break;
                case SocketAsyncOperation.Disconnect:
                    OneThreadSynchronizationContext.Instance.Post(this.OnDisconnectComplete, e);
                    break;
                default:
                    throw new Exception($"socket error: {e.LastOperation}");
            }
        }

        private void OnConnectComplete(object o)
        {
            if (this.NetSocket == null)
                return;

            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
            if (e.SocketError != SocketError.Success)
            {
                this.OnError?.Invoke(this, SocketError.SocketError);
                return;
            }

            this.LastConnectTime = TimeUitls.Now();
            e.RemoteEndPoint = null;
            OnConnect?.Invoke(this);
            this.Connected = true;
        }

        private void OnDisconnectComplete(object o)
        {
            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;
            this.OnDisConnect?.Invoke(this);
        }

        private void OnRecvComplete(object o)
        {
            isReceiving = false;
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
                    if (!RecvParser.TryGetPacket(out Packet packet))
                        break;

                    LastRecvTime = TimeUitls.Now();
                    if (!packet.IsHeartbeat)
                    {
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
                }
                catch (Exception ex)
                {
                    DisConnect();
                    LogRecord.Log(LogLevel.Warn, $"{this.GetType()}/OnRecvComplete", ex);
                    return;
                }

            }
            this.StartRecv();
        }

        private void OnSendComplete(object o)
        {
            this.LastSendTime = TimeUitls.Now();

            isSending = false;

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
