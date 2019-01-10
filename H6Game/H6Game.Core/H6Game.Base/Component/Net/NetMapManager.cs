using H6Game.Base.Message;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base.Component
{
    /// <summary>
    /// 外网连接管理
    /// 关于外网连接管理：代理服务只负责分发其他网关服务的IP端口连接信息，代理服务的IP端口是固定的，可以使用域名
    /// 来代替固定IP配置代理服务，代理服务也类似于DNS服务，只负责告诉客户端可以与哪台服务器创建连接，代理服务不参
    /// 与任何业务逻辑。
    /// </summary>
    public class ProxyNetMapManager
    {
        private Dictionary<int, int> ClientConnectionNumbers { get;} = new Dictionary<int, int>();
        private HashSet<OuterEndPointMessage> ConnectEntities { get; } = new HashSet<OuterEndPointMessage>();
        private Dictionary<int, OuterEndPointMessage> NetworkMapMessages { get; } = new Dictionary<int, OuterEndPointMessage>();
        private Dictionary<int, Network> HCodeMapChannel { get; } = new Dictionary<int, Network>();

        /// <summary>
        /// 一个服务承载最大客户端连接负载阀值，根据实际的项目配置该值。
        /// </summary>
        private const int OneServerMaxConnect = 10000;

        /// <summary>
        /// 获取教合适的网关连接信息。
        /// </summary>
        /// <returns></returns>
        public OuterEndPointMessage GetGoodConnectedInfo()
        {
            //取当前客户端连接最多的服务
            var list =this. ClientConnectionNumbers.OrderByDescending(a => a.Value);
            if (!list.Any())
                return null;

            var maxConnectInfo = list.First();
            var key = maxConnectInfo.Key;
            if (maxConnectInfo.Value >= OneServerMaxConnect)
            {
                if(this.ClientConnectionNumbers.Count == 1)
                    key = maxConnectInfo.Key;
                else
                    key = list.Skip(1).Take(1).First().Key;
            }

            //与客户端连接的服务的客户端数量+1
            this.ClientConnectionNumbers[key]++;
            return this.NetworkMapMessages[key];

        }

        internal void Add(Network network, OuterEndPointMessage message)
        {
            if (ClientConnectionNumbers.ContainsKey(network.Id))
                return;

            this.ClientConnectionNumbers[network.Id] = 0;
            this.ConnectEntities.Add(message);
            this.NetworkMapMessages[network.Id] = message;
            this.HCodeMapChannel[message.GetHashCode()] = network;
        }

        internal virtual void Remove(Network network)
        {
            if (this.TryGetFromChannelId(network, out OuterEndPointMessage message))
                this.Remove(message);
        }

        internal void Remove(OuterEndPointMessage message)
        {
            if (!this.HCodeMapChannel.TryGetValue(message.GetHashCode(), out Network network))
                return;

            this.ClientConnectionNumbers.Remove(network.Channel.Id);
            this.HCodeMapChannel.Remove(message.GetHashCode());
            this.NetworkMapMessages.Remove(network.Channel.Id);
        }

        /// <returns></returns>
        private bool TryGetFromChannelId(Network network, out OuterEndPointMessage message)
        {
            return this.NetworkMapMessages.TryGetValue(network.Id, out message);
        }
    }

    /// <summary>
    /// 管理服务端分布式网络映射组件。
    /// </summary>
    public class NetMapManager
    {
        private HashSet<NetEndPointMessage> ConnectInfos { get; } = new HashSet<NetEndPointMessage>();
        private Dictionary<int, NetEndPointMessage> NetworkMapMessages { get; } = new Dictionary<int, NetEndPointMessage>();
        private Dictionary<int, Network> HCodeMapChannel { get; } = new Dictionary<int, Network>();

        public IEnumerable<NetEndPointMessage> ConnectMessageInfos { get { return ConnectInfos; } }

        internal bool Existed(NetEndPointMessage message)
        {
            return this.ConnectInfos.Contains(message);
        }

        internal virtual void Remove(Network network)
        {
            if (this.TryGetFromChannelId(network, out NetEndPointMessage inMessage))
                this.Remove(inMessage);
        }

        internal virtual void Remove(NetEndPointMessage message)
        {
            if (this.ConnectInfos.Remove(message))
            {
                if (!this.HCodeMapChannel.TryGetValue(message.GetHashCode(), out Network network))
                    return;

                this.HCodeMapChannel.Remove(message.GetHashCode());
                this.NetworkMapMessages.Remove(network.Channel.Id);
            }
        }

        internal virtual void Add(Network network, NetEndPointMessage message)
        {
            if (this.ConnectInfos.Contains(message))
                return;

            this.ConnectInfos.Add(message);
            this.NetworkMapMessages[network.Id] = message;
            this.HCodeMapChannel[message.GetHashCode()] = network;
        }

        private bool TryGetFromChannelId(Network network, out NetEndPointMessage message)
        {
            return this.NetworkMapMessages.TryGetValue(network.Id, out message);
        }
    }
}
