﻿using H6Game.Base;
using ProtoBuf;

namespace H6Game.Actor
{
    [NetMessageType(SysNetMessageType.ActorMessage)]
    [ProtoContract]
    public class ActorSyncMessage : IActorMessage
    {
        [ProtoMember(1)]
        public int ActorId { get; set; }

        [ProtoMember(2)]
        public string ObjectId { get; set; }

        [ProtoMember(3)]
        public ActorType ActorType { get; set; }
    }
}
