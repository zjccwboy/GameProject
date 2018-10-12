using H6Game.Base;
using System.Collections.Generic;

namespace H6Game.Gate
{
    [SingleCase]
    [ComponentEvent(EventType.Awake)]
    public class ProxyComponent : NetComponentSubscriber
    {
        private NetDistributionsComponent Distributions { get; set; }
        public List<NetAcceptorComponent> OuterAccepts { get; private set; }

        public override void Awake()
        {
            this.Distributions = Game.Scene.GetComponent<NetDistributionsComponent>();
            if (this.Distributions == null)
                throw new ComponentException("DistributionsComponent组件没有加载。");

            this.OuterAccepts = this.Distributions.OuterAccepts;
        }

        [NetCommand(SysNetCommand.GetGateEndPoint)]
        public NetEndPointMessage SubscribeOnGetGateEndPointMessage(int protocalType)
        {
            if (this.Distributions.IsProxyServer)
            {
                var outer = this.Distributions.OuterNetMapManager.GetGoodConnectedInfo();
                if (outer == null)
                    return null;

                if (protocalType == (int)ProtocalType.Kcp)
                    return outer.KcpEndPointMessage;
                else if (protocalType == (int)ProtocalType.Tcp)
                    return outer.TcpEndPointMessage;
                else if (protocalType == (int)ProtocalType.Wcp)
                    return outer.WcpEndPointMessage;
            }
            return null;
        }
    }
}
