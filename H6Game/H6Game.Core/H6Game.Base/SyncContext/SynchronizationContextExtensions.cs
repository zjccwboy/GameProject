using System.Collections.Concurrent;
using System.Threading;

namespace H6Game.Base.SyncContext
{
    public static class SynchronizationContextExtensions
    {
        private static ConcurrentQueue<SynchronizationContextAwaiter> QueueAwaiters { get; } = new ConcurrentQueue<SynchronizationContextAwaiter>();
        public static SynchronizationContextAwaiter GetAwaiter(this SynchronizationContext context)
        {
            if (!QueueAwaiters.TryDequeue(out SynchronizationContextAwaiter awaiter))
            {
                awaiter = new SynchronizationContextAwaiter();
            }
            return awaiter;
        }

        public static void SetAwaiter(this SynchronizationContextAwaiter context)
        {
            QueueAwaiters.Enqueue(context);
        }
    }
}
