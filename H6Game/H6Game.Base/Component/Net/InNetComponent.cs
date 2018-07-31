﻿using H6Game.Base.Entity;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace H6Game.Base
{
    /// <summary>
    /// 内网分布式连接核心组件
    /// </summary>
    public class InNetComponent : BaseComponent
    {
        private SysConfig config { get; } = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private EndPointEntity defaultCenterEndPoint;
        private readonly Dictionary<int, Session> inConnectSessions = new Dictionary<int, Session>();
        private Session inAcceptSession;
        private Session outAcceptSession;
        private Session centerConnectSession;

        public NetMapManager InNetMapManager { get; } = new NetMapManager();
        public NetMapManager OutNetMapManager { get; } = new NetMapManager();

        public override void Start()
        {
            this.defaultCenterEndPoint = config.InNetConfig.CenterEndPoint;
            var center = this.config.GetCenterMessage();
            if (this.config.IsCenterServer)
            {
                HandleInAccept(center);
                return;
            }
            HandleInAccept(this.config.GetInMessage());
            this.Connecting(center);
            HandleOutAccept(this.config.GetOutMessage());
        }

        /// <summary>
        /// 更新内部分布式连接状态
        /// </summary>
        public override void Update()
        {
            if(outAcceptSession != null)
                outAcceptSession.Update();

            if (inAcceptSession != null)
                inAcceptSession.Update();

            if (centerConnectSession != null)
                centerConnectSession.Update();

            var connections = inConnectSessions.Values.ToList();
            foreach(var connect in connections)
            {
                connect.Update();
            }
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <returns></returns>
        public Task<T> CallMessage<T>(Session session, ANetChannel channel, byte[] bytes, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        {
            var tcs = new TaskCompletionSource<T>();
            var send = new Packet
            {
                IsCompress = isCompress,
                IsEncrypt = isEncrypt,
                MessageId = messageCmd,
                Data = bytes,
            };

            session.Subscribe(channel, send, (p) =>
            {
                var response = p.Data.ConvertToObject(typeof(T));
                tcs.TrySetResult((T)response);
            });
            return tcs.Task;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="channel"></param>
        /// <param name="messageCmd"></param>
        /// <param name="bytes"></param>
        /// <param name="rpcId"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        public void SendMessage(Session session, ANetChannel channel, int messageCmd, byte[] bytes, int rpcId = 0, bool isCompress = false, bool isEncrypt = false)
        {
            session.Notice(channel, new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            });
        }

        /// <summary>
        /// 广播内部消息
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="messageCmd"></param>
        public void BroadcastMessage(byte[] bytes, int messageCmd)
        {
            //中心服务只处理内部分布式连接管理消息
            if (this.config.IsCenterServer && !IsSysMessage(messageCmd))
                return;

            if (this.inAcceptSession == null)
                return;

            var packet = new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            };
            this.inAcceptSession.Broadcast(packet);
        }

        /// <summary>
        /// 广播内部通讯消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageCmd"></param>
        public void BroadcastConnections(List<NetEndPointMessage> message, int messageCmd)
        {
            if (!this.config.IsCenterServer)
                return;

            var bytes = message.ConvertToBytes();
            var packet = new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            };
            this.inAcceptSession.Broadcast(packet);
        }

        /// <summary>
        /// 发送一条消息给中心服务
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="messageCmd"></param>
        public void SendToCenter(byte[] bytes, int messageCmd)
        {
            var packet = new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            };
            this.centerConnectSession.Broadcast(packet);
        }

        /// <summary>
        /// 更新连接信息
        /// </summary>
        /// <param name="messages"></param>
        public void UpdateConnections(List<NetEndPointMessage> messages)
        {
            foreach(var message in messages)
            {
                if(!this.inConnectSessions.ContainsKey(message.GetHashCode()))
                    AddSession(message);
            }

            var messageHashKeys = messages.Select(a => a.GetHashCode());
            var keys = this.inConnectSessions.Keys.ToList();
            foreach (var key in keys)
            {
                if (!messageHashKeys.Contains(key))
                    RemoveSession(key);
            }

            //如果连接已经全部断开,清空映射表
            if (!inConnectSessions.Any())
            {
                this.InNetMapManager.Clear();
                this.OutNetMapManager.Clear();
                return;
            }
        }

        private void AddSession(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
                return;

            //判断是否是本地服务，是排除掉
            if(message == this.config.GetInMessage())
                return;

            //排除中心服务
            if (message == this.config.GetCenterMessage())
                return;

            //如果存在就不再创建新的Session
            if (this.inConnectSessions.ContainsKey(message.GetHashCode()))
                return;

            Connecting(message);
        }

        private void RemoveSession(int key)
        {
            if (config.IsCenterServer)
                return;

            //排除中心服务
            if (key == this.config.GetCenterMessage().GetHashCode())
                return;

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
                throw new Exception($"服务端口被占用.");
            }

            session.OnServerDisconnected = (c) =>
            {
                if (this.InNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage inMessage))
                {
                    this.InNetMapManager.Remove(inMessage);
                    var entitys = this.InNetMapManager.ConnectEntities;
                    if (this.config.IsCenterServer)
                    {
                        this.BroadcastConnections(entitys, (int)MessageCMD.UpdateInNetConnections);
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                    }
                    else
                    {
                        SendToCenter(inMessage.ConvertToBytes(), (int)MessageCMD.DeleteServer);                        
                    }                    
                }

                if (this.OutNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage outMessage))
                {
                    this.OutNetMapManager.Remove(outMessage);
                    var entitys = this.OutNetMapManager.ConnectEntities;
                    if (this.config.IsCenterServer)
                    {
                        this.BroadcastConnections(entitys, (int)MessageCMD.UpdateOutNetConnections);
                        LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                    }
                    else
                    {
                        SendToCenter(outMessage.ConvertToBytes(), (int)MessageCMD.DeleteOutServer);
                    }
                }
            };

            this.inAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听内网端口:{message.Port}成功.");

            if (this.config.IsCenterServer)
                LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"中心服务启动成功.");
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
                throw new Exception($"服务端口被占用.");

            this.outAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleOutAccept", $"监听外网端口:{message.Port}成功.");
        }

        private void Connecting(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
                return;

            if (this.inConnectSessions.ContainsKey(message.GetHashCode()))
                return;

            //不连接进程内的监听端口
            if(message == this.config.GetInMessage())
                return;

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);

            if (message == this.config.GetCenterMessage())
                this.centerConnectSession = session;
            else
                this.inConnectSessions[message.GetHashCode()] = session;

            //注册连接成功回调
            session.OnClientConnected = (c) =>
            {
                var localMessage = this.config.GetInMessage();
                var outMessage = this.config.GetOutMessage();
                SendToCenter(localMessage.ConvertToBytes(), (int)MessageCMD.AddInServer);
                SendToCenter(outMessage.ConvertToBytes(), (int)MessageCMD.AddOutServer);
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                if (message == this.config.GetCenterMessage())
                {
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前中心服务挂掉.");
                    return;
                }

                this.InNetMapManager.Remove(message);
                if (this.OutNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage outMessage))
                    this.OutNetMapManager.Remove(outMessage);

                this.inConnectSessions.Remove(message.GetHashCode());
            };
            session.Connect();
        }

        private bool IsSysMessage(int messageCmd)
        {
            return messageCmd == (int)MessageCMD.AddInServer
                || messageCmd == (int)MessageCMD.DeleteServer
                || messageCmd == (int)MessageCMD.AddOutServer
                || messageCmd == (int)MessageCMD.DeleteOutServer
                || messageCmd == (int)MessageCMD.UpdateInNetConnections
                || messageCmd == (int)MessageCMD.UpdateOutNetConnections;
        }

        private IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
    }
}
