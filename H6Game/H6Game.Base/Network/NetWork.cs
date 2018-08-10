using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 消息发送处理类
    /// </summary>
    public class Network
    {
        public ANetChannel Channel { get;}
        public ANetService NetService { get;}
        public Session Session { get;}
        public Packet SendPacket { get { return this.Channel.SendParser.Packet; } }
        public Packet RecvPacket { get { return this.Channel.RecvParser.Packet; } }

        public Network(Session session, ANetService netService, ANetChannel channel)
        {
            this.Session = session;
            this.NetService = netService;
            this.Channel = channel;
        }

        public static Session CreateSession(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var session = new Session(endPoint, protocalType);
            return session;
        }
    }
}
