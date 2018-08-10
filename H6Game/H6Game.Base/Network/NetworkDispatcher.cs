using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息分发给所有订阅MessageCmd的Hadler类
    /// </summary>
    public class NetworkDispatcher
    {
        private ANetChannel Channel { get;}
        private ANetService NetService { get;}
        private Session Session { get;}
        public Network Network { get;}

        public NetworkDispatcher(Session session, ANetService netService, ANetChannel channel)
        {
            this.Session = session;
            this.NetService = netService;
            this.Channel = channel;
            this.Network = new Network(this.Session, this.NetService, this.Channel);
        }

        public void DoReceive(Packet packet)
        {
            try
            {
                var handler = HandlerMSGFactory.Get(packet.MessageId);
                handler.Receive(this.Network);
            }
            catch (Exception e)
            {
                this.Log(LogLevel.Error, "DoReceive", e.ToString());
            }
        }
    }
}