using H6Game.Actor;
using H6Game.Base;
using H6Game.Hotfix.Enums;
using System;
using System.Collections.Generic;
using H6Game.Hotfix;


namespace H6Game.Gate
{
    [SubscriberCMD(MessageCMD.AddClientActor)]
    public class PlayerActorAdSubscriber : AMsgSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, int messageCmd)
        {
            var actor = Game.Scene.GetComponent<ActorPoolComponent>().GetActor(message);
            Game.Scene.GetComponent<GateComponent>().AddActor(network, actor);
        }
    }

    [SingleCase]
    [ComponentEvent(EventType.Awake)]
    public class GateComponent : BaseComponent
    {
        private Dictionary<int, BaseActorEntityComponent> OutNetActors { get; } = new Dictionary<int, BaseActorEntityComponent>();
        public Network GateNetwork { get; private set; }

        public override void Awake()
        {
            var distributionsComponent = Game.Scene.GetComponent<DistributionsComponent>();
            if (distributionsComponent == null)
                throw new ComponentException("DistributionsComponent组件没有加载。");

            this.GateNetwork = distributionsComponent.OutAcceptNetwork;
            this.GateNetwork.Session.OnServerDisconnected += c =>
            {
                if (!OutNetActors.ContainsKey(c.Id))
                    return;

                var actor = this.OutNetActors[c.Id];
            };
        }

        public void AddActor(Network network, BaseActorEntityComponent actor)
        {
            OutNetActors[network.Channel.Id] = actor;
        }

    }
}
