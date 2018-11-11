using System;
using System.Diagnostics;

namespace H6Game.Base
{
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    public class DelayComponent : BaseComponent
    {
        private Stopwatch Stopwatch { get; } = new Stopwatch();
        private Action Action { get; set; }
        private long MillisecondDelay { get; set; }
        private bool IsStart { get; set; }

        public override void Awake()
        {
            Stopwatch.Start();
        }

        public override void Start()
        {
            Stopwatch.Restart();
        }

        public override void Update()
        {
            if (!IsStart)
                return;

            if (Stopwatch.ElapsedMilliseconds < MillisecondDelay)
                return;

            Action?.Invoke();

            this.Dispose();
        }

        public override void Dispose()
        {
            IsStart = false;
            Stopwatch.Stop();
            MillisecondDelay = 0;
            Action = null;
            base.Dispose();
        }

        public void SetDelay(Action action, long millisecondsDelay)
        {
            this.MillisecondDelay = millisecondsDelay;
            this.Action = action;
            this.IsStart = true;
        }
    }
}
