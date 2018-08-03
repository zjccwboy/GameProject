using H6Game.Base.Entity;
using System;
using System.Net;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private SysConfig Config { get;} = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private Session ConnectSession;
        private IPEndPoint LoginServerEndPoint;

        public bool IsConnected { get; private set; }

        public OutNetComponent()
        {
            this.LoginServerEndPoint = GetLoginServerEndPoint();
            this.Connecting(this.LoginServerEndPoint);
        }

        public override void Update()
        {
            if(ConnectSession != null)
            {
                ConnectSession.Update();
            }
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <returns></returns>
        public Task<T> CallMessage<T>(byte[] bytes, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        {
            var tcs = new TaskCompletionSource<T>();
            var send = new Packet
            {
                IsCompress = isCompress,
                IsEncrypt = isEncrypt,
                MessageId = messageCmd,
                Data = bytes,
            };

            this.ConnectSession.Subscribe(this.ConnectSession.ConnectChannel, send, (p) =>
            {
                var response = p.Data.ProtoToObject<T>();
                if (response == null)
                {
                    tcs.TrySetResult(default);
                    return;
                }
                tcs.TrySetResult(response);
            });
            return tcs.Task;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageCmd"></param>
        /// <param name="bytes"></param>
        /// <param name="rpcId"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        public void SendMessage(int messageCmd, byte[] bytes, int rpcId = 0, bool isCompress = false, bool isEncrypt = false)
        {
            this.ConnectSession.Notice(this.ConnectSession.ConnectChannel, new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            });
        }

        /// <summary>
        /// RPC请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="messageCmd"></param>
        /// <param name="rpcAction"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        public void CallMessage<T>(byte[] bytes, int messageCmd, Action<T> rpcAction, bool isCompress = false, bool isEncrypt = false)
        {
            var send = new Packet
            {
                IsCompress = isCompress,
                IsEncrypt = isEncrypt,
                MessageId = messageCmd,
                Data = bytes,
            };

            this.ConnectSession.Subscribe(this.ConnectSession.ConnectChannel, send, (p) =>
            {
                var response = p.Data.ProtoToObject<T>();
                rpcAction(response);
            });
        }

        private void Connecting(IPEndPoint endPoint)
        {
            if(ConnectSession != null)
            {
                ConnectSession.Dispose();
            }
            ConnectSession = new Session(endPoint, ProtocalType.Kcp);
            ConnectSession.OnClientConnected = (c) => { this.IsConnected = c.Connected; };
            ConnectSession.OnClientDisconnected = (c) => { this.IsConnected = c.Connected; };
            ConnectSession.Connect();
        }

        private IPEndPoint GetLoginServerEndPoint()
        {
            var hostInfo = Dns.GetHostEntry(Config.OuNetHost);
            IPAddress ipAddress = hostInfo.AddressList[0];
            var endPoint = new IPEndPoint(ipAddress, Config.OuNetConfig.Port);
            return endPoint;
        }
    }
}
