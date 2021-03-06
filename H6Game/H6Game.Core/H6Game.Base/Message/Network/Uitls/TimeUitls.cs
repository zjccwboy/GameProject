﻿using System;
using System.Diagnostics;

namespace H6Game.Base.Message
{
    public static class TimeUitls
    {
        //private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

        private static Stopwatch StopWatch = new Stopwatch();
        static TimeUitls()
        {
            StopWatch.Start();
        }

        public static long ClientNow()
        {
            //return (DateTime.UtcNow.Ticks - epoch) / 10000;
            return StopWatch.ElapsedMilliseconds + ANetChannel.ReConnectInterval;
        }

        public static uint Now()
        {
            return (uint)ClientNow();
        }
    }
}
