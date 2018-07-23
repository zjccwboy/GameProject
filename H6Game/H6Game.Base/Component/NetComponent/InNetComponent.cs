using H6Game.Base.Entity;
using System;
using System.Net;
using System.Threading;

namespace H6Game.Base
{
    public class InNetComponent : BaseComponent
    {
        public NetConfigEntity NetConfigEntity { get; private set; }

        private Session connectSession;
        private Session acceptSession;
        private Session outAcceptSession;
        private NetMapComponent mapComponent;

        public InNetComponent()
        {
            this.NetConfigEntity = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            this.mapComponent = SinglePool.Get<NetMapComponent>();
            this.HandleConnect();
        }

        private void HandleConnect()
        {
            var localIpaddress = IPAddress.Parse(this.NetConfigEntity.LocalEndPoint.IP);
            var localPort = this.NetConfigEntity.LocalEndPoint.Port;

            var remoteIpAddress = IPAddress.Parse(this.NetConfigEntity.RemoteEndPoint.IP);
            var remotePort = this.NetConfigEntity.RemoteEndPoint.Port;

            this.acceptSession = new Session(new IPEndPoint(localIpaddress, localPort), ProtocalType.Tcp);
            this.acceptSession.Accept();
            var connect = new Session(new IPEndPoint(remoteIpAddress, remotePort), ProtocalType.Tcp);
            var channel = connect.Connect();
            var retry = 0;
            while (!channel.Connected)
            {
                Thread.Sleep(200);
                retry++;
                if (retry > 10)
                {
                    throw new Exception("分布式默认节点服务没有开启");
                }
            }
            this.connectSession = connect;
            this.connectSession.OnNetServiceDisconnected += (c) =>
            {
                this.NetConfigEntity.RemoteEndPoint = this.mapComponent.GetRemoteEndPoint();
                HandleConnect();
            };

            this.outAcceptSession = new Session(new IPEndPoint(localIpaddress, localPort), ProtocalType.Kcp);
            this.outAcceptSession.Accept();
        }
    }
}
