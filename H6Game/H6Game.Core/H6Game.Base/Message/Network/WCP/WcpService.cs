using H6Game.Base.Logger;
using H6Game.Base.SyncContext;
using System;
using System.Net;
using System.Threading.Tasks;

namespace H6Game.Base.Message
{
    public class WcpService : ANetService
    {
        private HttpListener Listener { get; set; }
        private string HttpPrefixed { get; set; }

        public WcpService (string httpPrefixed, Session session, Network network, NetServiceType serviceType) : base(session, network)
        {
            this.HttpPrefixed = httpPrefixed;
            this.ServiceType = serviceType;
            this.ProtocalType = ProtocalType.Wcp;
        }

        public override async void Accept()
        {
            if (this.Listener != null)
                return;

            this.Listener = new HttpListener();
            this.Listener.Prefixes.Add(this.HttpPrefixed);
            this.Listener.Start();
            while (true)
            {
                try
                {
                    var context = await this.Listener.GetContextAsync();
                    var channel = await HandleAccept(context);
                    await this.SyncContext;
                    OnAcceptComplete(channel);
                }
                catch(Exception e)
                {
                    Log.Error(e, LoggerBllType.System);
                    return;
                }
            }
        }

        private async Task<ANetChannel> HandleAccept(HttpListenerContext context)
        {
            var wsContext = await context.AcceptWebSocketAsync(null);
            var client = wsContext.WebSocket;
            var channel = new WcpChannel(this.HttpPrefixed, client, this)
            {
                LocalEndPoint = context.Request.LocalEndPoint,
                RemoteEndPoint = context.Request.RemoteEndPoint,
            };
            return channel;
        }

        private void OnAcceptComplete(ANetChannel channel)
        {
            OnAccept(channel);
            channel.StartRecv();
        }

        public override void Update()
        {
            if (this.ServiceType == NetServiceType.Client && ClientChannel != null)
                ClientChannel.StartConnecting();

            foreach(var channel in this.Channels.Values)
            {
                channel.StartSend();
            }

            this.CheckHeadbeat();
        }

        public override ANetChannel Connect()
        {
            if (this.ClientChannel == null)
            {
                ClientChannel = new WcpChannel(this.HttpPrefixed, this, this.Network)
                {
                    OnConnected = c=> { OnConnect(c); c.StartRecv();} 
                };
                ClientChannel.StartConnecting();
            }
            return this.ClientChannel;
        }
    }
}
