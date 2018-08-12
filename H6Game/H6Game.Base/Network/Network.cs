using System.Net;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息发送处理类
    /// </summary>
    public sealed class Network : IDisposable
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

        public void Update()
        {
            Session.Update();
        }

        public static Session CreateSession(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var session = new Session(endPoint, protocalType);
            return session;
        }

        public static Network CreateAcceptor(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var session = new Session(endPoint, protocalType);
            session.Accept();
            var network = new Network(session, session.NService, null);
            return network;
        }

        public static Network CreateConnecting(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var session = new Session(endPoint, protocalType);
            var channel = session.Connect();
            if(protocalType == ProtocalType.Kcp)
            {
                channel.RecvParser = new PacketParser(1400);
                channel.SendParser = new PacketParser(1400);
            }
            else if( protocalType == ProtocalType.Tcp)
            {
                channel.RecvParser = new PacketParser();
                channel.SendParser = new PacketParser();
            }
            var network = new Network(session, session.NService, channel);
            return network;
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
