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
        public OuterEndPointMessage SubscribeOnGetGateEndPointMessage()
        {
            if (this.Distributions.IsProxyServer)
            {
                return this.Distributions.OuterNetMapManager.GetGoodConnectedInfo();
            }
            return null;
        }
    }
}
