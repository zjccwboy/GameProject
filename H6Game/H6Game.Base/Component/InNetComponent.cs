using H6Game.Base.Entity;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

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
                HandleInAccept(center);
            else
                HandleInAccept(local);

            this.ConnectToCenter(center);
        }

        /// <summary>
        /// 更新内部分布式连接状态
        /// </summary>
        public void Update()
        {
            if(outAcceptSession != null)
                outAcceptSession.Update();

            if (inAcceptSession != null)
                inAcceptSession.Update();            

            if (!inConnectSessions.Any())
            {
                if (this.config.IsCenterServer)
                {
                    return;
                }
                var centerMessage = this.config.GetCenterMessage();
                ConnectToCenter(centerMessage);
            }

            var connections = inConnectSessions.Values;
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
                if (!DispatcherFactory.TryGetResponse(p.MessageId, p.Data, out T response))
                    tcs.TrySetResult(default(T));

                tcs.TrySetResult(response);
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
            if (this.config.IsCenterServer && !IsInNetMessage(messageCmd))
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

            //如果连接已经全部断开,清空映射表
            if (!inConnectSessions.Any())
            {
                this.InNetMapManager.Clear();
                this.OutNetMapManager.Clear();
                return;
            }

            var messageHashKeys = messages.Select(a => a.GetHashCode());
            var keys = this.inConnectSessions.Keys.ToList();
            foreach(var key in keys)
            {
                if (!messageHashKeys.Contains(key))
                    RemoveSession(key);
            }
        }

        private void AddSession(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
                return;

            var hashCode = message.GetHashCode();
            var localHashCode = this.config.GetInMessage().GetHashCode();
            //判断是否是本地服务，是排除掉
            if(hashCode == localHashCode)
                return;

            var centerHashCode = this.config.GetCenterMessage().GetHashCode();
            //排除中心服务
            if (hashCode == centerHashCode)
                return;

            if (this.inConnectSessions.ContainsKey(hashCode))
                return;

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);
            //注册连接成功回调
            session.OnClientConnected = (c) =>{ this.inConnectSessions[hashCode] = session; };
            //注册连接断开回调
            session.OnClientDisconnected = (c) =>{ this.inConnectSessions.Remove(hashCode); };
            session.Connect();
        }

        private void RemoveSession(int key)
        {
            if (config.IsCenterServer)
                return;

            var localHashCode = this.config.GetInMessage().GetHashCode();
            //判断是否是本地服务，是排除掉
            if (key == localHashCode)
                return;

            var centerHashCode = this.config.GetCenterMessage().GetHashCode();
            //排除中心服务
            if (key == centerHashCode)
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
                if (this.config.IsCenterServer)
                    throw new Exception($"中心服务端口被占用，");

                this.config.InNetConfig.LocalEndPoint.Port = message.Port++;
                HandleInAccept(message);
            }
            this.inAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听内网端口:{message.Port}成功.");
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
                            this.BroadcastConnections(entitys, (int)MessageCMD.UpdateInNetConnections);
                            LogRecord.Log(LogLevel.Debug, $"{this.GetType()}/HandleInAccept", $"广播新的连接映射表:{entitys.ConvertToJson()}.");
                        }

                        if (this.OutNetMapManager.TryGetFromChannelId(c.Id, out NetEndPointMessage outMessage))
                        {
                            this.OutNetMapManager.Remove(outMessage);
                            var entitys = this.OutNetMapManager.ConnectEntities;
                            this.BroadcastConnections(entitys, (int)MessageCMD.UpdateOutNetConnections);
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
                if (this.config.IsCenterServer)
                    throw new Exception($"中心服务端口被占用，");

                this.config.OuNetConfig.Port = message.Port++;
                HandleOutAccept(message);
            }
            this.outAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleOutAccept", $"监听外网端口:{message.Port}成功.");
        }

        private void ConnectToCenter(NetEndPointMessage message)
        {
            if (config.IsCenterServer)
                return;

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);

            //注册连接成功回调
            session.OnClientConnected = (c) =>
            {
                this.inConnectSessions[message.GetHashCode()] = session;
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
                if (this.OutNetMapManager.TryGetMappingMessage(c, out NetEndPointMessage outMessage))
                    this.OutNetMapManager.Remove(outMessage);

                this.inConnectSessions.Remove(messageRp.GetHashCode());
                if (config.IsCenterServer)
                {
                    return;
                }

                if (messageRp == this.config.GetCenterMessage())
                {
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前中心服务挂掉.");
                    return;
                }
            };
            session.Connect();
        }

        private bool IsInNetMessage(int messageCmd)
        {
            return messageCmd == (int)MessageCMD.AddInServer
                || messageCmd == (int)MessageCMD.AddOutServer
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
