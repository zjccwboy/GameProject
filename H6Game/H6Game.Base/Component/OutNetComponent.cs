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
        }

        public override void Start()
        {
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
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="messageCmd"></param>
        /// <param name="isCompress"></param>
        /// <param name="isEncrypt"></param>
        /// <returns></returns>
        public Task<T> CallMessage<T>(T data, int messageCmd, bool isCompress = false, bool isEncrypt = false)
        {
            var tcs = new TaskCompletionSource<T>();
            this.ConnectSession.Subscribe(this.ConnectSession.ConnectChannel, data, (p) =>
            {
                var response = p.Read<T>();
                tcs.TrySetResult(response);
            }, messageCmd);
            return tcs.Task;
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
