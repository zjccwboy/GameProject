using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{

    /// <summary>
    /// 管理整个服务端分布式的组件
    /// </summary>
    public class InNetMapComponent : BaseComponent
    {
        private readonly LinkedList<NetEndPointMessage> connectEntities = new LinkedList<NetEndPointMessage>();
        private readonly Dictionary<int, NetEndPointMessage> connectMaping = new Dictionary<int, NetEndPointMessage>();

        public List<NetEndPointMessage> ConnectEntities
        {
            get
            {
                return connectEntities.ToList();
            }
        }

        public override void Start()
        {

        }

        public void Remove(NetEndPointMessage message)
        {
            NetEndPointMessage remove = null;
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

        public void AddMaping(ANetChannel channel, NetEndPointMessage message)
        {
            connectMaping[channel.Id] = message;
        }

        public void RemoveMaping(ANetChannel channel)
        {
            connectMaping.Remove(channel.Id);
        }

        public bool TryGetMappingMessage(ANetChannel channel, out NetEndPointMessage message)
        {
            if(this.connectMaping.TryGetValue(channel.Id, out NetEndPointMessage value))
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
            foreach(var entity in entities)
            {
                connectEntities.AddLast(entity);
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
