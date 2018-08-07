using H6Game.Base.Entity;
using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    /// <summary>
    /// 内网分布式连接核心组件
    /// </summary>
    public class InNetComponent : BaseComponent
    {
        private SysConfig Config { get; } = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private EndPointEntity DefaultCenterEndPoint;
        private readonly ConcurrentDictionary<int, Session> InConnectSessions = new ConcurrentDictionary<int, Session>();
        private readonly ConcurrentDictionary<int, Session> DisconnectSessions = new ConcurrentDictionary<int, Session>();
        private Session InAcceptSession;
        private Session OutAcceptSession;
        private Session CenterConnectSession;

        public IEnumerable<Session> ConnectSessions { get { return this.InConnectSessions.Values; } }
        public NetEndPointMessage OutNetMessage { get { return this.Config.GetOutMessage(); } }
        public bool IsCenterServer { get { return this.Config.IsCenterServer; } }
        public NetMapManager InNetMapManager { get; } = new NetMapManager();
        public NetMapManager OutNetMapManager { get; } = new NetMapManager();

        public override void Start()
        {
            this.DefaultCenterEndPoint = Config.InNetConfig.CenterEndPoint;
            var center = this.Config.GetCenterMessage();
            if (this.Config.IsCenterServer)
            {
                HandleInAccept(center);
                return;
            }
            HandleInAccept(this.Config.GetInMessage());
            this.Connecting(center);
            HandleOutAccept(this.Config.GetOutMessage());
        }

        /// <summary>
        /// 更新内部分布式连接状态
        /// </summary>
        public override void Update()
        {
            if(OutAcceptSession != null)
                OutAcceptSession.Update();

            if (InAcceptSession != null)
                InAcceptSession.Update();

            if (CenterConnectSession != null)
                CenterConnectSession.Update();

            foreach(var connect in InConnectSessions.Values)
            {
                connect.Update();
            }

            foreach (var connect in DisconnectSessions.Values)
            {
                connect.Update();
            }
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <returns></returns>
        public Task<T> CallMessage<T>(Session session, ANetChannel channel, T data, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        {
            var tcs = new TaskCompletionSource<T>();
            session.Subscribe(channel, data, (p) =>
            {
                var response = p.Read<T>();
                if (response == null)
                {
                    tcs.TrySetResult(default);
                    return;
                }
                tcs.TrySetResult(response);
            }, messageCmd);
            return tcs.Task;
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <returns></returns>
        public void CallMessage<T,P>(Session session, ANetChannel channel, T data, int messageCmd, Action<P> notificationAction, bool isCompress = false, bool isEncrypt = false)
        {
            session.Subscribe(channel, data, (p) =>
            {
                var response = p.Read<P>();
                notificationAction(response);
            }, messageCmd);
        }

        public void AddSession(NetEndPointMessage message)
        {
            if (Config.IsCenterServer)
                return;

            //判断是否是本地服务，是排除掉
            if(message == this.Config.GetInMessage())
                return;

            //排除中心服务
            if (message == this.Config.GetCenterMessage())
                return;

            //如果存在就不再创建新的Session
            if (this.InConnectSessions.ContainsKey(message.GetHashCode()))
                return;

            Connecting(message);
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
                }
            };

            this.InAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"监听内网端口:{message.Port}成功.");

            if (this.Config.IsCenterServer)
                LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleInAccept", $"中心服务启动成功.");
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            var session = new Session(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
                throw new Exception($"服务端口被占用.");

            this.OutAcceptSession = session;
            LogRecord.Log(LogLevel.Info, $"{this.GetType()}/HandleOutAccept", $"监听外网端口:{message.Port}成功.");
        }

        private void Connecting(NetEndPointMessage message)
        {
            if (Config.IsCenterServer)
                return;

            var hashCode = message.GetHashCode();
            if (this.InConnectSessions.ContainsKey(hashCode))
                return;

            //不连接进程内的监听端口
            if(message == this.Config.GetInMessage())
                return;

            var session = new Session(GetIPEndPoint(message), ProtocalType.Tcp);

            if (message == this.Config.GetCenterMessage())
                this.CenterConnectSession = session;
            else
                this.InConnectSessions[hashCode] = session;

            //注册连接成功回调
            session.OnClientConnected = async (c) =>
            {
                if (this.DisconnectSessions.TryRemove(hashCode, out Session oldSession))
                    this.InConnectSessions[hashCode] = oldSession;

                var localMessage = this.Config.GetInMessage();
                this.InNetMapManager.Add(c, message);

                if (message != this.Config.GetCenterMessage())
                {
                    var remoteOutNet = await this.CallMessage<NetEndPointMessage>(session, c, null, (int)MessageCMD.GetOutServer);
                    this.OutNetMapManager.Add(c, remoteOutNet);
                }

                SendToCenter(localMessage, (int)MessageCMD.AddInServer);
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                if (message == this.Config.GetCenterMessage())
                {
                    LogRecord.Log(LogLevel.Error, $"{this.GetType()}/ConnectToCenter", $"当前中心服务挂掉.");
                    return;
                }

                if (this.InConnectSessions.TryRemove(hashCode, out Session oldSession))
                    this.DisconnectSessions[hashCode] = oldSession;

                this.InNetMapManager.Remove(message);
                if (this.OutNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage outMessage))
                    this.OutNetMapManager.Remove(outMessage);
            };
            session.Connect();
        }

        private void SendToCenter<T>(T data, int messageCmd)
        {
            this.CenterConnectSession.Broadcast(data, messageCmd);
        }

        private bool IsSysMessage(int messageCmd)
        {
            return messageCmd == (int)MessageCMD.AddInServer;
        }

        private IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
    }
}
