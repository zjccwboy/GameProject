using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Message;

namespace H6Game.Base
{
    /// <summary>
    /// 分布式系统消息处理
    /// </summary>
    public class MessageHandler<Response> : IMessageHandler<Response> where Response : IResponse
    {
        public ANetChannel Channel { get; set; }
        public ANetService NetService { get; set; }
        public Session Session { get; set; }

        public void DoReceive(Packet packet)
        {
            //var data = packet.GetData<DistributedMessageRp>();
        }
    }
}
