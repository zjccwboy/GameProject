using H6Game.Base.Entity;
using System.Net;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private SysConfig config { get;} = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        private Session connectSession;
        private IPEndPoint loginServerEndPoint;

        public bool IsConnected { get; private set; }

        private OutNetComponent()
        {
            this.loginServerEndPoint = GetLoginServerEndPoint();
            this.Connecting(this.loginServerEndPoint);
        }

        public override void Update()
        {
            if(connectSession != null)
            {
                connectSession.Update();
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

            this.connectSession.Subscribe(this.connectSession.ConnectChannel, send, (p) =>
            {
                var response = p.Data.ProtoToObject(typeof(T));
                if (response == null)
                {
                    tcs.TrySetResult(default(T));
                    return;
                }
                tcs.TrySetResult((T)response);
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
            this.connectSession.Notice(this.connectSession.ConnectChannel, new Packet
            {
                MessageId = messageCmd,
                Data = bytes,
            });
        }

        private void Connecting(IPEndPoint endPoint)
        {
            if(connectSession != null)
            {
                connectSession.Dispose();
            }
            connectSession = new Session(endPoint, ProtocalType.Kcp);
            connectSession.OnClientConnected = (c) => { this.IsConnected = c.Connected; };
            connectSession.OnClientDisconnected = (c) => { this.IsConnected = c.Connected; };
            connectSession.Connect();
        }

        private IPEndPoint GetLoginServerEndPoint()
        {
            var hostInfo = Dns.GetHostEntry(config.OuNetHost);
            IPAddress ipAddress = hostInfo.AddressList[0];
            var endPoint = new IPEndPoint(ipAddress, config.OuNetConfig.Port);
            return endPoint;
        }
    }
}
