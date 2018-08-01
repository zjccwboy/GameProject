using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{

    /// <summary>
    /// 管理服务端分布式网络映射组件
    /// </summary>
    public class NetMapManager
    {
        private readonly HashSet<NetEndPointMessage> connectEntities = new HashSet<NetEndPointMessage>();
        private readonly ConcurrentDictionary<int, NetEndPointMessage> channelIdMapMsg = new ConcurrentDictionary<int, NetEndPointMessage>();
        private readonly ConcurrentDictionary<int, ANetChannel> hCodeMapChannel = new ConcurrentDictionary<int, ANetChannel>();

        /// <summary>
        /// 连接消息集合
        /// </summary>
        public List<NetEndPointMessage> ConnectEntities
        {
            get
            {
                return connectEntities.ToList();
            }
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Existed(NetEndPointMessage message)
        {
            return connectEntities.Contains(message);
        }

        /// <summary>
        /// 删除一条连接消息
        /// </summary>
        /// <param name="message"></param>
        public void Remove(NetEndPointMessage message)
        {
            if (connectEntities.Remove(message))
            {
                if (hCodeMapChannel.TryGetValue(message.GetHashCode(), out ANetChannel channel))
                {
                    hCodeMapChannel.TryRemove(message.GetHashCode(), out ANetChannel channelVal);
                    channelIdMapMsg.TryRemove(channel.Id, out NetEndPointMessage messageVal);
                }
            }
        }

        /// <summary>
        /// 新增一个连接消息，非中心服务需要跟Channel映射
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public void Add(ANetChannel channel, NetEndPointMessage message)
        {
            if (connectEntities.Contains(message))
                return;

            this.connectEntities.Add(message);
            channelIdMapMsg[channel.Id] = message;
            hCodeMapChannel[message.GetHashCode()] = channel;
        }

        /// <summary>
        /// 用Channel Id获取相应的连接消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TryGetFromChannelId(ANetChannel channel, out NetEndPointMessage message)
        {
            return channelIdMapMsg.TryGetValue(channel.Id, out message);
        }
    }
}
