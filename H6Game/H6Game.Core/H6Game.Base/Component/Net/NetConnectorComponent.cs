using H6Game.Base.Config;
using H6Game.Base.Exceptions;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using System;

namespace H6Game.Base.Component
{
    public enum ConnectType
    {
        Proxy,
        Gate,
    }

    /// <summary>
    /// 客户端连接socket组件。
    /// 客户端连接网关流程：客户端会先连接到代理服务器，连接成功以后代理服务器会发送一个网关的地址给客户端，客户端会主动断开与
    /// 代理服务器的连接，再重新与网关连接。
    /// </summary>
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    [SingleCase]
    public sealed class NetConnectorComponent : BaseComponent
    {
        private NetConnectConfigEntity Config { get; set; }
        private ProtocalType ProtocalType { get; set; }

        /// <summary>
        /// Socket网络对象。
        /// </summary>
        public Network Network { get; private set; }

        /// <summary>
        /// Socket连接状态。
        /// </summary>
        public bool IsConnected { get { return this.Network.Channel.Connected; } }

        /// <summary>
        /// 最后连接时间。
        /// </summary>
        public DateTime LastConnectTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// Socket连接成功时回调。
        /// </summary>
        public Action<Network, ConnectType> OnConnect { get; set; }

        /// <summary>
        /// Socket连接断开时回调。
        /// </summary>
        public Action<Network, ConnectType> OnDisconnect { get; set; }

        public override void Awake()
        {
            this.Config = new NetConnectingConfig().Config;
            ProtocalType = Config.ProtocalType;
        }

        public override void Start()
        {
            //连接代理
            this.ConnectingProxy();
        }

        public override void Dispose()
        {
            Network.Dispose();
            base.Dispose();
        }

        public override void Update()
        {
            if (Network != null)
            {
                Network.Update();

                if (this.IsConnected)
                    this.LastConnectTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 重新连接，该方法会先断开当前与网关的连接，再通过代理服务连接到网关。
        /// </summary>
        public void ReConnecting()
        {
            if (this.Network != null)
                this.Network.Dispose();

            ConnectingProxy();
        }

        /// <summary>
        /// 连接到网关服务，连接成功后会断开与代理服务连接。 
        /// </summary>
        private async void ConnectingGate()
        {
            var message = await this.Network.CallMessageAsync<int,NetEndPointMessage>((int)this.ProtocalType, (ushort)SysNetCommand.GetGateEndPoint);
            if(message == null)
            {
                Log.Error("服务端未启动", LoggerBllType.System);
                return;
            }

            if (this.Config.ProtocalType == ProtocalType.Kcp || this.ProtocalType == ProtocalType.Tcp)
            {
                var endPoint = IPEndPointHelper.GetIPEndPoint(message);
                var proxyNetwork = this.Network.Channel;
                this.Network = Network.CreateConnector(endPoint, this.ProtocalType, network =>
                {
                    this.OnConnect?.Invoke(network, ConnectType.Gate);

                    //连接成功以后断开代理服务。
                    proxyNetwork.Disconnect();
                }, network =>
                {
                    this.OnDisconnect?.Invoke(network, ConnectType.Gate);
                });
            }
            else if(this.Config.ProtocalType == ProtocalType.Wcp)
            {
                var prefixed = message.WsPrefixed;
                var proxyNetwork = this.Network.Channel;
                this.Network = Network.CreateWebSocketConnector(prefixed, network =>
                {
                    this.OnConnect?.Invoke(network, ConnectType.Gate);

                    //连接成功以后断开代理服务。
                    proxyNetwork.Disconnect();
                },
                network =>
                {
                    this.OnDisconnect?.Invoke(network, ConnectType.Gate);
                });
            }
        }

        private void ConnectingProxy()
        {
            if(this.Config.ProtocalType == ProtocalType.Kcp || this.ProtocalType == ProtocalType.Tcp)
            {
                var proxyEndPoint = IPEndPointHelper.GetIPEndPoint(this.Config);
                this.Network = Network.CreateConnector(proxyEndPoint, this.ProtocalType, network =>
                {
                    if (this.Config.ProxyEnable)
                    {
                        ConnectingGate();
                        return;
                    }
                    this.OnConnect?.Invoke(network, ConnectType.Proxy);
                }, network =>
                {
                    this.OnDisconnect?.Invoke(network, ConnectType.Proxy);
                });
                return;
            }
            else if(this.Config.ProtocalType == ProtocalType.Wcp)
            {
                var prefixed = this.Config.Host;
                this.Network = Network.CreateWebSocketConnector(prefixed, network =>
                {
                    if (this.Config.ProxyEnable)
                    {
                        ConnectingGate();
                        return;
                    }
                    this.OnConnect?.Invoke(network, ConnectType.Proxy);
                },
                network =>
                {
                    this.OnDisconnect?.Invoke(network, ConnectType.Proxy);
                });
                return;
            }
            throw new ComponentException($"协议类型{this.Config.ProtocalType}不支持.");
        }
    }
}
