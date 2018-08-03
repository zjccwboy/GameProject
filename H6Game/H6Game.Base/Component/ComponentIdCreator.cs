using System.Threading;

namespace H6Game.Base
{
    public class ComponentIdCreator
    {
        private static int id = 0;
        public static int CreateId()
        {
            Interlocked.Increment(ref id);
            Interlocked.CompareExchange(ref id, 0, int.MaxValue);
            return id;
        }
    }
}
