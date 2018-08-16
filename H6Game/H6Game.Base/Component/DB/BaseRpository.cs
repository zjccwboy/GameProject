using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public abstract class BaseRpository
    {
        public IContext DBContext { get; }

        public BaseRpository(IContext context)
        {
            this.DBContext = context;
        }
    }
}
