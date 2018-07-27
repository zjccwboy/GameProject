using H6Game.Base.Entity;
using H6Game.Message.InNetMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H6Game.Base
{
    public class ConnectEntity
    {
        public string IP { get; set; }
        public int Port { get; set; }
    }

    /// <summary>
    /// 管理整个服务端分布式的组件
    /// </summary>
    public class NetMapComponent : BaseComponent
    {
        private readonly LinkedList<ConnectEntity> connectEntities = new LinkedList<ConnectEntity>();

        public void Remove(DistributedMessageRp message)
        {
            ConnectEntity remove = null;
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

        public void Add(DistributedMessageRp message)
        {
            this.connectEntities.AddLast(new ConnectEntity
            {
                IP = message.IP,
                Port = message.Port,
            });
        }

        public bool TryGetCenterIpEndPoint(out DistributedMessageRp message)
        {
            if (!this.connectEntities.Any())
            {
                message = null;
                return false;
            }

            var first = this.connectEntities.First.Value;
            message = new DistributedMessageRp
            {
                IP = first.IP,
                Port = first.Port,
            };
            return true;
        }

    }
}
