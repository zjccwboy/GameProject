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

        public bool Existed(ANetChannel channel, NetEndPointMessage message)
        {
            if(!channelIdMapMsg.TryGetValue(channel.Id, out NetEndPointMessage value))
                return false;

            return message == value;
        }

        public bool TryGetFromChannelId(ANetChannel channel, out NetEndPointMessage message)
        {
            return channelIdMapMsg.TryGetValue(channel.Id, out message);
        }

        public void Remove(NetEndPointMessage message)
        {
            var entities = connectEntities.ToList();
            foreach (var entiry in entities)
            {
                if(entiry == message)
                {
                    if(hCodeMapChannel.TryGetValue(message.GetHashCode(), out ANetChannel channel))
                    {
                        hCodeMapChannel.Remove(message.GetHashCode());
                        channelIdMapMsg.Remove(channel.Id);
                    }
                    connectEntities.Remove(entiry);
                    break;
                }
            }
        }

        public void Add(NetEndPointMessage message)
        {
            message.Order = MessageOrderCreator.CreateId();
            foreach (var entity in connectEntities)
            {
                if(entity == message)
                    return;
            }

            this.connectEntities.Add(new NetEndPointMessage
            {
                IP = message.IP,
                Port = message.Port,
            });
        }

        public void AddChannelMaping(ANetChannel channel, NetEndPointMessage message)
        {
            channelIdMapMsg[channel.Id] = message;
            hCodeMapChannel[message.GetHashCode()] = channel;
        }

        public bool TryGetMappingMessage(ANetChannel channel, out NetEndPointMessage message)
        {
            if(this.channelIdMapMsg.TryGetValue(channel.Id, out NetEndPointMessage value))
            {
                message = value;
                return true;
            }
            message = null;
            return false;
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
                    AddChannelMaping(channel, entity);

                if (!hCodeMapChannel.ContainsKey(entity.GetHashCode()))
                    hCodeMapChannel.Remove(entity.GetHashCode());
            }
        }
    }
}
