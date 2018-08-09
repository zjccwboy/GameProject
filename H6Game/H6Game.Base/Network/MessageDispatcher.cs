using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Message;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发
    /// </summary>
    public class MessageDispatcher
    {
        public ANetChannel Channel { get; set; }
        public ANetService NetService { get; set; }
        public Session Session { get; set; }

        public void DoReceive(Packet packet)
        {
            try
            {
                var handler = HandlerMSGFactory.Get(packet.MessageId);
                handler.Receive(this.Session, this.Channel);
            }
            catch (Exception e)
            {
                this.Log(LogLevel.Error, "DoReceive", e.ToString());
            }
        }
    }
}
