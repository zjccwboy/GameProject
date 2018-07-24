using H6Game.Base.Entity;
using H6Game.Message.InNetMessage;
using System;
using System.Net;

namespace H6Game.Base
{
    public class InNetComponent : BaseComponent
    {
        private NetConfig config;
        private Session inConnectSession;
        private Session inAcceptSession;
        private Session outAcceptSession;
        private NetMapComponent mapComponent;

        public InNetComponent()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            var centerEndPoint = new DistributedMessageRp
            {
                Port = this.config.InNetConfig.CenterEndPoint.Port,
                IP = this.config.InNetConfig.CenterEndPoint.IP,
            };
            this.mapComponent = SinglePool.Get<NetMapComponent>();
            this.ConnectToCenter(centerEndPoint);
        }

        public void HandleInAccept(DistributedMessageRp message)
        {
            if(message.Port > this.config.InNetConfig.MaxPort)
            {
                throw new Exception("TCP端口已经占满.");
            }

            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            var session = new Session(new IPEndPoint(ip, port), ProtocalType.Tcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleInAccept(message);
            }
            this.inAcceptSession = session;
        }

        public void HandleOutAccept(DistributedMessageRp message)
        {
            if (message.Port > this.config.InNetConfig.MaxPort)
            {
                throw new Exception("KCP端口已经占满.");
            }

            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            var session = new Session(new IPEndPoint(ip, port), ProtocalType.Kcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleOutAccept(message);
            }
            this.outAcceptSession = session;
        }

        public void ConnectToCenter(DistributedMessageRp message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            var session = new Session(new IPEndPoint(ip, port), ProtocalType.Tcp);
            if (!session.TryConnect(out ANetChannel channel))
            {
                if(!this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessageRp value))
                {
                    if(!TryConnect(out Session sessionVal))
                    {
                        HandleInAccept(message);
                        HandleOutAccept(message);
                        return;
                    }
                    session = sessionVal;
                }
            }
            this.inConnectSession = session;
            this.inConnectSession.OnNetServiceDisconnected = (c)=> 
            {
                this.mapComponent.Remove(new DistributedMessageRp
                {
                    IP = c.RemoteEndPoint.Address.ToString(),
                    Port = c.RemoteEndPoint.Port,
                });
            };
        }

        private bool TryConnect(out Session value)
        {
            var start = this.config.InNetConfig.MinPort;
            var end = this.config.InNetConfig.MaxPort;
            var ips = this.config.InNetConfig.IPList;
            foreach(var ip in ips)
            {
                for(var i = start; i<= end; i++)
                {
                    var port = i;
                    var ipstr = IPAddress.Parse(ip);
                    var session = new Session(new IPEndPoint(ipstr, port), ProtocalType.Tcp);
                    if (session.TryConnect(out ANetChannel channel))
                    {
                        value = session;
                        return true;
                    }
                }
            }
            value = null;
            return false;
        }
    }
}
