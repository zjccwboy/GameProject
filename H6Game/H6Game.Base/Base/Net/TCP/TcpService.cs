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
        private IPEndPoint endPoint;
        private readonly SocketAsyncEventArgs innArgs = new SocketAsyncEventArgs();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endPoint">Ip/端口</param>
        /// <param name="session"></param>
        public TcpService(IPEndPoint endPoint, Session session, NetServiceType serviceType) : base(session)
        {
            this.ServiceType = serviceType;
            this.endPoint = endPoint;
        }


        /// <summary>
        /// 开始监听并接受连接请求
        /// </summary>
        /// <returns></returns>
        public override bool Accept()
        {
            if (acceptor == null)
            {
                this.acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.acceptor.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                this.innArgs.Completed += this.OnComplete;
                try
                {
                    this.acceptor.Bind(this.endPoint);
                    this.acceptor.Listen(1000);
                }
                catch(Exception e)
                {
#if SERVER
                    LogRecord.Log(LogLevel.Error, "Accept", e);
#endif
                    return false;
                }

            }

            this.innArgs.AcceptSocket = null;
            if (this.acceptor.AcceptAsync(this.innArgs))
            {
                return true;
            }
            OnComplete(this, this.innArgs);

            return true;
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    OneThreadSynchronizationContext.Instance.Post(this.OnAcceptComplete, e);
                    break;
                default:
                    throw new Exception($"socket error: {e.LastOperation}");
            }
        }

        private void OnAcceptComplete(object o)
        {
            if (this.acceptor == null)
            {
                return;
            }
            SocketAsyncEventArgs e = o as SocketAsyncEventArgs;

            if (e.SocketError != SocketError.Success)
            {
#if SERVER
                LogRecord.Log(LogLevel.Warn, "OnAcceptComplete", $"接受连接发生错误.");
#endif
                return;
            }
            var channel = new TcpChannel(this.endPoint, e.AcceptSocket, this);
            channel.RemoteEndPoint = e.AcceptSocket.RemoteEndPoint as IPEndPoint;
            HandleAccept(channel);

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
                ClientChannel = new TcpChannel(endPoint, this);
                ClientChannel.OnConnect = HandleConnect;
                ClientChannel.StartConnecting();
            }
            return ClientChannel;
        }

        /// <summary>
        /// 处理接受连接成功回调
        /// </summary>
        /// <param name="channel"></param>
        private void HandleAccept(ANetChannel channel)
        {
            try
            {
                channel.Connected = true;
                channel.Handler = new MessageHandler
                {
                    Session = this.Session,
                    Channel = channel,
                    NetService = this,
                };
                channel.OnDisConnect = HandleDisConnectOnServer;
                channel.OnReceive += channel.Handler.DoReceive;
                this.AddChannel(channel);
                this.OnServerConnected?.Invoke(channel);
#if SERVER
                LogRecord.Log(LogLevel.Info, "HandleAccept", $"接受客户端:{channel.RemoteEndPoint}连接成功.");
#endif
            }
            catch (Exception e)
            {
#if SERVER
                LogRecord.Log(LogLevel.Warn, "HandleAccept", e);
#endif
            }
        }

        /// <summary>
        /// 处理连接成功回调
        /// </summary>
        /// <param name="channel"></param>
        private void HandleConnect(ANetChannel channel)
        {
            try
            {
                channel.Connected = true;
                channel.Handler = new MessageHandler
                {
                    Session = this.Session,
                    Channel = channel,
                    NetService = this,
                };
                channel.OnDisConnect = HandleDisConnectOnClient;
                channel.OnReceive += channel.Handler.DoReceive;
                this.AddChannel(channel);
                this.OnClientConnected?.Invoke(channel);
#if SERVER
                LogRecord.Log(LogLevel.Info, "HandleConnect", $"连接服务端:{channel.RemoteEndPoint}成功.");
#endif
            }
            catch (Exception e)
            {
#if SERVER
                LogRecord.Log(LogLevel.Warn, "HandleConnect", e);
#endif
            }
        }
     }
}
