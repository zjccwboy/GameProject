using H6Game.Message;
using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Concurrent;
using MongoDB.Bson;

namespace H6Game.Base
{
    /// <summary>
    /// 内网分布式连接核心组件
    /// </summary>
    [Event(EventType.Awake | EventType.Start | EventType.Update)]
    [SingletCase]
    public sealed class InNetComponent : BaseComponent
    {
        private SysConfig Config { get; set; }
        private EndPointEntity DefaultCenterEndPoint { get; set; }

        private readonly ConcurrentDictionary<int, Session> InConnectSessions = new ConcurrentDictionary<int, Session>();
        private readonly ConcurrentDictionary<int, Session> DisconnectSessions = new ConcurrentDictionary<int, Session>();
        private readonly ConcurrentDictionary<int, Network> InConnectNetworks = new ConcurrentDictionary<int, Network>();
        private readonly ConcurrentDictionary<int, Network> InAcceptNetworks = new ConcurrentDictionary<int, Network>();
        private readonly ConcurrentDictionary<int, Network> OuAcceptNetworks = new ConcurrentDictionary<int, Network>();

        private Session InAcceptSession;
        private Session OutAcceptSession;
        private Session CenterConnectSession;

        /// <summary>
        /// 内网所有客户端连接网络对象集合
        /// </summary>
        public IEnumerable<Network> InConnNets { get { return InConnectNetworks.Values; } }

        /// <summary>
        /// 内网服务端所有监听连接网络对象集合
        /// </summary>
        //public IEnumerable<Network> InAccNets { get { return InAcceptNetworks.Values; } }

        /// <summary>
        /// 外网服务端所有监听连接网络对象集合
        /// </summary>
        public IEnumerable<Network> OuAccNets { get { return OuAcceptNetworks.Values; } }

        /// <summary>
        /// 内网监听IP端口消息类
        /// </summary>
        public NetEndPointMessage InNetMessage { get { return this.Config.GetInMessage(); } }

        /// <summary>
        /// 外网监听IP端口消息类
        /// </summary>
        public NetEndPointMessage OutNetMessage { get { return this.Config.GetOutMessage(); } }

        /// <summary>
        /// 是否是中心服务
        /// </summary>
        public bool IsCenterServer { get { return this.Config.IsCenterServer; } }

        /// <summary>
        /// 内网消息映射管理类接口
        /// </summary>
        public NetMapManager InNetMapManager { get; } = new NetMapManager();

        /// <summary>
        /// 外网消息映射管理类接口
        /// </summary>
        public NetMapManager OutNetMapManager { get; } = new NetMapManager();

        /// <summary>
        /// 网络连接回调事件
        /// </summary>
        public Action<ANetChannel> OnConnected { get; set; }

        /// <summary>
        /// 网络断开回调事件
        /// </summary>
        public Action<ANetChannel> OnDisConnected { get; set; }

        public override void Awake()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
        }

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
                if (connect.ConnectChannel.Connected)
                    continue;

                connect.Update();
            }
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

            Connecting(message);
        }

        private void HandleInAccept(NetEndPointMessage message)
        {
            var session = Network.CreateSession(GetIPEndPoint(message), ProtocalType.Tcp);
            if (!session.Accept())
            {
                throw new Exception($"服务端口:{message.Port}被占用.");
            }

            session.OnServerConnected = (c) =>
            {
                InAcceptNetworks.TryAdd(c.Id, c.Dispatcher.Network);
            };

            session.OnServerDisconnected = (c) =>
            {
                InAcceptNetworks.TryRemove(c.Id, out Network network);

                if (this.InNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage inMessage))
                {
                    this.InNetMapManager.Remove(inMessage);
                }

                if (this.Config.IsCenterServer)
                    return;

                this.OnDisConnected?.Invoke(c);
            };

            this.InAcceptSession = session;
            this.Log(LogLevel.Info, "HandleInAccept", $"监听内网端口:{message.Port}成功.");

            if (this.Config.IsCenterServer)
                this.Log(LogLevel.Info, "HandleInAccept", $"中心服务启动成功.");
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            var session = Network.CreateSession(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
                throw new Exception($"服务端口:{message.Port}被占用.");

            session.OnServerConnected = (c) =>
            {
                OuAcceptNetworks.TryAdd(c.Id, c.Dispatcher.Network);
            };

            session.OnServerDisconnected = (c) => 
            {
                OuAcceptNetworks.TryRemove(c.Id, out Network network);
            };

            this.OutAcceptSession = session;
            this.Log(LogLevel.Info, "HandleOutAccept", $"监听外网端口:{message.Port}成功.");
        }

        private void Connecting(NetEndPointMessage message)
        {
            var hashCode = message.GetHashCode();
            if (this.InConnectSessions.ContainsKey(hashCode))
                return;

            //如果存在就不再创建新的Session
            if (this.DisconnectSessions.ContainsKey(hashCode))
                return;

            if (Config.IsCenterServer)
                return;

            //不连接进程内的监听端口
            if (message == this.Config.GetInMessage())
                return;

            var session = Network.CreateSession(GetIPEndPoint(message), ProtocalType.Tcp);

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

                //连接成功后把本地监听端口发送给远程进程
                c.Dispatcher.Network.Send(localMessage, (int)MessageCMD.AddInServerCmd);

                //把当前所有连接的内网监听服务发送给远程进程
                foreach(var entity in this.InNetMapManager.Entities)
                {
                    c.Dispatcher.Network.Send(entity, (int)MessageCMD.AddInServerCmd);
                }

                if (message != this.Config.GetCenterMessage())
                {
                    InConnectNetworks.TryAdd(c.Id, c.Dispatcher.Network);

                    var callResult = await c.Dispatcher.Network.CallMessage<NetEndPointMessage>((int)MessageCMD.GetOutServerCmd);
                    if (callResult.Result)
                    {
                        //this.Log(LogLevel.Debug, "Connecting", $"收到:{c.RemoteEndPoint} 消息CMD:{(int)MessageCMD.GetOutServerCmd} :{callResult.Content.ToJson()}");
                        this.OutNetMapManager.Add(c, callResult.Content);
                    }

                    this.OnConnected?.Invoke(c);
                }
                else
                {
                    this.Log(LogLevel.Info, "Connecting", "连接中心服务成功.");
                    //SendToCenter(localMessage, (int)MessageCMD.AddInServerCmd);
                }
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                if (message == this.Config.GetCenterMessage())
                {
                    this.Log(LogLevel.Error, "ConnectToCenter", $"当前中心服务挂掉.");
                    return;
                }

                InConnectNetworks.TryRemove(c.Id, out Network network);

                if (this.InConnectSessions.TryRemove(hashCode, out Session oldSession))
                    this.DisconnectSessions[hashCode] = oldSession;

                this.InNetMapManager.Remove(message);
                if (this.OutNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage outMessage))
                    this.OutNetMapManager.Remove(outMessage);
            };
            session.Connect();
        }

        private void SendToCenter<T>(T data, int messageCmd) where T : class
        {
            this.CenterConnectSession.Broadcast(data, messageCmd, 0 , 0);
        }

        private IPEndPoint GetIPEndPoint(NetEndPointMessage message)
        {
            var ip = IPAddress.Parse(message.IP);
            var port = message.Port;
            return new IPEndPoint(ip, port);
        }
    }
}
