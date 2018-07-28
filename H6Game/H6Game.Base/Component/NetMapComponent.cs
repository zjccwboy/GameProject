using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{

    /// <summary>
    /// 管理整个服务端分布式的组件
    /// </summary>
    public class NetMapManager
    {
        private readonly LinkedList<NetEndPointMessage> connectEntities = new LinkedList<NetEndPointMessage>();
        private readonly Dictionary<int, NetEndPointMessage> channelIdMapMsg = new Dictionary<int, NetEndPointMessage>();
        private readonly Dictionary<int, ANetChannel> hCodeMapChannel = new Dictionary<int, ANetChannel>();

        public List<NetEndPointMessage> ConnectEntities
        {
            get
            {
                return connectEntities.ToList();
            }
        }

        public bool TryGetFromChannelId(int channelId, out NetEndPointMessage message)
        {
            return channelIdMapMsg.TryGetValue(channelId, out message);
        }

        public void Remove(NetEndPointMessage message)
        {
            NetEndPointMessage remove = null;
            foreach (var entiry in connectEntities)
            {
                if(entiry.HashCode() == message.HashCode())
                {
                    remove = entiry;
                    hCodeMapChannel.Remove(message.HashCode());
                    break;
                }
            }

            if(remove != null)
            {
                connectEntities.Remove(remove);
            }
        }

        public void Add(NetEndPointMessage message)
        {
            foreach(var entity in connectEntities)
            {
                if(entity.IP == message.IP && entity.Port == message.Port)
                {
                    return;
                }
            }

            this.connectEntities.AddLast(new NetEndPointMessage
            {
                IP = message.IP,
                Port = message.Port,
            });
        }

        public void AddChannelMaping(ANetChannel channel, NetEndPointMessage message)
        {
            channelIdMapMsg[channel.Id] = message;
            hCodeMapChannel[message.HashCode()] = channel;
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

        public void UpdateMapping(IEnumerable<NetEndPointMessage> entities)
        {
            connectEntities.Clear();
            channelIdMapMsg.Clear();
            foreach (var entity in entities)
            {
                connectEntities.AddLast(entity);
                if(hCodeMapChannel.TryGetValue(entity.GetHashCode(), out ANetChannel channel))
                {
                    AddChannelMaping(channel, entity);
                }

                if (!hCodeMapChannel.ContainsKey(entity.HashCode()))
                {
                    hCodeMapChannel.Remove(entity.HashCode());
                }
            }
        }

        public bool TryGetCenterIpEndPoint(out NetEndPointMessage message)
        {
            if (!this.connectEntities.Any())
            {
                message = null;
                return false;
            }

            var first = this.connectEntities.First.Value;
            message = new NetEndPointMessage
            {
                IP = first.IP,
                Port = first.Port,
            };
            return true;
        }
    }
}
