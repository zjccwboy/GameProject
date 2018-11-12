using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;

namespace H6Game.Base
{
    /// 内网分布式连接核心组件，如果服务是基于分布式构建，应该使用该组件来构建基于分布式的Socket连接，该组
    /// 件能够提供一个基于去中心化、可靠、高可扩展的分布式模型，只需要对"H6Game.DistributionsConfig.json"
    /// 文件进行相应的配置，就能使用完整的分布式功能。
    /// 一、关于分布式中心服务：分布式中心服务只负责内网分布式系统的桥接，告诉每一个服务其他所有服务的IP端口连
    /// 接信息，中心服务不参与任何业务逻辑。
    /// 二、关于去中心化分布式实现的原理：
    /// 1、当Socket创建连接成功时，当前服务会给远程服务发送本地服务监听的IP端口信息，远程服务把本地服务监听的IP
    /// 端口信息保存，并且与本地服务监听端口创建一条连接，两个服务之间通讯一直保持两条长链接。
    /// 2、本地服务主动连接成功以后，会把其他连接到本地服务的远程服务监听IP端口信息发送给当前连接成功的远程服务，
    /// 远程服务拿到其他服务的监听端口，判断是否与本服务是否有连接，没有连接就主动连接所有的其他服务监听端口，这
    /// 是去中心化的关键，这样做就算中心服务挂掉，也可以保证可以与其他的服务建立连接，而不需要完全依赖中心服务。
    /// 3、本地服务主动发送信息与远程服务通讯，使用本地服务主动连接的Socket，本地服务监听的Socket用来被动订阅消
    /// 息，这样就很容易结合Actor模型的设计，开发的时候，不需要去关心哪边时Client，哪边时Server，底层的通讯模块
    /// 被抽象成管道，开发人员只需要关心接收到的数据接口、消息类型、业务逻辑。这里的通讯模型只限于服务端，外网的
    /// 客户端是可以使用Client主动连接的Socket来订阅消息的。
    /// 4、当远程服务为中心服务时，会给其他的服务广播一条当前服务监听的IP端口信息，拿到当前服务监听IP端口信
    /// 息会主动与当前服务创建连接，中心服务的作用就是为了第一次握手时告诉分布式系统内其他全部服务新增的服务所
    /// 监听的IP端口信息。
    /// 5、去中心化并不能完全做到去中心化，如果是一个新的服务要加入到分布式系统，必须依赖于中心服务，其他服务
    /// 只有在记录下了连接信息，才能在不依赖中心服务的情况下主动重连到挂掉的服务。

    public enum ServerType
    {
        Default,
        CenterServer,
        ProxyServer,
    }


    /// <summary>
    /// 内网分布式连接核心组件。
    /// </summary>
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    [SingleCase]
    public sealed class NetDistributionsComponent : BaseComponent
    {
        private NetConfigEntity Config { get; set; }
        private ConcurrentDictionary<NetEndPointMessage, Network> DisconnectedNetworks { get; } = new ConcurrentDictionary<NetEndPointMessage, Network>();
        private ConcurrentDictionary<NetEndPointMessage, Network> InConnectedNetworks { get; } = new ConcurrentDictionary<NetEndPointMessage, Network>();
        private ConcurrentDictionary<NetEndPointMessage, Network> NotExistProxyNetworks { get; } = new ConcurrentDictionary<NetEndPointMessage, Network>();
        private Dictionary<int, Network> InAcceptNetworks { get; } = new Dictionary<int, Network>();
        private Dictionary<int, Network> OuAcceptNetworks { get; } = new Dictionary<int, Network>();
        private Network LocalAcceptor { get; set; }
        private Network CenterConnector { get; set; }

        internal OuterEndPointMessage OuterNetMessage { get { return this.Config.GetOuterMessage(); } }
        internal NetEndPointMessage InnerNetMessage { get { return this.Config.GetLocalMessage(); } }

        /// <summary>
        /// 监听外网连接网络类。
        /// </summary>
        public List<NetAcceptorComponent> OuterAccepts { get;} = new List<NetAcceptorComponent>();

        /// <summary>
        /// 内网中心服务与代理服务以外的服务Socket连接集合。
        /// </summary>
        public IEnumerable<Network> InnerNetworks { get { return NotExistProxyNetworks.Values; } }

        /// <summary>
        /// 是否是中心服务。
        /// </summary>
        public bool IsCenterServer { get { return this.Config.CenterAcceptConfig.Enable; } }

        /// <summary>
        /// 是否是代理服务。
        /// </summary>
        public bool IsProxyServer { get { return this.Config.IsProxyServer; } }

        /// <summary>
        /// 内网消息映射管理类接口。
        /// </summary>
        public NetMapManager InnerNetMapManager { get; } = new NetMapManager();

        /// <summary>
        /// 外网消息映射管理类接口。
        /// </summary>
        public ProxyNetMapManager OuterNetMapManager { get; } = new ProxyNetMapManager();

        /// <summary>
        /// 内网网络服务端连接成功回调事件。
        /// </summary>
        public Action<Network> OnConnect { get; set; }

        /// <summary>
        /// 内网客户端网络连接断开回调事件。
        /// </summary>
        public Action<Network> OnDisconnect { get; set; }

        public override void Awake()
        {
            this.Config = new NetDistributionsConfig().Config;
        }

        public override void Start()
        {
            var center = this.Config.GetCenterMessage();
            if (this.IsCenterServer)
            {
                HandleAccept(center);
                return;
            }
            //启动内网监听
            HandleAccept(this.Config.GetLocalMessage());

            //连接中心服务
            this.Connecting(center);

            //启动外网监听
            HandleOutAccept();
        }

        /// <summary>
        /// 更新内部分布式连接状态
        /// </summary>
        public override void Update()
        {
            foreach(var acceptor in OuterAccepts)
                acceptor.Update();

            if (LocalAcceptor != null)
                LocalAcceptor.Update();

            if (CenterConnector != null)
                CenterConnector.Update();

            foreach (var network in InConnectedNetworks.Values)
                network.Update();

            foreach (var network in DisconnectedNetworks.Values)
                network.Update();
        }

        internal void AddNetwork(NetEndPointMessage message)
        {
            if (this.IsCenterServer)
                return;

            //排除掉本地服务
            if (message == this.Config.GetLocalMessage())
                return;

            //排除中心服务
            if (message == this.Config.GetCenterMessage())
                return;

            Connecting(message);
        }

        internal void BroadcastAddNewService(Network network, NetEndPointMessage message)
        {
            this.InnerNetMapManager.Add(network, message);
            network.Broadcast(message, SysNetCommand.AddInnerServerCmd);
            foreach (var entity in this.InnerNetMapManager.Entities)
            {
                network.Send(entity, SysNetCommand.AddInnerServerCmd);
            }
        }

        private void HandleAccept(NetEndPointMessage message)
        {
            this.LocalAcceptor = Network.CreateAcceptor(IPEndPointHelper.GetIPEndPoint(message), ProtocalType.Tcp, network =>
            {
                InAcceptNetworks[network.Id] = network;
            }, network =>
            {
                InAcceptNetworks.Remove(network.Id);
                this.InnerNetMapManager.Remove(network);

                if (this.IsCenterServer)
                    return;
            });

            if (this.IsCenterServer)
                Log.Info($"中心服务启动成功.", LoggerBllType.System);
            else if(this.IsProxyServer)
                Log.Info($"代理服务启动成功.", LoggerBllType.System);
            else
                Log.Info($"监听内网端口:{message.Port}成功.", LoggerBllType.System);
        }

        private void HandleOutAccept()
        {
            var config = this.Config.OuterAcceptConfig;
            if (config.OuterKcpAcceptConfig.Enable)
            {
                var endPoint = IPEndPointHelper.GetIPEndPoint(config.OuterKcpAcceptConfig.GetMessage());
                var acceptor = Game.Scene.AddComponent<NetAcceptorComponent, IPEndPoint, ProtocalType>(endPoint, ProtocalType.Kcp);
                this.OuterAccepts.Add(acceptor);
            }
            if (config.OuterTcpAcceptConfig.Enable)
            {
                var endPoint = IPEndPointHelper.GetIPEndPoint(config.OuterTcpAcceptConfig.GetMessage());
                var acceptor = Game.Scene.AddComponent<NetAcceptorComponent, IPEndPoint, ProtocalType>(endPoint, ProtocalType.Tcp);
                this.OuterAccepts.Add(acceptor);
            }
            if (config.OuterWebSocketConfig.Enable)
            {
                var prefixed = config.OuterWebSocketConfig.HttpPrefixed;
                var acceptor = Game.Scene.AddComponent<NetAcceptorComponent, string, ProtocalType>(prefixed, ProtocalType.Wcp);
                this.OuterAccepts.Add(acceptor);
            }
        }

        private void Connecting(NetEndPointMessage message)
        {
            if (this.InConnectedNetworks.ContainsKey(message))
                return;

            //如果存在就不再创建新的Network
            if (this.DisconnectedNetworks.ContainsKey(message))
                return;

            if (this.IsCenterServer)
                return;

            //不连接进程内的监听端口
            if (message == this.Config.GetLocalMessage())
                return;

            var connector = Network.CreateConnector(IPEndPointHelper.GetIPEndPoint(message), ProtocalType.Tcp, network =>
            {
                OnClientConnect(network, message);
            }, network =>
            {
                OnClientDisconnect(network, message);
            });

            if (message == this.Config.GetCenterMessage())
            {
                this.CenterConnector = connector;
            }
            else
            {
                this.InConnectedNetworks[message] = connector;
                this.NotExistProxyNetworks[message] = connector;
            }
        }

        private async void OnClientConnect(Network network, NetEndPointMessage message)
        {
            if (this.DisconnectedNetworks.TryRemove(message, out Network oldNetwork))
            {
                this.InConnectedNetworks[message] = oldNetwork;
                this.NotExistProxyNetworks[message] = oldNetwork;
            }

            this.InnerNetMapManager.Add(network, message);

            //发送本地进程连接信息
            SendConnections(network);

            if (message == this.Config.GetCenterMessage())
            {
                Log.Info("连接中心服务成功.", LoggerBllType.System);
                return;
            }

            var outerMessage = await network.CallMessageAsync<OuterEndPointMessage>(SysNetCommand.GetOutServerCmd);
            this.OuterNetMapManager.Add(network, outerMessage);

            if (this.IsProxyServer)
                return;

            var serverType = await network.CallMessageAsync<int>(SysNetCommand.GetServerType);
            if (serverType != (int)ServerType.Default)
            {
                //删掉连接中的代理服务
                this.NotExistProxyNetworks.TryRemove(message, out Network valu);
                return;
            }

            this.OnConnect?.Invoke(network);
        }

        private void SendConnections(Network network)
        {
            var localMessage = this.Config.GetLocalMessage();
            //连接成功后把本地监听端口发送给远程进程
            network.Send(localMessage, SysNetCommand.AddInnerServerCmd);

            //把当前所有连接的内网监听服务发送给远程进程
            foreach (var entity in this.InnerNetMapManager.Entities)
            {
                network.Send(entity, SysNetCommand.AddInnerServerCmd);
            }
        }

        private void OnClientDisconnect(Network network, NetEndPointMessage message)
        {
            if (message == this.Config.GetCenterMessage())
            {
                Log.Info($"当前中心服务挂掉.", LoggerBllType.System);
                return;
            }

            if (this.InConnectedNetworks.TryRemove(message, out Network oldNetwork))
            {
                this.NotExistProxyNetworks.TryRemove(message, out Network value);
                this.DisconnectedNetworks[message] = oldNetwork;
            }

            this.InnerNetMapManager.Remove(message);
            this.OuterNetMapManager.Remove(network);


            this.OnDisconnect?.Invoke(network);
        }
    }

    [NetCommand(SysNetCommand.AddInnerServerCmd)]
    public class SubscribeOnAddServerConnection : NetSubscriber<NetEndPointMessage>
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();

        protected override void Subscribe(NetEndPointMessage message, int command)
        {
            throw new NotImplementedException();
        }

        protected override void Subscribe(Network network, NetEndPointMessage message, int netCommand)
        {
            if (Distributions.InnerNetMapManager.Existed(message))
                return;

            Distributions.AddNetwork(message);
            if (!Distributions.IsCenterServer)
                return;

            Distributions.BroadcastAddNewService(network, message);
        }
    }

    [NetCommand(SysNetCommand.GetOutServerCmd)]
    public class SubscribeOnSyncOutNetMessage : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(Distributions.OuterNetMessage);
        }
    }

    [NetCommand(SysNetCommand.GetInServerCmd)]
    public class SubscribeOnSyncInnerNetMessage : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(Distributions.InnerNetMessage);
        }
    }

    [NetCommand(SysNetCommand.GetServerType)]
    public class SubscribeOnGetServerType : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            if (Distributions.IsCenterServer)
            {
                network.Response(ServerType.CenterServer);
            }
            else if (Distributions.IsProxyServer)
            {
                network.Response(ServerType.ProxyServer);
            }
            else
            {
                network.Response(ServerType.Default);
            }
        }
    }
}
