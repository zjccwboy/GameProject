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
        private string HttpPrefixed { get; set; }
        private WebSocket NetSocket { get; set; }

        /// <summary>
        /// 发送状态机
        /// </summary>
        private bool IsSending { get; set; }

        /// <summary>
        /// 接收状态机
        /// </summary>
        private bool IsReceiving { get; set; }


        public WcpChannel(string httpPrefixed, ANetService netService) : base(netService)
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
                if (now - this.LastConnectTime < ANetChannel.ReConnectInterval)
                    return;

                this.LastConnectTime = now;

                if (this.NetSocket == null)
                    this.NetSocket = new ClientWebSocket();

                await (this.NetSocket as ClientWebSocket).ConnectAsync(new Uri(this.HttpPrefixed), CancellationToken.None);

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
                await NetSocket.SendAsync(segment, WebSocketMessageType.Binary, true, CancellationToken.None);

                //SendAsync为多线程异步，需要放到主线程中执行
                ThreadCallbackContext.Instance.Post(OnSendComplete, null);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
                this.DisConnect();
            }
        }

        private void OnSendComplete(object o)
        {
            this.LastSendTime = TimeUitls.Now();
            this.IsSending = false;

            if (this.NetSocket == null)
                return;

            this.SendParser.Buffer.UpdateRead(SendParser.Buffer.FirstDataSize);

            if (this.SendParser.Buffer.DataSize > 0)
                this.StartSend();
        }

        public override async void StartRecv()
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
                var segment = new ArraySegment<byte>(this.RecvParser.Buffer.Last, this.RecvParser.Buffer.LastWriteOffset, this.RecvParser.Buffer.LastCapacity);
                var result = await this.NetSocket.ReceiveAsync(segment, CancellationToken.None);
                if (result.Count == 0)
                {
                    this.DisConnect();
                    return;
                }

                //ReceiveAsync为多线程异步，需要放到主线程中执行
                ThreadCallbackContext.Instance.Post(OnRecvComplete, result);
            }
            catch (Exception e)
            {
                this.IsReceiving = false;
                Log.Error(e, LoggerBllType.System);
                this.DisConnect();
            }
        }

        private void OnRecvComplete(object o)
        {
            this.IsReceiving = false;
            if (this.NetSocket == null)
                return;

            var result = o as WebSocketReceiveResult;
            this.RecvParser.Buffer.UpdateWrite(result.Count);

            while (true)
            {
                if (!this.RecvParser.TryRead())
                    break;

                this.HandleReceive(this.RecvParser.Packet);
                this.RecvParser.Packet.BodyStream.SetLength(0);
                this.RecvParser.Packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
            }

            this.StartRecv();
        }

        public override async void DisConnect()
        {
            try
            {
                if (!this.Connected)
                    return;

                if (NetSocket == null)
                    return;

                this.Connected = false;

                this.OnDisConnect(this);

                if (this.NetService.ServiceType == NetServiceType.Client)
                    await this.NetSocket.CloseAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None);
                else
                    await this.NetSocket.CloseOutputAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None);

            }
            finally
            {
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

                this.NetSocket.Dispose();
                this.NetSocket = null;
            }
        }
    }
}
