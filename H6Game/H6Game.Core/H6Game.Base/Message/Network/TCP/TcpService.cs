using System;
using System.Net;
using System.Net.Sockets;

namespace H6Game.Base
{
    /// <summary>
    /// TCP通讯服务
    /// </summary>
    public class TcpService : ANetService
    {
        private readonly IPEndPoint EndPoint;
        private readonly SocketAsyncEventArgs InnArgs = new SocketAsyncEventArgs();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endPoint">Ip/端口</param>
        /// <param name="session">会话类</param>
        /// <param name="network">网络类</param>
        /// <param name="serviceType">通讯服务类型</param>
        public TcpService(IPEndPoint endPoint, Session session, Network network, NetServiceType serviceType) : base(session, network)
        {
            this.ServiceType = serviceType;
            this.EndPoint = endPoint;
            this.ProtocalType = ProtocalType.Tcp;
        }


        /// <summary>
        /// 开始监听并接受连接请求
        /// </summary>
        /// <returns></returns>
        public override void Accept()
        {
            if (Acceptor == null)
            {
                this.Acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.Acceptor.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                this.InnArgs.Completed += this.OnComplete;
                this.Acceptor.Bind(this.EndPoint);
                this.Acceptor.Listen(1000);
            }

            this.InnArgs.AcceptSocket = null;
            if (this.Acceptor.AcceptAsync(this.InnArgs))
                return;

            this.OnComplete(this, this.InnArgs);
        }

        public override void Update()
        {
            if (this.ServiceType == NetServiceType.Client && ClientChannel != null)
                ClientChannel.StartConnecting();

            foreach(var channel in this.Channels.Values)
            {
                channel.StartSend();
                channel.StartRecv();
            }

            this.CheckHeadbeat();
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ThreadCallbackContext.Instance.Post(this.OnAcceptComplete, e);
                    break;
                default:
                    throw new Exception($"socket error: {e.LastOperation}");
            }
        }

        private void OnAcceptComplete(object o)
        {
            if (this.Acceptor == null)
                return;

            SocketAsyncEventArgs e = o as SocketAsyncEventArgs;

            if (e.SocketError != SocketError.Success)
            {
                Log.Fatal($"接受连接发生错误.", LoggerBllType.System);
                return;
            }
            var channel = new TcpChannel(this.EndPoint, e.AcceptSocket, this, this.Network)
            {
                RemoteEndPoint = e.AcceptSocket.RemoteEndPoint as IPEndPoint
            };
            this.OnAccept(channel);

            this.Accept();
        }

        /// <summary>
        /// 发送连接请求
        /// </summary>
        /// <returns></returns>
        public override ANetChannel Connect()
        {
            if(this.ClientChannel == null)
            {
                ClientChannel = new TcpChannel(EndPoint, this, this.Network)
                {
                    OnConnect = OnConnect
                };
                ClientChannel.StartConnecting();
            }
            return this.ClientChannel;
        }
    }
}
