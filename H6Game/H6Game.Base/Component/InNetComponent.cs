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

        public NetMapManager InNetMapManager { get; } = new NetMapManager();
        public NetMapManager OutNetMapManager { get; } = new NetMapManager();

        public override void Start()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            this.defaultCenterEndPoint = config.InNetConfig.CenterEndPoint;

            var outEndPoint = this.config.GetOutMessage();
            HandleOutAccept(outEndPoint);

            var center = this.config.GetCenterMessage();
            var local = this.config.GetInMessage();
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
                if(!this.inConnectSessions.ContainsKey(message.HashCode()))
                {
                    AddSession(message);
                }
            }

            var messageHashKeys = messages.Select(a => a.HashCode());
            var keys = this.inConnectSessions.Keys;
            foreach(var key in keys)
            {
                if (!messageHashKeys.Contains(key))
                {
                    RemoveSession(key);
                }
            }
        }

        public void SendMessage(Session session, ANetChannel channel, int messageCmd, byte[] bytes)
        {
            session.Notice(channel, new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            });
        }

        private void AddSession(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var hashCode = message.HashCode();
            var localHashCode = this.config.GetInMessage().HashCode();
            //判断是否是本地服务，是排除掉
            if(hashCode == localHashCode)
            {
                return;
            }

            var centerHashCode = this.config.GetCenterMessage().HashCode();
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

            var localHashCode = this.config.GetInMessage().HashCode();
            //判断是否是本地服务，是排除掉
            if (key == localHashCode)
            {
                return;
            }

            var centerHashCode = this.config.GetCenterMessage().HashCode();
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
                    if (this.InNetMapManager.TryGetMappingMessage(c, out NetEndPointMessage value))
                    {
                        if (this.InNetMapManager.TryGetFromChannelId(c.Id, out NetEndPointMessage inMessage))
                        {
                            this.InNetMapManager.Remove(inMessage);
                            var entitys = this.InNetMapManager.ConnectEntities;
                            this.BroadcastConnections(session, entitys, (int)MessageCMD.UpdateInNetConnections);
                            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                        }

                        if (this.OutNetMapManager.TryGetFromChannelId(c.Id, out NetEndPointMessage outMessage))
                        {
                            this.OutNetMapManager.Remove(outMessage);
                            var entitys = this.OutNetMapManager.ConnectEntities;
                            this.BroadcastConnections(session, entitys, (int)MessageCMD.UpdateOutNetConnections);
                            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                        } 
                    }
                };
                this.InNetMapManager.Add(message);
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
                this.InNetMapManager.Add(message);
                var localMessage = this.config.GetInMessage();
                var outMessage = this.config.GetOutMessage();
                SendMessage(session, c, (int)MessageCMD.AddInServer, localMessage.ConvertToBytes());
                SendMessage(session, c, (int)MessageCMD.AddOutServer, outMessage.ConvertToBytes());
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                var messageRp = new NetEndPointMessage
                {
                    IP = c.RemoteEndPoint.Address.ToString(),
                    Port = c.RemoteEndPoint.Port,
                };

                this.InNetMapManager.Remove(messageRp);
                this.inConnectSessions.Remove(messageRp.HashCode());
                if (config.IsCenterServer)
                {
                    return;
                }

                var centerHashCode = this.config.GetCenterMessage().HashCode();
                //如果是中心服务挂掉，切换中心服务
                if (messageRp.HashCode() == centerHashCode)
                {
                    if (!this.InNetMapManager.TryGetCenterIpEndPoint(out NetEndPointMessage value))
                    {
                        LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前所有服务已经关闭.");
                        //服务全部挂调，重新开始等待默认中心服务
                        ConnectToCenter(defaultCenterEndPoint.GetMessage());
                        return;
                    }

                    var newCenterEndPoint = new EndPointEntity
                    {
                        IP = value.IP,
                        Port = value.Port,
                    };
                    //切换中心服务
                    this.config.InNetConfig.CenterEndPoint = newCenterEndPoint;
                    var centerMessage = newCenterEndPoint.GetMessage();
                    LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"切换中心服务器:{messageRp.IP}:{messageRp.Port} -> {newCenterEndPoint.IP}:{newCenterEndPoint.Port}.");
                    if (this.config.GetInMessage().HashCode() == centerMessage.HashCode())
                    {
                        this.config.IsCenterServer = true;
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"当前服务变为中心服务:{this.config.InNetConfig.LocalEndPoint.IP}:{this.config.InNetConfig.LocalEndPoint.Port}.");
                    }
                }
            };

            session.Connect();
            this.inConnectSessions[message.HashCode()] = session;
        }

        private IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
    }
}
