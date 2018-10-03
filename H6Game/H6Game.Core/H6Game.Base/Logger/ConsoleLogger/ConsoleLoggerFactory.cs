using System.Collections.Generic;

namespace H6Game.Base
{
    public class ConsoleLoggerFactory
    {

        private Dictionary<LogLevel, IConsoleLogger> ConsoleWriters { get; } = new Dictionary<LogLevel, IConsoleLogger>();

        public void Create()
        {
            var levels = FileInfoManager.NameLevels.Values;
            foreach (var level in levels)
            {
                ConsoleWriters[level] = Create(level);
            }
        }

        public void WriteMessage(TLogger entity)
        {
            ConsoleWriters[entity.FLogLevel].ShowMessage(entity);
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
