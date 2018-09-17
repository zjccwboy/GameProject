using System.Collections.Generic;

namespace H6Game.Base
{
    public class ConsoleLoggerFatory
    {

        private Dictionary<LogLevel, IConsoleLogger> FileWriters { get; } = new Dictionary<LogLevel, IConsoleLogger>();

        public void Create()
        {
            var levels = FileHelper.NameLevels.Values;
            foreach (var level in levels)
            {
                FileWriters[level] = Create(level);
            }
        }

        public void WriteMessage(LoggerEntity entity)
        {
            FileWriters[entity.FLogLevel].ShowMessage(entity);
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
