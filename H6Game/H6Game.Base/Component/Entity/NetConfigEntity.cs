using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base.Entity
{
    public class NetConfig
    {
        public bool IsCenterServer { get; set; }
        public InNetConfigEntity InNetConfig { get; set; }
        public OutNetConfigEntity OuNetConfig { get; set; }
    }

    public class InNetConfigEntity
    {
        public EndPointEntity CenterEndPoint { get; set; }
        public EndPointEntity LocalEndPoint { get; set; }
        public int OutNetListenPort { get; set; }
    }

    public class OutNetConfigEntity
    {
        public string Host { get; set; }
    }
}