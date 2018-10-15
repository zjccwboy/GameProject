using System;
using System.Net.WebSockets;
using System.Threading;

namespace H6Game.Base
{
    /// <summary>
    /// WebSocket通讯管道
    /// </summary>
    public class WcpChannel : ANetChannel
    {
        public string HttpPrefixed { get;}
        private WebSocket NetSocket { get; set; }

        /// <summary>
        /// 发送状态机
        /// </summary>
        private bool IsSending { get; set; }

        public WcpChannel(string httpPrefixed, ANetService netService, Network network) : base(netService, network)
        {
            this.HttpPrefixed = httpPrefixed;
            this.RecvParser = ParserStorage.GetParser();
            this.SendParser = ParserStorage.GetParser();
        }


        public WcpChannel(string httpPrefixed, WebSocket socket, ANetService netService) : base(netService)
        {
            this.HttpPrefixed = httpPrefixed;
            this.NetSocket = socket;
            this.RecvParser = ParserStorage.GetParser();
            this.SendParser = ParserStorage.GetParser();
        }

        public override async void StartConnecting()
        {
            try
            {
                if (this.Connected)
                    return;

                var now = TimeUitls.Now();
                if (now - this.LastConnectTime < ReConnectInterval)
                    return;

                this.LastConnectTime = now;

                this.NetSocket = new ClientWebSocket();
                var ctk = new CancellationTokenSource();
                await (this.NetSocket as ClientWebSocket).ConnectAsync(new Uri(this.HttpPrefixed), ctk.Token);

                //ConnectAsync为多线程异步，需要放到主线程中执行
                ThreadCallbackContext.Instance.Post(OnConnectComplete, this);

            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
            }
        }

        private void OnConnectComplete(object o)
        {
            this.Connected = true;
            this.OnConnect?.Invoke(o as WcpChannel);
        }

        public override async void StartSend()
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

            try
            {
                var segment = new ArraySegment<byte>(SendParser.Buffer.First, SendParser.Buffer.FirstReadOffset, SendParser.Buffer.FirstDataSize);
                this.SendParser.Buffer.UpdateRead(SendParser.Buffer.FirstDataSize);
                await NetSocket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);

                //SendAsync为多线程异步，需要放到主线程中执行
                ThreadCallbackContext.Instance.Post(OnSendComplete, null);
            }
            catch (Exception e)
            {
                this.DisConnect();
            }
        }

        private void OnSendComplete(object o)
        {
            this.LastSendTime = TimeUitls.Now();
            this.IsSending = false;
        }

        private readonly AutoResetEvent AutoReset = new AutoResetEvent(false);
        public override async void StartRecv()
        {
            while (!CancellationToken.None.IsCancellationRequested)
            {
                try
                {
                    var segment = new ArraySegment<byte>(this.RecvParser.Buffer.Last, this.RecvParser.Buffer.LastWriteOffset, this.RecvParser.Buffer.LastCapacity);
                    var result = await this.NetSocket.ReceiveAsync(segment, CancellationToken.None);
                    if (result.Count == 0)
                    {
                        this.DisConnect();
                        return;
                    }
                    //ReceiveAsync为多线程异步，需要放到主线程中执行
                    ThreadCallbackContext.Instance.Post(OnRecvComplete, result);
                    AutoReset.WaitOne();
                }
                catch
                {
                    this.DisConnect();
                    return;
                }
            }
        }

        private void OnRecvComplete(object o)
        {
            var result = o as WebSocketReceiveResult;
            this.RecvParser.Buffer.UpdateWrite(result.Count);
            try
            {
                while (true)
                {
                    if (!this.RecvParser.TryRead())
                        break;

                    this.HandleReceive(this.RecvParser.Packet);
                    this.RecvParser.Packet.BodyStream.SetLength(0);
                    this.RecvParser.Packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
                }
            }
            finally
            {
                AutoReset.Set();
            }
        }

        public override void DisConnect()
        {
            if (!this.Connected)
                return;

            if (NetSocket == null)
                return;

            this.Connected = false;

            this.OnDisConnect(this);

            //服务端连接断开把缓冲区丢进池
            if (this.NetService.ServiceType == NetServiceType.Server)
            {
                ParserStorage.Push(SendParser);
                ParserStorage.Push(RecvParser);
            }
            else
            {
                this.SendParser.Clear();
                this.RecvParser.Clear();
            }

            SendClose(this.NetSocket);
        }

        private async void SendClose(WebSocket netSocket)
        {
            var socket = netSocket;
            try
            {
                if (this.NetSocket.State == WebSocketState.Open)
                {
                    var ctk = new CancellationTokenSource();
                    await socket.CloseOutputAsync(WebSocketCloseStatus.Empty, null, ctk.Token);
                }
            }
            catch { }
            finally
            {
                socket.Dispose();
                socket = null;
            }
        }
    }
}
