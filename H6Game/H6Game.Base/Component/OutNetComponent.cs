using H6Game.Base.Entity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private SysConfig Config { get;} = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private Session ConnectSession;
        private IPEndPoint LoginServerEndPoint;
        public Network1 Network { get { return this.ConnectSession.ConnectChannel.Handler.Network; } }

        public bool IsConnected { get; private set; }

        public OutNetComponent()
        {
            this.LoginServerEndPoint = GetLoginServerEndPoint();
        }

        public override void Start()
        {
            this.Connecting(this.LoginServerEndPoint);
        }

        public override void Update()
        {
            if(ConnectSession != null)
            {
                ConnectSession.Update();
            }
        }

        private void Connecting(IPEndPoint endPoint)
        {
            if(ConnectSession != null)
            {
                ConnectSession.Dispose();
            }
            ConnectSession = Network1.CreateSession(endPoint, ProtocalType.Kcp);
            ConnectSession.OnClientConnected = (c) => { this.IsConnected = c.Connected; };
            ConnectSession.OnClientDisconnected = (c) => { this.IsConnected = c.Connected; };
            ConnectSession.Connect();
        }

        private IPEndPoint GetLoginServerEndPoint()
        {
            var hostInfo = Dns.GetHostEntry(Config.OuNetHost);
            IPAddress ipAddress = hostInfo.AddressList[0];
            var endPoint = new IPEndPoint(ipAddress, Config.OuNetConfig.Port);
            return endPoint;
        }
    }
}
