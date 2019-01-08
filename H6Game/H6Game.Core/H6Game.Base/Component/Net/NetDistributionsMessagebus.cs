﻿using H6Game.Base.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base.Component.Net
{
    [NetCommand(SysNetCommand.AddInnerServerCmd)]
    public class SubscribeOnAddServerConnection : NetSubscriber<NetEndPointMessage>
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, NetEndPointMessage message, int netCommand)
        {
            if (Distributions.InnerNetMapManager.Existed(message))
                return;

            Distributions.AddNetwork(message);
            if (!Distributions.IsCenterServer)
                return;

            Distributions.BroadcastAddNewService(network, message);
        }
    }

    [NetCommand(SysNetCommand.GetOutServerCmd)]
    public class SubscribeOnSyncOutNetMessage : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(Distributions.OuterNetMessage);
        }
    }

    [NetCommand(SysNetCommand.GetInServerCmd)]
    public class SubscribeOnSyncInnerNetMessage : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(Distributions.InnerNetMessage);
        }
    }

    [NetCommand(SysNetCommand.GetServerType)]
    public class SubscribeOnGetServerType : NetSubscriber
    {
        private NetDistributionsComponent Distributions { get; } = Game.Scene.GetComponent<NetDistributionsComponent>();
        protected override void Subscribe(Network network, int netCommand)
        {
            if (Distributions.IsCenterServer)
            {
                network.Response(ServerType.CenterServer);
            }
            else if (Distributions.IsProxyServer)
            {
                network.Response(ServerType.ProxyServer);
            }
            else
            {
                network.Response(ServerType.Default);
            }
        }
    }
}
