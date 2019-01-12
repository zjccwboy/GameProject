using System;
using System.Collections.Concurrent;
using System.Threading;

namespace H6Game.Base.SyncContext
{
    /// <summary>
    /// 多线程回调上下文执行队列
    /// </summary>
    public class ThreadCallbackContext : SynchronizationContext
    {
        public static ThreadCallbackContext Instance = new ThreadCallbackContext();

        // 线程同步队列,发送接收socket回调都放到该队列,由poll线程统一执行
        private readonly ConcurrentQueue<Action> ActionQueue = new ConcurrentQueue<Action>();

        private void Add(Action action)
        {
            this.ActionQueue.Enqueue(action);
        }

        public void Update()
        {
            while (true)
            {
                if (!this.ActionQueue.TryDequeue(out Action action))
                    return;

                action();
            }
        }

        public override void Post(SendOrPostCallback callback, object state)
        {
            this.Add(() => { callback(state); });
        }
    }
}