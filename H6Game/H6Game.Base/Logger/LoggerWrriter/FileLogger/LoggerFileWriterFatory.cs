using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base
{
    public class LoggerFileWriterFatory
    {
        private Dictionary<LogLevel, ILoggerFileWriter> FileWriters { get; }

        public LoggerFileWriterFatory()
        {
            FileWriters = new Dictionary<LogLevel, ILoggerFileWriter>();
        }

        public void ReCreateWriters()
        {
            foreach(var kv in FileWriters)
            {
                kv.Value.Dispose();
            }
            FileWriters.Clear();
            Create();
        }

        public void Create()
        {
            var levels = FileHelper.NameLevels.Values;
            foreach (var level in levels)
            {
                FileWriters[level] = Create(level);
            }

            var patch = Game.Scene.GetComponent<LoggerConfigComponent>().Config.LoggerPath;
            FileHelper.UpdateFileInfo(patch);
            var needReload = FileHelper.LastCreateFileNames.Count <= 0;
            foreach (var writer in FileWriters.Values)
            {
                writer.CreateOrOpenFile();
            }

            if (needReload)
            {
                FileHelper.UpdateFileInfo(patch);
            }
        }

        public async Task WriteMessage(LoggerEntity entity)
        {
            await FileWriters[entity.FLogLevel].WriteMessage(entity);
        }

        private ILoggerFileWriter Create(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return new DebugLoggerFileWriter(this);
                case LogLevel.Info:
                    return new InfoLoggerFileWriter(this);
                case LogLevel.Notice:
                    return new NoticeLoggerFileWriter(this);
                case LogLevel.Warn:
                    return new WarnLoggerFileWriter(this);
                case LogLevel.Error:
                    return new ErrorLoggerFileWriter(this);
                case LogLevel.Fatal:
                    return new FatalLoggerFileWriter(this);
            }
            return default;
        }
    }
}
