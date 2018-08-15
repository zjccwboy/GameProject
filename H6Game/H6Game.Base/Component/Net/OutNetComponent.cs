﻿using System.Net;

namespace H6Game.Base
{
    [Event(EventType.Awake | EventType.Start | EventType.Update)]
    [SingletCase]
    public sealed class OutNetComponent : BaseComponent
    {
        private SysConfig Config { get; set; }

        public Network Network { get; private set; }

        public bool IsConnected { get { return this.Network.Channel.Connected; } }

        public override void Awake()
        {
            this.Config = Game.Scene.GetComponent<NetConfigComponent>().ConfigEntity;
        }

        public override void Start()
        {
            this.Connecting(GetLoginServerEndPoint());
        }

        public override void Update()
        {
            if(Network != null)
            {
                Network.Update();
            }
        }

        public void ReConnect()
        {
            if (this.Network != null)
                this.Network.Session.Dispose();

            this.Network = null;

            Connecting(GetLoginServerEndPoint());
        }

        private void Connecting(IPEndPoint endPoint)
        {
            this.Network = Network.CreateConnecting(endPoint, ProtocalType.Kcp);
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
