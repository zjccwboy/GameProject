using H6Game.Base.Entity;
using H6Game.Message.InNetMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    /// <summary>
    /// 管理整个服务端分布式的组件
    /// </summary>
    public class NetMapComponent : BaseComponent
    {

        public bool TryGetCenterIpEndPoint(out DistributedMessageRp message)
        {
            message = new DistributedMessageRp();
            return true;
        }

    }
}
