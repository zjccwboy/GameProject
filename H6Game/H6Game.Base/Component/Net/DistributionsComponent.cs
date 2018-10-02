using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Concurrent;
using H6Game.Hotfix.Messages.Attributes;
using H6Game.Hotfix.Messages.Inner;
using H6Game.Hotfix.Messages.Enums;

namespace H6Game.Base
{
    [SubscriberCMD(InnerMessageCMD.AddInServerCmd)]
    public class DistributionsSubscriber : AMsgSubscriber<NetEndPointMessage>
    {
        protected override void Subscribe(Network network, NetEndPointMessage message)
        {
            Game.Scene.GetComponent<DistributionsComponent>().AddServerConnection(network, message);
        }
    }

    [SubscriberCMD(InnerMessageCMD.GetOutServerCmd)]
    public class OutNetMessageSyncSubscriber : AMsgSubscriber
    {
        protected override void Subscribe(Network network)
        {
            Game.Scene.GetComponent<DistributionsComponent>().ResponseOutNetEndPointMessage(network);
        }
    }

    [SubscriberCMD(InnerMessageCMD.GetInServerCmd)]
    public class InnerMessageSyncSubscriber : AMsgSubscriber
    {
        protected override void Subscribe(Network network)
        {
            Game.Scene.GetComponent<DistributionsComponent>().ResponseInnerEndPintMessage(network);
        }
    }

    /// <summary>
    /// 内网分布式连接核心组件
    /// </summary>
    [Event(EventType.Awake | EventType.Start | EventType.Update)]
    [SingletCase]
    public sealed class DistributionsComponent : BaseComponent
    {
        private DistributionsConfigEntity Config { get; set; }
        private EndPointConfigEntity DefaultCenterEndPoint { get; set; }

        private readonly ConcurrentDictionary<int, Session> InConnectedSessions = new ConcurrentDictionary<int, Session>();
        private readonly ConcurrentDictionary<int, Session> DisconnectedSessions = new ConcurrentDictionary<int, Session>();
        private readonly ConcurrentDictionary<int, Network> InConnectedNetworks = new ConcurrentDictionary<int, Network>();
        private readonly ConcurrentDictionary<int, Network> InAcceptNetworks = new ConcurrentDictionary<int, Network>();
        private readonly ConcurrentDictionary<int, Network> OuAcceptNetworks = new ConcurrentDictionary<int, Network>();

        private Session InAcceptSession;
        private Session OutAcceptSession;
        private Session CenterConnectSession;

        /// <summary>
        /// 内网所有客户端连接网络对象集合。
        /// </summary>
        public IEnumerable<Network> InConnNets { get { return InConnectedNetworks.Values; } }

        /// <summary>
        /// 外网服务端所有监听连接网络对象集合。
        /// </summary>
        public IEnumerable<Network> OuAccNets { get { return OuAcceptNetworks.Values; } }

        /// <summary>
        /// 内网监听IP端口消息类。
        /// </summary>
        private NetEndPointMessage InNetMessage { get { return this.Config.GetInnerMessage(); } }

        /// <summary>
        /// 外网监听IP端口消息类。
        /// </summary>
        private NetEndPointMessage OutNetMessage { get { return this.Config.GetOutMessage(); } }

        /// <summary>
        /// 是否是中心服务。
        /// </summary>
        public bool IsCenterServer { get { return this.Config.IsCenterServer; } }

        /// <summary>
        /// 内网消息映射管理类接口。
        /// </summary>
        public NetMapManager InNetMapManager { get; } = new NetMapManager();

        /// <summary>
        /// 外网消息映射管理类接口。
        /// </summary>
        public NetMapManager OutNetMapManager { get; } = new NetMapManager();

        /// <summary>
        /// 内网网络服务端连接成功回调事件。
        /// </summary>
        public Action<ANetChannel> OnInnerClientConnected { get; set; }

        /// <summary>
        /// 内网客户端网络连接断开回调事件。
        /// </summary>
        public Action<ANetChannel> OnInnerClientDisconnected { get; set; }

        public override void Awake()
        {
            this.Config = new DistributionsConfig().Config;
        }

        public override void Start()
        {
            this.DefaultCenterEndPoint = Config.InnerListenConfig.CenterEndPoint;
            var center = this.Config.GetCenterMessage();
            if (this.Config.IsCenterServer)
            {
                HandleInAccept(center);
                return;
            }
            HandleInAccept(this.Config.GetInnerMessage());
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

            foreach(var connect in InConnectedSessions.Values)
            {
                connect.Update();
            }

            foreach (var connect in DisconnectedSessions.Values)
            {
                if (connect.ConnectChannel.Connected)
                    continue;

                connect.Update();
            }
        }

        public void AddServerConnection(Network network, NetEndPointMessage message)
        {
            if (InNetMapManager.Existed(message))
                return;

            AddSession(message);
            if (IsCenterServer)
            {
                InNetMapManager.Add(network.Channel, message);

                network.Broadcast(message, (int)InnerMessageCMD.AddInServerCmd);
                foreach (var entity in InNetMapManager.Entities)
                {
                    network.Response(entity);
                }
            }
        }

        public void ResponseOutNetEndPointMessage(Network network)
        {
            network.Response(OutNetMessage);
        }

        public void ResponseInnerEndPintMessage(Network network)
        {
            network.Response(InNetMessage);
        }

        private void AddSession(NetEndPointMessage message)
        {
            if (Config.IsCenterServer)
                return;

            //判断是否是本地服务，是排除掉
            if (message == this.Config.GetInnerMessage())
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
                InAcceptNetworks.TryAdd(c.Id, c.Network);
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
            };

            this.InAcceptSession = session;
            Log.Info($"监听内网端口:{message.Port}成功.", LoggerBllType.System);

            if (this.Config.IsCenterServer)
                Log.Info($"中心服务启动成功.", LoggerBllType.System);
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            var session = Network.CreateSession(GetIPEndPoint(message), ProtocalType.Kcp);
            if (!session.Accept())
                throw new Exception($"服务端口:{message.Port}被占用.");

            session.OnServerConnected = (c) =>
            {
                OuAcceptNetworks.TryAdd(c.Id, c.Network);
            };

            session.OnServerDisconnected = (c) => 
            {
                OuAcceptNetworks.TryRemove(c.Id, out Network network);
            };

            this.OutAcceptSession = session;
            Log.Info($"监听外网端口:{message.Port}成功.", LoggerBllType.System);
        }

        private void Connecting(NetEndPointMessage message)
        {
            var hashCode = message.GetHashCode();
            if (this.InConnectedSessions.ContainsKey(hashCode))
                return;

            //如果存在就不再创建新的Session
            if (this.DisconnectedSessions.ContainsKey(hashCode))
                return;

            if (Config.IsCenterServer)
                return;

            //不连接进程内的监听端口
            if (message == this.Config.GetInnerMessage())
                return;

            var session = Network.CreateSession(GetIPEndPoint(message), ProtocalType.Tcp);

            if (message == this.Config.GetCenterMessage())
                this.CenterConnectSession = session;
            else
                this.InConnectedSessions[hashCode] = session;

            //注册连接成功回调
            session.OnClientConnected = async (c) =>
            {

                if (this.DisconnectedSessions.TryRemove(hashCode, out Session oldSession))
                    this.InConnectedSessions[hashCode] = oldSession;

                var localMessage = this.Config.GetInnerMessage();
                this.InNetMapManager.Add(c, message);

                //连接成功后把本地监听端口发送给远程进程
                c.Network.Send(localMessage, (int)InnerMessageCMD.AddInServerCmd);

                //把当前所有连接的内网监听服务发送给远程进程
                foreach(var entity in this.InNetMapManager.Entities)
                {
                    c.Network.Send(entity, (int)InnerMessageCMD.AddInServerCmd);
                }

                if (message != this.Config.GetCenterMessage())
                {
                    InConnectedNetworks.TryAdd(c.Id, c.Network);

                    var callResult = await c.Network.CallMessageAsync<NetEndPointMessage>((int)InnerMessageCMD.GetOutServerCmd);
                    if (callResult.Result)
                    {
                        //this.Log(LogLevel.Debug, "Connecting", $"收到:{c.RemoteEndPoint} 消息CMD:{(int)MessageCMD.GetOutServerCmd} :{callResult.Content.ToJson()}");
                        this.OutNetMapManager.Add(c, callResult.Content);
                    }

                    this.OnInnerClientConnected?.Invoke(c);
                }
                else
                {
                    Log.Info("连接中心服务成功.", LoggerBllType.System);
                    //SendToCenter(localMessage, (int)MessageCMD.AddInServerCmd);
                }
            };

            //注册连接断开回调
            session.OnClientDisconnected = (c) =>
            {
                if (message == this.Config.GetCenterMessage())
                {
                    Log.Info($"当前中心服务挂掉.", LoggerBllType.System);
                    return;
                }

                InConnectedNetworks.TryRemove(c.Id, out Network network);

                if (this.InConnectedSessions.TryRemove(hashCode, out Session oldSession))
                    this.DisconnectedSessions[hashCode] = oldSession;

                this.InNetMapManager.Remove(message);
                if (this.OutNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage outMessage))
                    this.OutNetMapManager.Remove(outMessage);

                this.OnInnerClientDisconnected?.Invoke(c);
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
