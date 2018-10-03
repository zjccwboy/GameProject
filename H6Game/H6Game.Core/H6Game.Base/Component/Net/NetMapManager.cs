using System.Collections.Generic;

namespace H6Game.Base
{

    /// <summary>
    /// 管理服务端分布式网络映射组件。
    /// </summary>
    public class NetMapManager
    {
        private readonly HashSet<NetEndPointMessage> ConnectEntities = new HashSet<NetEndPointMessage>();
        private readonly Dictionary<int, NetEndPointMessage> ChannelIdMapMsg = new Dictionary<int, NetEndPointMessage>();
        private readonly Dictionary<int, ANetChannel> HCodeMapChannel = new Dictionary<int, ANetChannel>();

        public IEnumerable<NetEndPointMessage> Entities { get { return ConnectEntities; } }

        /// <summary>
        /// 判断是否存在。
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Existed(NetEndPointMessage message)
        {
            return ConnectEntities.Contains(message);
        }

        /// <summary>
        /// 删除一条连接消息。
        /// </summary>
        /// <param name="message"></param>
        public void Remove(NetEndPointMessage message)
        {
            if (ConnectEntities.Remove(message))
            {
                if (!HCodeMapChannel.TryGetValue(message.GetHashCode(), out ANetChannel channel))
                    return;

                HCodeMapChannel.Remove(message.GetHashCode());
                ChannelIdMapMsg.Remove(channel.Id);
            }
        }

        /// <summary>
        /// 新增一个连接消息，非中心服务需要跟Channel映射。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public void Add(ANetChannel channel, NetEndPointMessage message)
        {
            if (ConnectEntities.Contains(message))
                return;

            this.ConnectEntities.Add(message);
            ChannelIdMapMsg[channel.Id] = message;
            HCodeMapChannel[message.GetHashCode()] = channel;
        }

        /// <summary>
        /// 用Channel Id获取相应的连接消息。
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryGetFromChannelId(ANetChannel channel, out NetEndPointMessage message)
        {
            return ChannelIdMapMsg.TryGetValue(channel.Id, out message);
        }
    }
}
