using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 外网连接管理
    /// 关于外网连接管理：外网连接管理服务只负责分发其他网关服务的IP端口连接信息，代理服务的IP端口是固定的，类似于DNS服务的概念，代理服务不参与
    /// 任何业务逻辑。
    /// </summary>
    public class ProxyNetMapManager : NetMapManager
    {
        private Dictionary<int, int> ClientConnectionNumbers { get; set; } = new Dictionary<int, int>();

        public NetEndPointMessage GetEndPointMessage()
        {
            //取当前客户端连接最小的服务
            var minConnectInfo = ClientConnectionNumbers.OrderBy(a => a.Value).FirstOrDefault();

            //与客户端连接的服务的客户端数量+1
            ClientConnectionNumbers[minConnectInfo.Key]++;

            return ChannelIdMapMsg[minConnectInfo.Key];
        }

        public override void Add(ANetChannel channel, NetEndPointMessage message)
        {
            if (!ClientConnectionNumbers.ContainsKey(channel.Id))
                ClientConnectionNumbers[channel.Id] = 0;

            base.Add(channel, message);
        }

        public override void Remove(NetEndPointMessage message)
        {
            if(HCodeMapChannel.TryGetValue(message.GetHashCode(), out Network network))
                ClientConnectionNumbers.Remove(network.Channel.Id);

            base.Remove(message);
        }
    }

    /// <summary>
    /// 管理服务端分布式网络映射组件。
    /// </summary>
    public class NetMapManager
    {
        protected readonly HashSet<NetEndPointMessage> ConnectEntities = new HashSet<NetEndPointMessage>();
        protected readonly Dictionary<int, NetEndPointMessage> ChannelIdMapMsg = new Dictionary<int, NetEndPointMessage>();
        protected readonly Dictionary<int, Network> HCodeMapChannel = new Dictionary<int, Network>();

        public IEnumerable<NetEndPointMessage> Entities { get { return ConnectEntities; } }

        /// <summary>
        /// 判断是否存在。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal bool Existed(NetEndPointMessage message)
        {
            return ConnectEntities.Contains(message);
        }

        /// <summary>
        /// 删除一条连接消息。
        /// </summary>
        /// <param name="message"></param>
        public virtual void Remove(NetEndPointMessage message)
        {
            if (ConnectEntities.Remove(message))
            {
                if (!HCodeMapChannel.TryGetValue(message.GetHashCode(), out Network network))
                    return;

                HCodeMapChannel.Remove(message.GetHashCode());
                ChannelIdMapMsg.Remove(network.Channel.Id);
            }
        }

        /// <summary>
        /// 新增一个连接消息，非中心服务需要跟Channel映射。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public virtual void Add(ANetChannel channel, NetEndPointMessage message)
        {
            if (ConnectEntities.Contains(message))
                return;

            this.ConnectEntities.Add(message);
            ChannelIdMapMsg[channel.Id] = message;
            HCodeMapChannel[message.GetHashCode()] = channel.Network;
        }

        /// <summary>
        /// 用Channel Id获取相应的连接消息。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal bool TryGetFromChannelId(ANetChannel channel, out NetEndPointMessage message)
        {
            return ChannelIdMapMsg.TryGetValue(channel.Id, out message);
        }
    }
}
