using System;
using System.Collections.Generic;
using System.Text;
using NetChannel;

namespace H6Game.Component.Base.Entity
{
    public class NetConfigEntity
    {
        public EndPointEntity GameServer { get; set; }
        public EndPointEntity AccountServer { get; set; }
        public EndPointEntity GateServer { get; set; }
        public EndPointEntity PayServer { get; set; }
        public EndPointEntity ResourceServer { get; set; }
        public EndPointEntity LoginServer { get; set; }
        public EndPointEntity DespacherServer { get; set; }
    }
}