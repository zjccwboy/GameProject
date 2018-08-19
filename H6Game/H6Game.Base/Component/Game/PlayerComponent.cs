using H6Game.Entitys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    [Event(EventType.Awake | EventType.Start | EventType.Update)]
    public class PlayerComponent : BaseComponent
    {


        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
