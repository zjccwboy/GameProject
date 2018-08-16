using System;
using System.Collections.Generic;
using System.Text;
using H6Game.Entitys;
using MongoDB.Driver;

namespace H6Game.Base
{
    public class Class1 : BaseRpository, IRpository
    {
        public Class1(IContext context) : base(context)
        {

        }
    }
}
