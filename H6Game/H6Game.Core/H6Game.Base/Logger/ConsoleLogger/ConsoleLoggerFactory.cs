using System.Collections.Generic;

namespace H6Game.Base.Logger
{
    public class ConsoleLoggerFactory
    {

        private Dictionary<LogLevel, IConsoleLogger> ConsoleWriters { get; } = new Dictionary<LogLevel, IConsoleLogger>();

        public void Create()
        {
#if SERVER
            var levels = FileInfoManager.NameLevels.Values;
            foreach (var level in levels)
            {
                ConsoleWriters[level] = Create(level);
            }
#else
            ConsoleWriters[LogLevel.Debug] = Create(LogLevel.Debug);
#endif
        }

        public void WriteMessage(TLogger entity)
        {
#if SERVER
            ConsoleWriters[entity.FLogLevel].ShowMessage(entity);
#else
            ConsoleWriters[LogLevel.Debug].ShowMessage(entity);
#endif
        }

        public static IConsoleLogger Create(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return new DebugConsoleLogger();
                case LogLevel.Info:
                    return new InfoConsoleLogger();
                case LogLevel.Notice:
                    return new NoticeConsoleLogger();
                case LogLevel.Warn:
                    return new WarnConsoleLogger();
                case LogLevel.Error:
                    return new ErrorConsoleLogger();
                case LogLevel.Fatal:
                    return new FatalConsoleLogger();
            }

            return default;
        }
    }
}
