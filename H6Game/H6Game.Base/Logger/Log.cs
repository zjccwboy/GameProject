

namespace H6Game.Base
{
    public class Log
    {
        public static ILogger Logger { get; }

        static Log()
        {
            LoggerContext.SetCurrent(new TraceSourceLoggerFactory());
            Logger = LoggerContext.CreateLog();
        }
    }
}
