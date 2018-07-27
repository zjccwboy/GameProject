﻿using H6Game.Base.Entity;
using H6Game.Message;
using H6Game.Message.InNetMessage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace H6Game.Base
{
    public class InNetComponent : BaseComponent
    {
        private NetConfig config;
        private EndPointEntity defaultCenterEndPoint;
        private Dictionary<int, Session> inConnectSessions = new Dictionary<int, Session>();
        private Session inAcceptSession;
        private Session outAcceptSession;
        private NetMapComponent mapComponent;

        public InNetComponent()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
            this.mapComponent = SinglePool.Get<NetMapComponent>();

            this.defaultCenterEndPoint = config.InNetConfig.CenterEndPoint;

            var outEndPoint = GetOutEndPointMessage();
            HandleOutAccept(outEndPoint);

            var center = GetCenterEndPointMessage();
            var local = GetLocalEndPointMessage();
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

        public void BroadcastConnections(Session session, List<DistributedMessage> message)
        {
            if (!this.config.IsCenterServer)
            {
                return;
            }

            var bytes = message.ConvertToBytes();
            var packet = new Packet
            {
                MessageId = (int)MessageCMD.UpdateInNetonnections,
                Data = bytes,
            };
            session.Broadcast(packet);
        }

        public void UpdateConnections(List<DistributedMessage> messages)
        {
            foreach(var message in messages)
            {
                var hashCode = GetMessageHashCode(message);
                if (!this.inConnectSessions.ContainsKey(hashCode))
                {
                    AddSession(message);
                }
            }

            var messageHashKeys = messages.Select(a => GetMessageHashCode(a));
            var keys = this.inConnectSessions.Keys;
            foreach(var key in keys)
            {
                if (!messageHashKeys.Contains(key))
                {
                    RemoveSession(key);
                }
            }
        }

        private void AddSession(DistributedMessage message)
        {
            if (config.IsCenterServer)
            {
                return;
            }

            var hashCode = GetMessageHashCode(message);
            var localHashCode = GetLocalHashCode();
            //判断是否是本地服务，是排除掉
            if(hashCode == localHashCode)
            {
                return;
            }

            var centerHashCode = GetCenterHashCode();
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

            var localHashCode = GetLocalHashCode();
            //判断是否是本地服务，是排除掉
            if (key == localHashCode)
            {
                return;
            }

            var centerHashCode = GetCenterHashCode();
            //排除中心服务
            if(key == centerHashCode)
            {
                return;
            }

            if (inConnectSessions.TryGetValue(key, out Session session))
            {
                this.inConnectSessions.Remove(key);
                session.Dispose();
            }
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
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听端口:{message.Port}成功.");
            if (config.IsCenterServer)
            {
                LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"中心服务启动成功.");
            }
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
                this.mapComponent.Add(message);
                var localMessage = GetLocalEndPointMessage();
                session.Notice(c, new Packet
                {
                    MessageId = (int)MessageCMD.AddOneServer,
                    Data = localMessage.ConvertToBytes(),
                });
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                var messageRp = new DistributedMessage
                {
                    IP = c.RemoteEndPoint.Address.ToString(),
                    Port = c.RemoteEndPoint.Port,
                };

                this.mapComponent.Remove(messageRp);
                this.inConnectSessions.Remove(GetMessageHashCode(messageRp));
                if (config.IsCenterServer)
                {
                    return;
                }

                var messageHashCode = GetMessageHashCode(messageRp);
                var centerHashCode = GetCenterHashCode();
                //如果是中心服务挂掉，切换中心服务
                if(messageHashCode == centerHashCode)
                {
                    if (!this.mapComponent.TryGetCenterIpEndPoint(out DistributedMessage value))
                    {
                        LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前所有服务已经关闭.");
                        //服务全部挂调，重新开始等待默认中心服务
                        var defaulCenterMessage = GetEndPointMessage(defaultCenterEndPoint);
                        ConnectToCenter(defaulCenterMessage);
                        return;
                    }

                    var newCenterEndPoint = new EndPointEntity
                    {
                        IP = value.IP,
                        Port = value.Port,
                    };
                    //切换中心服务
                    this.config.InNetConfig.CenterEndPoint = newCenterEndPoint;
                    var centerMessage = GetEndPointMessage(newCenterEndPoint);
                    LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"切换中心服务器:{messageRp.IP}:{messageRp.Port} -> {newCenterEndPoint.IP}:{newCenterEndPoint.Port}.");
                    if (GetLocalHashCode() == GetMessageHashCode(centerMessage))
                    {
                        this.config.IsCenterServer = true;
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/ConnectToCenter", $"当前服务变为中心服务:{this.config.InNetConfig.LocalEndPoint.IP}:{this.config.InNetConfig.LocalEndPoint.Port}.");
                    }
                }
            };

            session.Connect();
            var hashCode = GetMessageHashCode(message);
            this.inConnectSessions[hashCode] = session;
        }

        private int GetCenterHashCode()
        {
            var message = GetCenterEndPointMessage();
            return GetMessageHashCode(message);
        }

        private int GetLocalHashCode()
        {
            var message = GetLocalEndPointMessage();
            return GetMessageHashCode(message);
        }

        private DistributedMessage GetCenterEndPointMessage()
        {
            var centerEndPoint = new DistributedMessage
            {
                Port = this.config.InNetConfig.CenterEndPoint.Port,
                IP = this.config.InNetConfig.CenterEndPoint.IP,
            };
            return centerEndPoint;
        }

        private DistributedMessage GetLocalEndPointMessage()
        {
            var localEndPoint = new DistributedMessage
            {
                Port = this.config.InNetConfig.LocalEndPoint.Port,
                IP = this.config.InNetConfig.LocalEndPoint.IP,
            };
            return localEndPoint;
        }

        private DistributedMessage GetOutEndPointMessage()
        {
            var outEndPoint = new DistributedMessage
            {
                Port = config.InNetConfig.OutNetListenPort,
                IP = "0.0.0.0",
            };
            return outEndPoint;
        }

        private DistributedMessage GetEndPointMessage(EndPointEntity endPointEntity)
        {
            return new DistributedMessage
            {
                IP = endPointEntity.IP,
                Port = endPointEntity.Port,
            };
        }

        private IPEndPoint GetIPEndPoint(DistributedMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }

        private int GetMessageHashCode(DistributedMessage messageRp)
        {
            var hashCode = $"{messageRp.IP}:{messageRp.Port}".GetHashCode();
            return hashCode;
        }
    }
}
