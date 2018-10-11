using System.Net;

namespace H6Game.Base
{
    public class WcpService : ANetService
    {
        private HttpListener Listener { get; set; }
        private string HttpPrefixed { get; set; }

        public WcpService (string httpPrefixed, Session session, Network network, NetServiceType serviceType) : base(session, network)
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
            while (true)
            {                
                var context = await this.Listener.GetContextAsync();

                //GetContextAsync多线程异步，放到主线程中执行回调
                ThreadCallbackContext.Instance.Post(HandleAccept, context);
            }
        }

        private async void HandleAccept(object obj)
        {
            var context = obj as HttpListenerContext;
            var wsContext = await context.AcceptWebSocketAsync(null);
            var client = wsContext.WebSocket;
            var channel = new WcpChannel(this.HttpPrefixed, client, this)
            {
                LocalEndPoint = context.Request.LocalEndPoint,
                RemoteEndPoint = context.Request.RemoteEndPoint,
            };
            OnAccept(channel);
        }

        public override ANetChannel Connect()
        {
            if (this.ClientChannel == null)
            {
                ClientChannel = new WcpChannel(this.HttpPrefixed, this, this.Network)
                {
                    OnConnect = OnConnect
                };
                ClientChannel.StartConnecting();
            }
            return this.ClientChannel;
        }

        public override void Update()
        {
            if (this.ServiceType == NetServiceType.Client && ClientChannel != null)
                ClientChannel.StartConnecting();

            foreach (var channel in this.Channels.Values)
            {
                channel.StartSend();
                channel.StartRecv();
            }
            this.CheckHeadbeat();
        }
    }
}
