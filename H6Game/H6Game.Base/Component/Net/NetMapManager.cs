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

        public List<NetEndPointMessage> ConnectEntities
        {
            get
            {
                return connectEntities.OrderBy(c=>c.Order).ToList();
            }
        }

        public bool Existed(NetEndPointMessage message)
        {
            return hCodeMapChannel.ContainsKey(message.GetHashCode());
        }

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

        public void Add(ANetChannel channel, NetEndPointMessage message)
        {
            if (connectEntities.Contains(message))
                return;

            message.Order = MessageOrderCreator.CreateId();
            this.connectEntities.Add(message);
            channelIdMapMsg[channel.Id] = message;
            hCodeMapChannel[message.GetHashCode()] = channel;
        }

        public bool TryGetFromChannelId(ANetChannel channel, out NetEndPointMessage message)
        {
            return channelIdMapMsg.TryGetValue(channel.Id, out message);
        }

        public bool TryGetMappingMessage(ANetChannel channel, out NetEndPointMessage message)
        {
            return this.channelIdMapMsg.TryGetValue(channel.Id, out message);
        }

        public void Clear()
        {
            connectEntities.Clear();
            channelIdMapMsg.Clear();
            hCodeMapChannel.Clear();
        }

        public void UpdateMapping(IEnumerable<NetEndPointMessage> entities)
        {
            entities = entities.OrderBy(c => c.Order);
            connectEntities.Clear();
            channelIdMapMsg.Clear();
            foreach (var entity in entities)
            {
                connectEntities.Add(entity);
                if(hCodeMapChannel.TryGetValue(entity.GetHashCode(), out ANetChannel channel))
                    Add(channel, entity);

                if (!hCodeMapChannel.ContainsKey(entity.GetHashCode()))
                    hCodeMapChannel.Remove(entity.GetHashCode());
            }
        }
    }
}
