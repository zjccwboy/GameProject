﻿using System;
using System.Collections.Generic;
using System.Text;
using NetChannel;

namespace H6Game.Component.Base.Entity
{
    public class NetConfigEntity
    {
        public EndPointEntity RemoteEndPoint { get; set; }
        public EndPointEntity LocalEndPoint { get; set; }
    }
}