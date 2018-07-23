using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Message;

namespace H6Game.Component.Base
{
    /// <summary>
    /// 分布式系统消息处理
    /// </summary>
    public class DistributedMessageHandler : IMessageHandler
    {
        public ANetChannel Channel { get; set; }
        public ANetService NetService { get; set; }

        public void DoReceive(Packet packet)
        {

        }
    }
}
