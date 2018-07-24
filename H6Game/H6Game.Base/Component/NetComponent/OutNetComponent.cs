using H6Game.Base.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace H6Game.Base
{
    public class OutNetComponent : BaseComponent
    {
        private NetConfig config { get; set; }
        private Session connectSession;
        public OutNetComponent()
        {
            this.config = SinglePool.Get<ConfigNetComponent>().ConfigEntity;
        }

        private void HandleConnect()
        {

        }
    }
}
