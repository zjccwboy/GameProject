using System;
using System.Net;

namespace H6Game.Base
{
    public class WcpService : ANetService
    {
        private HttpListener Listener { get; set; }
        private string HttpPrefixed { get; set; }

        public WcpService (string httpPrefixed, Session session, NetServiceType serviceType) : base(session)
        {
            this.HttpPrefixed = httpPrefixed;
        }

        public override async void Accept()
        {
            if (this.Listener != null)
                return;

            this.Listener = new HttpListener();
            this.Listener.Prefixes.Add(this.HttpPrefixed);
            this.Listener.Start();
            var context = await this.Listener.GetContextAsync();
            HandleAccept(context);
        }

        private async void HandleAccept(HttpListenerContext context)
        {
            while (true)
            {
                var wsContext = await context.AcceptWebSocketAsync(null);
                var client = wsContext.WebSocket;

                var channel = new WcpChannel(this.HttpPrefixed, client, this)
                {
                    LocalEndPoint = context.Request.LocalEndPoint,
                    RemoteEndPoint = context.Request.RemoteEndPoint,
                };
                OnAccept(channel);
            }
        }

        public override ANetChannel Connect()
        {
            if (this.ClientChannel == null)
            {
                ClientChannel = new WcpChannel(this.HttpPrefixed, this)
                {
                    OnConnect = OnConnect
                };
                ClientChannel.StartConnecting();
            }
            return ClientChannel;
        }

        public override void Update()
        {
            if (this.ServiceType == NetServiceType.Client && ClientChannel != null)
                ClientChannel.StartConnecting();

            foreach (var channel in this.Channels.Values)
            {
                channel.StartSend();
            }
            this.CheckHeadbeat();
        }

        /// <summary>
        /// 处理接受连接成功回调
        /// </summary>
        /// <param name="channel"></param>
        private void OnAccept(ANetChannel channel)
        {
            try
            {
                Log.Debug($"接受客户端:{channel.RemoteEndPoint}连接成功.", LoggerBllType.System);
                channel.Connected = true;
                channel.OnDisConnect = HandleDisConnectOnServer;
                channel.OnReceive = (p) => { channel.Network.Dispatch(p); };
                this.AddChannel(channel);
                this.OnServerConnected?.Invoke(channel);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
            }
        }

        /// <summary>
        /// 处理连接成功回调
        /// </summary>
        /// <param name="channel"></param>
        private void OnConnect(ANetChannel channel)
        {
            try
            {
                Log.Debug($"连接服务端:{channel.RemoteEndPoint}成功.", LoggerBllType.System);
                channel.Connected = true;
                channel.OnDisConnect = HandleDisConnectOnClient;
                channel.OnReceive = (p) => { channel.Network.Dispatch(p); };
                this.AddChannel(channel);
                this.OnClientConnected?.Invoke(channel);
            }
            catch (Exception e)
            {
                Log.Error(e, LoggerBllType.System);
            }
        }

    }
}
