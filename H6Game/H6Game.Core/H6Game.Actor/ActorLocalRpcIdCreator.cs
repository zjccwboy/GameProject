using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Actor
{
    internal class ActorLocalRpcIdCreator
    {
        private static int Id;
        public static int RpcId
        {
            get
            {
                if (Id == int.MaxValue)
                    Id = 0;
                return ++Id;
            }
        }
    }
}
