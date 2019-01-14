using H6Game.Base.Config;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public class LoggerFileWriterFactory
    {
        private Dictionary<LogLevel, ILoggerFileWriter> FileWriters { get; }
        private LoggerConfigEntity Config { get; }

        public LoggerFileWriterFactory()
        {
            FileWriters = new Dictionary<LogLevel, ILoggerFileWriter>();
            FileInfoManager.Load();
            Config = LoggerFactory.Config;
            FileInfoManager.UpdateLastCreateFileInfo(Config.Path);
        }

        public void Create()
        {
            var levels = FileInfoManager.NameLevels.Values;
            foreach (var level in levels)
            {
                FileWriters[level] = Create(level);
            }
        }

        public async Task WriteMessage(TLogger entity)
        {
            var writer = FileWriters[entity.FLogLevel];

            await writer.WriteMessage(entity);
        }

        private ILoggerFileWriter Create(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return new DebugLoggerFileWriter();
                case LogLevel.Info:
                    return new InfoLoggerFileWriter();
                case LogLevel.Notice:
                    return new NoticeLoggerFileWriter();
                case LogLevel.Warn:
                    return new WarnLoggerFileWriter();
                case LogLevel.Error:
                    return new ErrorLoggerFileWriter();
                case LogLevel.Fatal:
                    return new FatalLoggerFileWriter();
            }
            return default;
        }
    }
}
