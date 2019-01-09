using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Actor
{
    [NetCommand(MSGCommand.AddActorCmd)]
    public class SubscribeOnRemoteAddActor : NetSubscriber<ActorSyncMessage>
    {
        private ActorComponentStorage ActorStorage { get; } = Game.Scene.GetComponent<ActorComponentStorage>();

        protected override void Subscribe(Network network, ActorSyncMessage message, ushort netCommand)
        {
            var logs = $"CMD:{netCommand} ActorId:{message.ActorId} MSG:{message.ToJson()}";
            Log.Info(logs, LoggerBllType.System);
            ActorStorage.AddActor(network, message);
        }
    }

    [NetCommand(MSGCommand.RemoveActorCmd)]
    public class SubscribeOnRemoteActorRemove : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, ushort netCommand)
        {
            Log.Info($"CMD：{netCommand} 删除AcotrId:{message} ", LoggerBllType.System);
            if (Game.Scene.GetComponent(message, out BaseComponent component))
                component.Dispose();
        }
    }

    [NetCommand(MSGCommand.SyncActorInfoCmd)]
    public class SubscribeOnRemoteSyncFullActorInfo : NetSubscriber
    {
        private ActorComponentStorage ActorStorage { get; } = Game.Scene.GetComponent<ActorComponentStorage>();
        protected override void Subscribe(Network network, ushort netCommand)
        {
            Log.Info(netCommand, LoggerBllType.System);
            this.ActorStorage.SendLocalActors(network);
        }
    }

}
