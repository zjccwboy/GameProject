using System.Net;
using System;

namespace H6Game.Base
{
    /// <summary>
    /// 消息发送处理类
    /// </summary>
    public sealed class Network : IDisposable
    {
        /// <summary>
        /// 通讯管道类。
        /// </summary>
        public ANetChannel Channel { get; set; }

        /// <summary>
        /// 通讯会话类。
        /// </summary>
        public Session Session { get; private set; }

        /// <summary>
        /// 通讯服务类。
        /// </summary>
        public ANetService NetService { get { return this.Channel.Session.NService; } }

        /// <summary>
        /// 发送数据包缓冲区。
        /// </summary>
        public Packet SendPacket { get { return this.Channel.SendParser.Packet; } }

        /// <summary>
        /// 接收数据包缓冲区。
        /// </summary>
        public Packet RecvPacket { get { return this.Channel.RecvParser.Packet; } }

        /// <summary>
        /// 网络Id
        /// </summary>
        public int Id { get { return this.Channel.Id; } }

        private Network(){}

        /// <summary>
        /// 通讯状态更新入口。
        /// </summary>
        public void Update()
        {
            Session.Update();
        }


        /// <summary>
        /// 创建一个监听连接的Network。
        /// </summary>
        /// <param name="endPoint">服务端监听IP端口。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateAcceptor(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var network = new Network();
            var session = new Session(endPoint, network, protocalType);
            network.Session = session;
            session.Accept();
            return network;
        }


        /// <summary>
        /// 创建一个监听连接的Network。
        /// </summary>
        /// <param name="endPoint">服务端监听IP端口。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <param name="connectedAction">连接成功回调。</param>
        /// <param name="disconnectedAction">连接断开回调。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateAcceptor(IPEndPoint endPoint, ProtocalType protocalType
            , Action<Network> connectedAction, Action<Network> disconnectedAction)
        {
            var network = new Network();
            var session = new Session(endPoint, network, protocalType);
            network.Session = session;
            session.OnServerConnected += connectedAction;
            session.OnServerDisconnected += disconnectedAction;
            session.Accept();
            return network;
        }

        /// <summary>
        /// 创建一个连接的Network。
        /// </summary>
        /// <param name="endPoint">服务端IP端口。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateConnector(IPEndPoint endPoint, ProtocalType protocalType)
        {
            var network = new Network();
            var session = new Session(endPoint, network, protocalType);
            network.Session = session;
            session.Connect();
            return network;
        }

        /// <summary>
        /// 创建一个连接的Network。
        /// </summary>
        /// <param name="endPoint">连接服务端IP端口。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <param name="connectedAction">连接成功回调。</param>
        /// <param name="disconnectedAction">连接断开回调。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateConnector(IPEndPoint endPoint, ProtocalType protocalType
            , Action<Network> connectedAction, Action<Network> disconnectedAction)
        {
            var network = new Network();
            var session = new Session(endPoint, network, protocalType);
            network.Session = session;
            session.OnClientConnected += connectedAction;
            session.OnClientDisconnected += disconnectedAction;
            session.Connect();
            return network;
        }

        /// <summary>
        /// 创建一个WebSocket监听连接的Network。
        /// </summary>
        /// <param name="httpPrefixed">WebSocket监听前缀。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateWebSocketAcceptor(string httpPrefixed)
        {
            var network = new Network();
            var session = new Session(httpPrefixed, network, ProtocalType.Wcp);
            network.Session = session;
            session.Accept();
            return network;
        }

        /// <summary>
        /// 创建一个WebSocket监听连接的Network。
        /// </summary>
        /// <param name="httpPrefixed">WebSocket监听前缀。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <param name="connectedAction">连接成功回调。</param>
        /// <param name="disconnectedAction">连接断开回调。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateWebSocketAcceptor(string httpPrefixed, Action<Network> connectedAction, Action<Network> disconnectedAction)
        {
            var network = new Network();
            var session = new Session(httpPrefixed, network, ProtocalType.Wcp);
            network.Session = session;
            session.OnServerConnected += connectedAction;
            session.OnServerDisconnected += disconnectedAction;
            session.Accept();
            return network;
        }

        /// <summary>
        /// 创建一个WebSocket连接的Network。
        /// </summary>
        /// <param name="endPoint">服务端IP端口。</param>
        /// <param name="protocalType">通讯协议类型。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateWebSocketConnector(string httpPrefixed)
        {
            var network = new Network();
            var session = new Session(httpPrefixed, network, ProtocalType.Wcp);
            network.Session = session;
            session.Connect();
            return network;
        }

        /// <summary>
        /// 创建一个WebSocket连接的Network。
        /// </summary>
        /// <param name="httpPrefixed">连接服务端IP端口。</param>
        /// <param name="connectedAction">连接成功回调。</param>
        /// <param name="disconnectedAction">连接断开回调。</param>
        /// <returns>Network 网络类对象</returns>
        public static Network CreateWebSocketConnector(string httpPrefixed, Action<Network> connectedAction, Action<Network> disconnectedAction)
        {
            var network = new Network();
            var session = new Session(httpPrefixed, network, ProtocalType.Wcp);
            network.Session = session;
            session.OnClientConnected += connectedAction;
            session.OnClientDisconnected += disconnectedAction;
            session.Connect();
            return network;
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
