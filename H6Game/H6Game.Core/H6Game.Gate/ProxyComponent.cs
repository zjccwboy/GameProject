using H6Game.Base;

namespace H6Game.Gate
{
    [SingleCase]
    [ComponentEvent(EventType.Awake)]
    public class ProxyComponent : NetController
    {
        private NetDistributionsComponent Distributions { get; set; }
        public Network GateNetwork { get; private set; }

        public override void Awake()
        {
            this.Distributions = Game.Scene.GetComponent<NetDistributionsComponent>();
            if (this.Distributions == null)
                throw new ComponentException("DistributionsComponent组件没有加载。");

            this.GateNetwork = this.Distributions.OutAcceptNetwork;
        }

        [NetCommand(SysNetCommand.GetGateEndPoint)]
        public NetEndPointMessage GetGateEndPointMessage()
        {
            if (this.Distributions.IsProxyServer)
            {
                return this.Distributions.OutNetMapManager.GetGoodConnectedInfo();
            }
            return null;
        }
    }
}
