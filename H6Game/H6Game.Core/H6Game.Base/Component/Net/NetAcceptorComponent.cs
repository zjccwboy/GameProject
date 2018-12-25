using H6Game.Base.Logger;
using H6Game.Base.Message;
using System;
using System.Net;

namespace H6Game.Base.Component
{
    [ComponentEvent(EventType.Awake)]
    public class NetAcceptorComponent : BaseComponent
    {
        private ProtocalType ProtocalType { get; set; }
        private IPEndPoint EndPoint { get; }
        private string HttpPrefixed { get; }

        public Network Network { get; private set; }
        public Action<Network> OnConnect { get; set; }
        public Action<Network> OnDisconnect { get; set; }
        
        public NetAcceptorComponent(IPEndPoint endPoint, ProtocalType protocalType)
        {
            this.EndPoint = endPoint;
            this.ProtocalType = protocalType;
        }

        public NetAcceptorComponent(string httpPrefixed, ProtocalType protocalType)
        {
            this.HttpPrefixed = httpPrefixed;
            this.ProtocalType = protocalType;
        }

        public override void Awake()
        {
            switch (this.ProtocalType)
            {
                case ProtocalType.Tcp:
                    Accept(this.EndPoint);
                    break;
                case ProtocalType.Kcp:
                    Accept(this.EndPoint);
                    break;
                case ProtocalType.Wcp:
                    Accept(this.HttpPrefixed);
                    break;
            }

        }

        public override void Update()
        {
            if (Network != null)
            {
                Network.Update();
            }
        }

        public override void Dispose()
        {
            this.Network.Dispose();
            base.Dispose();
        }

        private void Accept(IPEndPoint endPoint)
        {
            this.Network = Network.CreateAcceptor(endPoint, this.ProtocalType, network =>
            {
                this.OnConnect?.Invoke(network);
            }, network =>
            {
                this.OnDisconnect?.Invoke(network);
            }
            );
            Log.Info($"监听外网{this.ProtocalType.ToString()}端口:{endPoint.Port}成功.", LoggerBllType.System);
        }

        private void Accept(string httpPrefixed)
        {
            this.Network = Network.CreateWebSocketAcceptor(httpPrefixed, network =>
            {
                this.OnConnect?.Invoke(network);
            }, network =>
            {
                this.OnDisconnect?.Invoke(network);
            }
            );
            Log.Info($"监听外网{this.ProtocalType.ToString()} Prefixed:{this.HttpPrefixed}成功.", LoggerBllType.System);
        }
    }
}
