using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace H6Game.Base
{
    [NetCommand(SysNetCommand.AddInServerCmd)]
    public class DistributionsSubscriber : NetSubscriber<NetEndPointMessage>
    {
        protected override void Subscribe(Network network, NetEndPointMessage message, int netCommand)
        {
            Game.Scene.GetComponent<NetDistributionsComponent>().AddServerConnection(network, message);
        }
    }

    [NetCommand(SysNetCommand.GetOutServerCmd)]
    public class OutNetMessageSyncSubscriber : NetSubscriber
    {
        protected override void Subscribe(Network network, int netCommand)
        {
            Game.Scene.GetComponent<NetDistributionsComponent>().ResponseOutNetEndPointMessage(network);
        }
    }

    [NetCommand(SysNetCommand.GetInServerCmd)]
    public class InnerMessageSyncSubscriber : NetSubscriber
    {
        protected override void Subscribe(Network network, int netCommand)
        {
            Game.Scene.GetComponent<NetDistributionsComponent>().ResponseInnerEndPintMessage(network);
        }
    }

    /// <summary>
    /// 内网分布式连接核心组件，如果服务是基于分布式构建，应该使用该组件来构建基于分布式的Socket连接，该组件能够提供一个基于去中心化，可靠，高可扩展的
    /// 分布式模型，只需要对"H6Game.DistributionsConfig.json"文件设置相应的配置，就能使用完整的分布式功能。
    /// </summary>
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    [SingleCase]
    public sealed class NetDistributionsComponent : BaseComponent
    {
        private NetConfigEntity Config { get; set; }
        private EndPointConfigEntity DefaultCenterEndPoint { get; set; }
        private NetEndPointMessage InNetMessage { get { return this.Config.GetInnerMessage(); } }
        private NetEndPointMessage OutNetMessage { get { return this.Config.GetOutMessage(); } }
        private ConcurrentDictionary<int, Network> DisconnectedNetworks { get; } = new ConcurrentDictionary<int, Network>();
        private ConcurrentDictionary<int, Network> InConnectedNetworks { get; } = new ConcurrentDictionary<int, Network>();
        private Dictionary<int, Network> InAcceptNetworks { get; } = new Dictionary<int, Network>();
        private Dictionary<int, Network> OuAcceptNetworks { get; } = new Dictionary<int, Network>();
        private Network InAcceptNetwork { get; set; }
        private Network CenterConnectNetwork { get; set; }

        /// <summary>
        /// 监听外网连接网络类。
        /// </summary>
        public Network OutAcceptNetwork { get; private set; }

        /// <summary>
        /// 内网所有客户端连接网络对象集合。
        /// </summary>
        public IEnumerable<Network> InConnNets { get { return InConnectedNetworks.Values; } }

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
            this.Config = new NetDistributionsConfig().Config;
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
            if (OutAcceptNetwork != null)
                OutAcceptNetwork.Update();

            if (InAcceptNetwork != null)
                InAcceptNetwork.Update();

            if (CenterConnectNetwork != null)
                CenterConnectNetwork.Update();

            foreach (var network in InConnectedNetworks.Values)
            {
                network.Update();
            }

            foreach (var network in DisconnectedNetworks.Values)
            {
                network.Update();
            }
        }

        public void AddServerConnection(Network network, NetEndPointMessage message)
        {
            if (InNetMapManager.Existed(message))
                return;

            AddNetwork(message);
            if (IsCenterServer)
            {
                InNetMapManager.Add(network.Channel, message);

                network.Broadcast(message, (int)SysNetCommand.AddInServerCmd);
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

        private void AddNetwork(NetEndPointMessage message)
        {
            if (Config.IsCenterServer)
                return;

            //排除掉本地服务
            if (message == this.Config.GetInnerMessage())
                return;

            //排除中心服务
            if (message == this.Config.GetCenterMessage())
                return;

            Connecting(message);
        }

        private void HandleInAccept(NetEndPointMessage message)
        {
            this.InAcceptNetwork = Network.CreateAcceptor(IPEndPointHelper.GetIPEndPoint(message), ProtocalType.Tcp, c =>
            {
                InAcceptNetworks[c.Id] = c.Network;
            }, c =>
            {
                InAcceptNetworks.Remove(c.Id);

                if (this.InNetMapManager.TryGetFromChannelId(c, out NetEndPointMessage inMessage))
                {
                    this.InNetMapManager.Remove(inMessage);
                }

                if (this.Config.IsCenterServer)
                    return;
            });

            Log.Info($"监听内网端口:{message.Port}成功.", LoggerBllType.System);

            if (this.Config.IsCenterServer)
                Log.Info($"中心服务启动成功.", LoggerBllType.System);
        }

        private void HandleOutAccept(NetEndPointMessage message)
        {
            this.OutAcceptNetwork = Network.CreateAcceptor(IPEndPointHelper.GetIPEndPoint(message), ProtocalType.Kcp, c =>
            {
                OuAcceptNetworks[c.Id] = c.Network;
            }, c =>
            {
                OuAcceptNetworks.Remove(c.Id);
            }
            );
            Log.Info($"监听外网端口:{message.Port}成功.", LoggerBllType.System);
        }

        private void Connecting(NetEndPointMessage message)
        {
            var hashCode = message.GetHashCode();
            if (this.InConnectedNetworks.ContainsKey(hashCode))
                return;

            //如果存在就不再创建新的Network
            if (this.DisconnectedNetworks.ContainsKey(hashCode))
                return;

            if (Config.IsCenterServer)
                return;

            //不连接进程内的监听端口
            if (message == this.Config.GetInnerMessage())
                return;

            var network = Network.CreateConnecting(IPEndPointHelper.GetIPEndPoint(message), ProtocalType.Tcp, c =>
            {
                OnClientConnect(c, message, hashCode);
            }, c =>
            {
                OnClientDisconnect(c, message, hashCode);
            });

            if (message == this.Config.GetCenterMessage())
                this.CenterConnectNetwork = network;
            else
                this.InConnectedNetworks[hashCode] = network;
        }

        private async void OnClientConnect(ANetChannel channel, NetEndPointMessage message, int hashCode)
        {
            if (this.DisconnectedNetworks.TryRemove(hashCode, out Network oldNetwork))
                this.InConnectedNetworks[hashCode] = oldNetwork;

            var localMessage = this.Config.GetInnerMessage();
            this.InNetMapManager.Add(channel, message);

            //连接成功后把本地监听端口发送给远程进程
            channel.Network.Send(localMessage, (int)SysNetCommand.AddInServerCmd);

            //把当前所有连接的内网监听服务发送给远程进程
            foreach (var entity in this.InNetMapManager.Entities)
            {
                channel.Network.Send(entity, (int)SysNetCommand.AddInServerCmd);
            }

            if (message != this.Config.GetCenterMessage())
            {
                var callResult = await channel.Network.CallMessageAsync<NetEndPointMessage>((int)SysNetCommand.GetOutServerCmd);
                if (callResult.Result)
                {
                    this.OutNetMapManager.Add(channel, callResult.Content);
                }
                this.OnInnerClientConnected?.Invoke(channel);
            }
            else
            {
                Log.Info("连接中心服务成功.", LoggerBllType.System);
            }
        }

        private void OnClientDisconnect(ANetChannel channel, NetEndPointMessage message, int hashCode)
        {
            if (message == this.Config.GetCenterMessage())
            {
                Log.Info($"当前中心服务挂掉.", LoggerBllType.System);
                return;
            }

            if (this.InConnectedNetworks.TryRemove(hashCode, out Network oldNetwork))
                this.DisconnectedNetworks[hashCode] = oldNetwork;

            this.InNetMapManager.Remove(message);
            if (this.OutNetMapManager.TryGetFromChannelId(channel, out NetEndPointMessage outMessage))
                this.OutNetMapManager.Remove(outMessage);

            this.OnInnerClientDisconnected?.Invoke(channel);
        }
    }
}
