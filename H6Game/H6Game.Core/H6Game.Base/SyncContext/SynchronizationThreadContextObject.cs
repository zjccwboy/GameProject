using System.Threading;

namespace H6Game.Base.SyncContext
{
    public abstract class SynchronizationThreadContextObject
    {
        public SynchronizationContext SyncContext => SynchronizationThreadContext.Instance;
    }
}
