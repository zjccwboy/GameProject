using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class LoggerFileWriterFactory
    {
        private Dictionary<LogLevel, ILoggerFileWriter> FileWriters { get; }
        private LoggerConfigEntity Config { get; }

        public LoggerFileWriterFactory()
        {
            FileWriters = new Dictionary<LogLevel, ILoggerFileWriter>();
            FileInfoManager.Load();
            Config = Game.Scene.GetComponent<LoggerConfigComponent>().Config;
            FileInfoManager.UpdateLastCreateFileInfo(Config.Path);
        }

        public void Create()
        {
            var levels = FileInfoManager.NameLevels.Values;
            foreach (var level in levels)
            {
                FileWriters[level] = Create(level);
            }
            CreateNewFiles();
        }

        public async Task WriteMessage(TLogger entity)
        {
            var writer = FileWriters[entity.FLogLevel];

            if (!writer.CanWrite())
                CreateNewFiles();

            await writer.WriteMessage(entity);
        }

        private void CreateNewFiles()
        {
            foreach (var writer in FileWriters.Values)
            {
                if (!writer.IsWorking)
                {
                    writer.CreateNewFile();
                }
                else
                {
                    writer.IsCreateNew = true;
                }
            }
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
