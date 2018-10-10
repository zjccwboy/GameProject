using System;
using System.Net.WebSockets;
using System.Threading;

namespace H6Game.Base
{
    public class WcpChannel : ANetChannel
    {
        private string HttpPrefixed { get; set; }
        private WebSocket NetSocket { get; set; }
        private bool IsSending { get; set; }

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
                if (Connected)
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
            OnConnect?.Invoke(o as WcpChannel);
        }

        public override async void StartSend()
        {
            if (!Connected)
                return;

            if (IsSending)
                return;

            if (this.SendParser == null)
                return;

            if (SendParser.Buffer.DataSize == 0)
            {
                IsSending = false;
                return;
            }

            this.IsSending = true;
            this.LastSendTime = TimeUitls.Now();

            try
            {
                var segment = new ArraySegment<byte>(SendParser.Buffer.First, SendParser.Buffer.FirstReadOffset, SendParser.Buffer.FirstDataSize);
                await NetSocket.SendAsync(new ArraySegment<byte>(SendParser.Buffer.First), WebSocketMessageType.Binary, true, CancellationToken.None);
                SendParser.Buffer.UpdateRead(SendParser.Buffer.FirstReadOffset);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
                DisConnect();
                return;
            }
            finally
            {
                IsSending = false;
            }
        }

        public override async void StartRecv()
        {
            while (true)
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
                    this.RecvParser.Buffer.UpdateWrite(result.Count);

                    if (!RecvParser.TryRead())
                        continue;

                    HandleReceive(this.RecvParser.Packet);
                    this.RecvParser.Packet.BodyStream.SetLength(0);
                    this.RecvParser.Packet.BodyStream.Seek(0, System.IO.SeekOrigin.Begin);
                }
                catch(Exception e)
                {
                    DisConnect();
                    Log.Error(e, LoggerBllType.System);
                    return;
                }
            }
        }

        public override async void DisConnect()
        {
            try
            {
                if (!this.Connected)
                    return;

                if (NetSocket == null)
                    return;

                Connected = false;

                OnDisConnect(this);

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
                    SendParser.Clear();
                    RecvParser.Clear();
                }

                NetSocket.Dispose();
                NetSocket = null;
            }
        }
    }
}
