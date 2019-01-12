using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace H6Game.Base.SyncContext
{
    public class SynchronizationContextAwaiter : INotifyCompletion
    {
        private SynchronizationContext Context { get; } = SynchronizationThreadContext.Instance;
        private SendOrPostCallback Executor { get; } = a => (a as Action)();

        public bool IsCompleted
        {
            get { return false; }
        }

        public void OnCompleted(Action action)
        {
            this.Context.Post(this.Executor, action);
            this.SetAwaiter();
        }

        public void GetResult()
        {

        }
    }
}
