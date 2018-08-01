using H6Game.Base.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private SysConfig config { get;} = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private Session connectSession;
        private IPEndPoint loginServerEndPoint;

        public bool IsConnected { get; private set; }

        public override void Start()
        {
            this.loginServerEndPoint = GetLoginServerEndPoint();
            this.Connecting(this.loginServerEndPoint);
        }

        public override void Update()
        {
            if(connectSession != null)
            {
                connectSession.Update();
            }
        }

        public void Connecting(IPEndPoint endPoint)
        {
            if(connectSession != null)
            {
                connectSession.Dispose();
            }
            connectSession = new Session(endPoint, ProtocalType.Kcp);
            connectSession.OnClientConnected = (c) => { this.IsConnected = c.Connected; };
            connectSession.OnClientDisconnected = (c) => { this.IsConnected = c.Connected; };
            connectSession.Connect();
        }

        private IPEndPoint GetLoginServerEndPoint()
        {
            var hostInfo = Dns.GetHostEntry(config.OuNetHost);
            IPAddress ipAddress = hostInfo.AddressList[0];
            var endPoint = new IPEndPoint(ipAddress, config.OuNetConfig.Port);
            return endPoint;
        }
    }
}
