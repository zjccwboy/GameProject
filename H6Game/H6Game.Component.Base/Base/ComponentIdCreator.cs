using System.Threading;

namespace H6Game.Component.Base
{
    public class ComponentIdCreator
    {
        private static long id = 0;
        public static uint CreateId()
        {
            Interlocked.Increment(ref id);
            Interlocked.CompareExchange(ref id, 0, uint.MaxValue);
            return (uint)id;
        }
    }
}
