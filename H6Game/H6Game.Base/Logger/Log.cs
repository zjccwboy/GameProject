

namespace H6Game.Base
{
    public class Log
    {
        public static ILoggerTrace Logger { get; }

        static Log()
        {
            LoggerContext.SetCurrent(new LoggerTraceFactory());
            Logger = LoggerContext.CreateLog();
        }
    }
}
