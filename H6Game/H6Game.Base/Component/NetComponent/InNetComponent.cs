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
            var outEndPoint = new DistributedMessageRp
            {
                Port = this.config.OuNetConfig.CenterEndPoint.Port,
                IP = this.config.OuNetConfig.CenterEndPoint.IP,
            };
            HandleOutAccept(outEndPoint);

            var inEndPoint = new DistributedMessageRp
            {
                Port = this.config.InNetConfig.CenterEndPoint.Port,
                IP = this.config.InNetConfig.CenterEndPoint.IP,
            };
            this.mapComponent = SinglePool.Get<NetMapComponent>();
            if (config.IsCenterServer)
            {
                HandleInAccept(inEndPoint);
                return;
            }
            this.ConnectToCenter(inEndPoint);
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
                this.mapComponent.Remove(message);
                if(this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessageRp value))
                {
                    ConnectToCenter(value);
                }
                else
                {
                    if (!TryConnectAll(out Session sessionVal))
                    {
                        var centerEndPoint = new DistributedMessageRp
                        {
                            Port = this.config.InNetConfig.CenterEndPoint.Port,
                            IP = this.config.InNetConfig.CenterEndPoint.IP,
                        };
                        this.mapComponent.Add(centerEndPoint);
                        HandleInAccept(centerEndPoint);
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

                if (this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessageRp value))
                {
                    ConnectToCenter(value);
                }
                else
                {
                    var centerEndPoint = new DistributedMessageRp
                    {
                        Port = this.config.InNetConfig.CenterEndPoint.Port,
                        IP = this.config.InNetConfig.CenterEndPoint.IP,
                    };
                    HandleInAccept(centerEndPoint);
                    return;
                }
            };
            this.mapComponent.Add(message);
        }

        private bool TryConnectAll(out Session value)
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
