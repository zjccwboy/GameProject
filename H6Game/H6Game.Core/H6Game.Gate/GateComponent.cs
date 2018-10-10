﻿using H6Game.Actor;
using H6Game.Base;
using System.Collections.Generic;

namespace H6Game.Gate
{
    [SingleCase]
    [ComponentEvent(EventType.Awake)]
    public class GateComponent : NetComponentSubscriber
    {
        private Dictionary<int, BaseActorComponent> OutNetActors { get; } = new Dictionary<int, BaseActorComponent>();
        private Dictionary<int, Network> ActorNetworks { get; } = new Dictionary<int, Network>();
        private ActorPoolComponent ActorPool { get; set; }
        private NetDistributionsComponent Distributions { get; set; }
        public Network GateNetwork { get; private set; }

        public override void Awake()
        {
            this.Distributions = Game.Scene.GetComponent<NetDistributionsComponent>();
            this.ActorPool = Game.Scene.GetComponent<ActorPoolComponent>();

            if (this.Distributions == null)
                throw new ComponentException("DistributionsComponent组件没有加载。");

            this.GateNetwork = this.Distributions.OutAcceptNetwork;
            this.GateNetwork.Session.OnServerConnected += network =>
            {
                if (this.Distributions.IsProxyServer)
                    return;
            };

            this.GateNetwork.Session.OnServerDisconnected += network =>
            {
                if (this.Distributions.IsProxyServer)
                    return;

                if (!OutNetActors.ContainsKey(network.Channel.Id))
                    return;

                var actor = this.OutNetActors[network.Channel.Id];
                actor.Dispose();
            };
        }
    }
}
