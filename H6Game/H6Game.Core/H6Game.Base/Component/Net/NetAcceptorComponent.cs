using System;

namespace H6Game.Base
{
    [ComponentEvent(EventType.Awake | EventType.Start)]
    [SingleCase]
    public class NetAcceptorComponent : BaseComponent
    {
        private NetConnectConfigEntity Config { get; set; }

        private ProtocalType ProtocalType { get; set; }

        public Network Network { get; private set; }

        public Action<Network> OnConnected { get; set; }

        public Action<Network> OnDisconnected { get; set; }


        public override void Awake()
        {
            this.Config = new NetConnectingConfig().Config;

            if (this.Config.ProtocalType == 0)
            {
                ProtocalType = ProtocalType.Tcp;
            }
            else if (this.Config.ProtocalType == 1)
            {
                ProtocalType = ProtocalType.Kcp;
            }
        }

        public override void Start()
        {
            Accept();
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

        private void Accept()
        {
            this.Network = Network.CreateAcceptor(IPEndPointHelper.GetIPEndPoint(this.Config), this.ProtocalType, network =>
            {
                this.OnConnected?.Invoke(network);
            }, network =>
            {
                this.OnDisconnected?.Invoke(network);
            }
            );
            Log.Info($"监听端口:{this.Config.Port}成功.", LoggerBllType.System);
        }
    }
}
