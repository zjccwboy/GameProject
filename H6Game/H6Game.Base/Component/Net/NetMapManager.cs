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
        private readonly Dictionary<int, NetEndPointMessage> channelIdMapMsg = new Dictionary<int, NetEndPointMessage>();
        private readonly Dictionary<int, ANetChannel> hCodeMapChannel = new Dictionary<int, ANetChannel>();

        /// <summary>
        /// 连接消息集合
        /// </summary>
        public List<NetEndPointMessage> ConnectEntities
        {
            get
            {
                return connectEntities.OrderBy(c=>c.Order).ToList();
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
                    hCodeMapChannel.Remove(message.GetHashCode());
                    channelIdMapMsg.Remove(channel.Id);
                }
            }
        }

        /// <summary>
        /// 新增一个连接消息，中心服务不需要跟Channel映射
        /// </summary>
        /// <param name="message"></param>
        public void Add(NetEndPointMessage message)
        {
            if (connectEntities.Contains(message))
                return;

            connectEntities.Add(message);
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

            message.Order = MessageOrderCreator.CreateId();
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

        /// <summary>
        /// 清除所有连接消息与映射表
        /// </summary>
        public void Clear()
        {
            connectEntities.Clear();
            channelIdMapMsg.Clear();
            hCodeMapChannel.Clear();
        }

        /// <summary>
        /// 更新消息连接与映射信息
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateConnections(IEnumerable<NetEndPointMessage> entities)
        {
            foreach (var entity in entities)
            {
                connectEntities.Add(entity);
                foreach(var oldEntity in connectEntities)
                {
                    if (!entities.Contains(oldEntity))
                    {
                        Remove(oldEntity);
                        break;
                    }
                }
            }
        }
    }
}
