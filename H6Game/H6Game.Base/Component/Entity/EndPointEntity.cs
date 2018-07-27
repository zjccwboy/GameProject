using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Message;
using H6Game.Message.InNetMessage;

namespace H6Game.Base.Entity
{
    public class EndPointEntity
    {
        public int Port { get; set; }
        public string IP { get; set; }
        public string Desc { get; set; }

        public DistributedMessage DMessage
        {
            get
            {
                var message = new DistributedMessage
                {
                    Port = this.Port,
                    IP = this.IP,
                };
                return message;
            }
        }
    }
}