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
        public NetConfigEntity NetConfigEntity { get; private set; }
        private Session connectSession;
        public OutNetComponent()
        {
            this.NetConfigEntity = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        }

        private void HandleConnect()
        {
            var localIpaddress = IPAddress.Parse(this.NetConfigEntity.LocalEndPoint.IP);
            var localPort = this.NetConfigEntity.LocalEndPoint.Port;

            var remoteIpAddress = IPAddress.Parse(this.NetConfigEntity.RemoteEndPoint.IP);
            var remotePort = this.NetConfigEntity.RemoteEndPoint.Port;

            var connect = new Session(new IPEndPoint(remoteIpAddress, remotePort), ProtocalType.Kcp);
            var channel = connect.Connect();
            while (!channel.Connected)
            {
                Thread.Sleep(200);
            }
            this.connectSession = connect;
        }
    }
}
