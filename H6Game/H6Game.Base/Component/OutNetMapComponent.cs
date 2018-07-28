using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class OutNetMapComponent : BaseComponent
    {
        private readonly ConcurrentDictionary<int, NetEndPointMessage> netMapingDictionary = new ConcurrentDictionary<int, NetEndPointMessage>();

        public override void Start()
        {
            
        }
    }
}
