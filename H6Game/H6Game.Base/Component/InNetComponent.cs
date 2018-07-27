using H6Game.Base.Entity;
using H6Game.Message;
using H6Game.Message.InNetMessage;
using System;
using System.Collections.Generic;
using System.Net;

namespace H6Game.Base
{
    public class InNetComponent : BaseComponent
    {
        private NetConfig config;
        private Dictionary<int, Session> inConnectSessions = new Dictionary<int, Session>();
        private Session inAcceptSession;
        private Session outAcceptSession;
        private NetMapComponent mapComponent;

        public InNetComponent()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            var outEndPoint = new DistributedMessage
            {
                Port = config.InNetConfig.OutNetListenPort,
                IP = "0.0.0.0",
            };
            HandleOutAccept(outEndPoint);

            this.mapComponent = SinglePool.Get<NetMapComponent>();

            var centerEndPoint = new DistributedMessage
            {
                Port = this.config.InNetConfig.CenterEndPoint.Port,
                IP = this.config.InNetConfig.CenterEndPoint.IP,
            };
            var localEndPoint = new DistributedMessage
            {
                Port = this.config.InNetConfig.LocalEndPoint.Port,
                IP = this.config.InNetConfig.LocalEndPoint.IP,
            };
            if (this.config.IsCenterServer)
            {
                HandleInAccept(centerEndPoint);
            }

            else
            {
                HandleInAccept(localEndPoint);
            }
            this.ConnectToCenter(centerEndPoint);
        }

        public void Update()
        {
            if(outAcceptSession != null)
            {
                outAcceptSession.Update();
            }

            if(inAcceptSession != null)
            {
                inAcceptSession.Update();
            }

            var connections = inConnectSessions.Values;
            foreach(var connect in connections)
            {
                connect.Update();
            }
        }

        public void BroadcastConnections(Session session, List<DistributedMessage> message)
        {
            var bytes = message.ConvertToBytes();
            var packet = new Packet
            {
                MessageId = (int)MessageCMD.UpdateInNetonnections,
                Data = bytes,
            };
            session.Broadcast(packet);
        }

        private void HandleInAccept(DistributedMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleInAccept(message);
            }
            this.inAcceptSession = session;
#if SERVER
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听端口:{message.Port}成功.");
#endif

#if SERVER
            if (config.IsCenterServer)
            {
                LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"中心服务启动成功.");
            }
#endif
        }

        private void HandleOutAccept(DistributedMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleOutAccept(message);
            }
            this.outAcceptSession = session;
        }

        private void ConnectToCenter(DistributedMessage message)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);

            //注册连接成功回调
            session.OnClientConnected = (c) =>
            {
                session.Notice(c, new Packet
                {
                    MessageId = (int)MessageCMD.AddOneServer,
                    Data = message.ConvertToBytes(),
                });
            };

            if (!session.TryConnect(out ANetChannel channel))
            {
                this.mapComponent.Remove(message);
                if (!this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessage value))
                {
#if SERVER
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前所有服务已经关闭.");
#endif
                    return;
                }
                ConnectToCenter(value);
            }
            this.mapComponent.Add(message);
            var hashCode = GetHashCode(message);
            this.inConnectSessions.Add(hashCode, session);
            this.mapComponent.Add(message);

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                var messageRp = new DistributedMessage
                {
                    IP = c.RemoteEndPoint.Address.ToString(),
                    Port = c.RemoteEndPoint.Port,
                };

                this.mapComponent.Remove(messageRp);
                this.inConnectSessions.Remove(GetHashCode(messageRp));
                if (config.IsCenterServer)
                {
                    return;
                }
                if (!this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessage value))
                {
#if SERVER
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前所有服务已经关闭.");
#endif
                    return;
                }
                ConnectToCenter(value);
            };
        }

        private IPEndPoint GetIPEndPoint(DistributedMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }

        private int GetHashCode(DistributedMessage messageRp)
        {
            var hashCode = $"{messageRp.IP}:{messageRp.Port}".GetHashCode();
            return hashCode;
        }
    }
}
