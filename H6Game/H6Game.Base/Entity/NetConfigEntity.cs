using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base.Entity
{
    public class NetConfigEntity
    {
        public EndPointEntity CenterEndPoint { get; set; }
        public int MinPort { get; set; }
        public int MaxPort { get; set; }
        public List<string> IPList { get; set; } = new List<string>();
    }
}