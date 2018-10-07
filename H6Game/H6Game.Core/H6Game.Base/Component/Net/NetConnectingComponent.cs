using System;
using System.Net;

namespace H6Game.Base
{
    /// <summary>
    /// 客户端连接socket组件
    /// </summary>
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    [SingleCase]
    public sealed class NetConnectingComponent : BaseComponent
    {
        private NetConnectConfigEntity Config { get; set; }
        private ProtocalType ProtocalType { get; set; }

        public Network Network { get; private set; }

        public bool IsConnected { get { return this.Network.Channel.Connected; } }

        public Action<ANetChannel> OnConnected { get; set; }

        public Action<ANetChannel> OnDisconnected { get; set; }

        public override void Awake()
        {
            this.Config = new NetConnectingConfig().Config;
            if(this.Config.ProtocalType == 0)
            {
                ProtocalType = ProtocalType.Tcp;
            }
            else if(this.Config.ProtocalType == 1)
            {
                ProtocalType = ProtocalType.Kcp;
            }
        }

        public override void Start()
        {
            this.Connecting(IPEndPointHelper.GetIPEndPoint(this.Config));
        }

        public override void Dispose()
        {
            Network.Dispose();
            base.Dispose();
        }

        public override void Update()
        {
            if(Network != null)
            {
                Network.Update();
            }
        }

        private void Connecting(IPEndPoint endPoint)
        {
            this.Network = Network.CreateConnecting(endPoint, this.ProtocalType, c => { this.OnConnected?.Invoke(c); }, c => { this.OnDisconnected?.Invoke(c); });
        }
    }
}
