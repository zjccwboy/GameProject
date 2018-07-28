using H6Game.Base.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private SysConfig config { get; set; }
        private Session connectSession;
        public override void Start()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        }

        private void HandleConnect()
        {
            IPHostEntry hostInfo = Dns.GetHostEntry("www.xxxxxx.xxx");
            IPAddress ipAddress = hostInfo.AddressList[0];
        }
    }
}
