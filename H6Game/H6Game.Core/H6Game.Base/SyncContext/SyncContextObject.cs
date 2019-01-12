using System.Threading;

namespace H6Game.Base.SyncContext
{
    public abstract class SyncContextObject
    {
        public SynchronizationContext SyncContext { get; } = ThreadCallbackContext.Instance;
    }
}
