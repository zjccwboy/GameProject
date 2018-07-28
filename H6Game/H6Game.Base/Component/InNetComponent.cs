using H6Game.Base.Entity;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace H6Game.Base
{
    public class InNetComponent : BaseComponent
    {
        private SysConfig config;
        private EndPointEntity defaultCenterEndPoint;
        private readonly Dictionary<int, Session> inConnectSessions = new Dictionary<int, Session>();
        private Session inAcceptSession;
        private Session outAcceptSession;
        private InNetMapComponent mapComponent;

        public override void Start()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            this.mapComponent = SinglePool.Get<InNetMapComponent>();

            this.defaultCenterEndPoint = config.InNetConfig.CenterEndPoint;

            var outEndPoint = GetOutEndPointMessage();
            HandleOutAccept(outEndPoint);

            var center = this.config.InNetConfig.CenterEndPoint.DMessage;
            var local = this.config.InNetConfig.LocalEndPoint.DMessage;
            if (this.config.IsCenterServer)
            {
                HandleInAccept(center);
            }
            else
            {
                HandleInAccept(local);
            }
            this.ConnectToCenter(center);
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

        public void BroadcastConnections(Session session, List<NetEndPointMessage> message, int messageCmd)
        {
            if (!this.config.IsCenterServer)
            {
                return;
            }

            var bytes = message.ConvertToBytes();
            var packet = new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            };
            session.Broadcast(packet);
        }

        public void UpdateConnections(List<NetEndPointMessage> messages)
        {
            foreach(var message in messages)
            {
                if(!this.inConnectSessions.ContainsKey(message.HashCode))
                {
                    AddSession(message);
                }
            }

            var messageHashKeys = messages.Select(a => a.HashCode);
            var keys = this.inConnectSessions.Keys;
            foreach(var key in keys)
            {
                if (!messageHashKeys.Contains(key))
                {
                    RemoveSession(key);
                }
            }
        }

        private void AddSession(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var hashCode = message.HashCode;
            var localHashCode = this.config.InNetConfig.LocalEndPoint.DMessage.HashCode;
            //判断是否是本地服务，是排除掉
            if(hashCode == localHashCode)
            {
                return;
            }

            var centerHashCode = this.config.InNetConfig.CenterEndPoint.DMessage.HashCode;
            //排除中心服务
            if (hashCode == centerHashCode)
            {
                return;
            }

            if (this.inConnectSessions.ContainsKey(hashCode))
            {
                return;
            }

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);
            //注册连接成功回调
            session.OnClientConnected = (c) =>
            {
                this.inConnectSessions[hashCode] = session;
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                this.inConnectSessions.Remove(hashCode);
            };
            session.Connect();
        }

        private void RemoveSession(int key)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var localHashCode = this.config.InNetConfig.LocalEndPoint.DMessage.HashCode;
            //判断是否是本地服务，是排除掉
            if (key == localHashCode)
            {
                return;
            }

            var centerHashCode = this.config.InNetConfig.CenterEndPoint.DMessage.HashCode;
            //排除中心服务
            if (key == centerHashCode)
            {
                return;
            }

            if (inConnectSessions.TryGetValue(key, out Session session))
            {
                this.inConnectSessions.Remove(key);
                session.Dispose();
            }
        }

        private void HandleInAccept(NetEndPointMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleInAccept(message);
            }
            this.inAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听端口:{message.Port}成功.");
            if (config.IsCenterServer)
            {
                this.inAcceptSession.OnServerDisconnected = (c) =>
                {
                    if (this.mapComponent.TryGetMappingMessage(c, out NetEndPointMessage value))
                    {
                        this.mapComponent.Remove(value);
                        var entitys = this.mapComponent.ConnectEntities;
                        this.BroadcastConnections(session, entitys, (int) MessageCMD.UpdateInNetConnections);
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                    }
                };
                LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"中心服务启动成功.");
            }
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
            {
                message.Port++;
                HandleOutAccept(message);
            }
            this.outAcceptSession = session;
        }

        private void ConnectToCenter(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);

            //注册连接成功回调
            session.OnClientConnected = (c) =>
            {
                this.mapComponent.Add(message);
                var localMessage = this.config.InNetConfig.LocalEndPoint.DMessage;
                session.Notice(c, new Packet
                {
                    MessageId = (int)MessageCMD.AddOneServer,
                    Data = localMessage.ConvertToBytes(),
                });
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                var messageRp = new NetEndPointMessage
                {
                    IP = c.RemoteEndPoint.Address.ToString(),
                    Port = c.RemoteEndPoint.Port,
                };

                this.mapComponent.Remove(messageRp);
                this.inConnectSessions.Remove(messageRp.HashCode);
                if (config.IsCenterServer)
                {
                    return;
                }

                var centerHashCode = this.config.InNetConfig.CenterEndPoint.DMessage.HashCode;
                //如果是中心服务挂掉，切换中心服务
                if (messageRp.HashCode == centerHashCode)
                {
                    if (!this.mapComponent.TryGetCenterIpEndPoint(out NetEndPointMessage value))
                    {
                        LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前所有服务已经关闭.");
                        //服务全部挂调，重新开始等待默认中心服务
                        ConnectToCenter(defaultCenterEndPoint.DMessage);
                        return;
                    }

                    var newCenterEndPoint = new EndPointEntity
                    {
                        IP = value.IP,
                        Port = value.Port,
                    };
                    //切换中心服务
                    this.config.InNetConfig.CenterEndPoint = newCenterEndPoint;
                    var centerMessage = newCenterEndPoint.DMessage;
                    LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"切换中心服务器:{messageRp.IP}:{messageRp.Port} -> {newCenterEndPoint.IP}:{newCenterEndPoint.Port}.");
                    if (this.config.InNetConfig.LocalEndPoint.DMessage.HashCode == centerMessage.HashCode)
                    {
                        this.config.IsCenterServer = true;
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"当前服务变为中心服务:{this.config.InNetConfig.LocalEndPoint.IP}:{this.config.InNetConfig.LocalEndPoint.Port}.");
                    }
                }
            };

            session.Connect();
            this.inConnectSessions[message.HashCode] = session;
        }

        
        private NetEndPointMessage GetOutEndPointMessage()
        {
            var outEndPoint = new NetEndPointMessage
            {
                Port = config.OuNetConfig.Port,
                IP = "0.0.0.0",
            };
            return outEndPoint;
        }

        private IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
    }
}
