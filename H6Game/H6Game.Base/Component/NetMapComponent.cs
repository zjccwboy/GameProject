using H6Game.Message.InNetMessage;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{

    /// <summary>
    /// 管理整个服务端分布式的组件
    /// </summary>
    public class NetMapComponent : BaseComponent
    {
        private readonly LinkedList<DistributedMessage> connectEntities = new LinkedList<DistributedMessage>();
        private readonly Dictionary<int, DistributedMessage> connectMaping = new Dictionary<int, DistributedMessage>();

        public List<DistributedMessage> ConnectEntities
        {
            get
            {
                return connectEntities.ToList();
            }
        }

        public void Remove(DistributedMessage message)
        {
            DistributedMessage remove = null;
            foreach (var entiry in connectEntities)
            {
                if(entiry.IP == message.IP && entiry.Port == message.Port)
                {
                    remove = entiry;
                    break;
                }
            }

            if(remove != null)
            {
                connectEntities.Remove(remove);
            }
        }

        public void Add(DistributedMessage message)
        {
            foreach(var entity in connectEntities)
            {
                if(entity.IP == message.IP && entity.Port == message.Port)
                {
                    return;
                }
            }

            this.connectEntities.AddLast(new DistributedMessage
            {
                IP = message.IP,
                Port = message.Port,
            });
        }

        public void AddMaping(ANetChannel channel, DistributedMessage message)
        {
            connectMaping[channel.Id] = message;
        }

        public void RemoveMaping(ANetChannel channel)
        {
            connectMaping.Remove(channel.Id);
        }

        public bool TryGetMappingMessage(ANetChannel channel, out DistributedMessage message)
        {
            if(this.connectMaping.TryGetValue(channel.Id, out DistributedMessage value))
            {
                message = value;
                return true;
            }
            message = null;
            return false;
        }

        public void UpdateMapping(IEnumerable<DistributedMessage> entities)
        {
            connectEntities.Clear();
            foreach(var entity in entities)
            {
                connectEntities.AddLast(entity);
            }
        }

        public bool TryGetCenterIpEndPoint(out DistributedMessage message)
        {
            if (!this.connectEntities.Any())
            {
                message = null;
                return false;
            }

            var first = this.connectEntities.First.Value;
            message = new DistributedMessage
            {
                IP = first.IP,
                Port = first.Port,
            };
            return true;
        }

    }
}
