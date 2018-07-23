using H6Game.Base.Entity;
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

        public EndPointEntity GetRemoteEndPoint()
        {
            return new EndPointEntity();
        }
    }
}
