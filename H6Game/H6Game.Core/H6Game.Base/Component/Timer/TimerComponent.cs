using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace H6Game.Base
{
    [ComponentEvent(EventType.Awake | EventType.Start | EventType.Update)]
    public class TimerComponent : BaseComponent
    {

        private Stopwatch Stopwatch { get; } = new Stopwatch();
        private Action Action { get; set; }
        private long MillisecondInterval { get; set; }
        private long LastTime { get; set; }
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

            if (Stopwatch.ElapsedMilliseconds < MillisecondInterval)
                return;

            if (Stopwatch.ElapsedMilliseconds - this.LastTime < MillisecondInterval)
                return;

            this.LastTime = Stopwatch.ElapsedMilliseconds;

            Action?.Invoke();
        }

        public override void Dispose()
        {
            Stopwatch.Stop();
            MillisecondInterval = 0;
            LastTime = 0;
            IsStart = false;
            base.Dispose();
        }

        public void SetTimer(Action action, long interval)
        {
            this.MillisecondInterval = interval;
            this.Action = action;
            this.IsStart = true;
        }

    }
}
