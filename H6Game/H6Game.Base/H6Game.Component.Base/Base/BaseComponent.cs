using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Component.Base
{
    public abstract class BaseComponent
    {
        public uint ComponentId { get; set; }

        public void Close()
        {
            this.PutBack();
        }
    }
}
