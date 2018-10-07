using System;
using System.Net;

namespace H6Game.Base
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
    public sealed class NetConnectingComponent : NetController
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
        /// 最后连接时间，该事件可以用于判断客户端掉线的时间，根据实际的情况，调用ReConnecting重连，这样做可以避免与当前客户端连接的
        /// 节点的服务挂调了，客户端一直没有重连不上的问题。
        /// </summary>
        public DateTime LastConnectTime { get; private set; } = DateTime.Now;

        /// <summary>
        /// Socket连接成功时回调。
        /// </summary>
        public Action<ANetChannel, ConnectType> OnConnected { get; set; }

        /// <summary>
        /// Socket连接断开时回调。
        /// </summary>
        public Action<ANetChannel, ConnectType> OnDisconnected { get; set; }

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
        /// 订阅代理服务发送的网关服务连接IP端口。
        /// </summary>
        /// <param name="endPointMessage"></param>
        [NetCommand(SysNetCommand.GetGateEndPoint)]
        public void GetGateIPEndPoint(NetEndPointMessage endPointMessage)
        {
            //断开代理与代理服务器连接
            this.Network.Dispose();

            var endPoint = IPEndPointHelper.GetIPEndPoint(endPointMessage);

            //连接网关
            ConnectingGate(endPoint);
        }

        private void ConnectingProxy()
        {
            var endPoint = IPEndPointHelper.GetIPEndPoint(this.Config);
            this.Network = Network.CreateConnecting(endPoint, this.ProtocalType, c =>
            {
                this.OnConnected?.Invoke(c, ConnectType.Proxy);
            }, c =>
            {
                this.OnDisconnected?.Invoke(c, ConnectType.Proxy);
            });
        }

        private void ConnectingGate(IPEndPoint endPoint)
        {
            this.Network = Network.CreateConnecting(endPoint, this.ProtocalType, c => 
            {
                this.OnConnected?.Invoke(c, ConnectType.Gate);
            }, c => 
            {
                this.OnDisconnected?.Invoke(c, ConnectType.Gate);
            });
        }
    }
}
