using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发给所有订阅MessageCmd的Hadler类
    /// </summary>
    public class MessageDispatcher
    {
        private ANetChannel Channel { get;}
        private ANetService NetService { get;}
        private Session Session { get;}
        private NetWork NetWork { get;}

        public MessageDispatcher(Session session, ANetService netService, ANetChannel channel)
        {
            this.Session = session;
            this.NetService = netService;
            this.Channel = channel;
            this.NetWork = new NetWork(this.Session, this.NetService, this.Channel);
        }

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