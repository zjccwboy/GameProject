using System;
using System.Net;
using System.Text.RegularExpressions;

namespace H6Game.Base
{
    [Event(EventType.Awake | EventType.Update)]
    [SingletCase]
    public sealed class OuterComponent : BaseComponent
    {
        private OutNetConfig Config { get; set; }

        public Network Network { get; private set; }

        public bool IsConnected { get { return this.Network.Channel.Connected; } }

        public Action<ANetChannel> OnConnected { get; set; }

        public Action<ANetChannel> OnDisconnected { get; set; }

        public override void Awake()
        {
            this.Config = Game.Scene.AddComponent<OutNetConfigComponent>().OutNetConfig;
            this.Connecting(GetLoginServerEndPoint());
        }

        public override void Update()
        {
            if(Network != null)
            {
                Network.Update();
            }
        }

        public void ReConnect()
        {
            if (this.Network != null)
                this.Network.Session.Dispose();

            this.Network = null;

            Connecting(GetLoginServerEndPoint());
        }

        private void Connecting(IPEndPoint endPoint)
        {
            this.Network = Network.CreateConnecting(endPoint, ProtocalType.Kcp, c => { this.OnConnected?.Invoke(c); }, c => { this.OnDisconnected?.Invoke(c); });
        }

        private IPEndPoint GetLoginServerEndPoint()
        {
            const string ipOrDomainRegex = @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";
            const string ipRegex = @"((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)";

            var regex = new Regex(ipOrDomainRegex);
            var isMatch = regex.IsMatch(Config.OutNetHost);
            if (!isMatch)
                throw new Exception("连接服务端主机IP地址或者域名配置错误，请检查OutNetConfig.json文件配置。");

            regex = new Regex(ipRegex);
            var isIpadress = regex.IsMatch(Config.OutNetHost);
            IPAddress ipAddress = null;
            if(isIpadress)
            {
                ipAddress = IPAddress.Parse(Config.OutNetHost);
            }
            else
            {
                var hostInfo = Dns.GetHostEntry(Config.OutNetHost);
                ipAddress = hostInfo.AddressList[0];
            }
            var endPoint = new IPEndPoint(ipAddress, Config.Port);
            return endPoint;
        }
    }
}
