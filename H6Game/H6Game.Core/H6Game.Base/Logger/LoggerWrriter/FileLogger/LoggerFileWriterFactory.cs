using H6Game.Base.Config;
using H6Game.Base.SyncContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public class LoggerFileWriterFactory : SynchronizationThreadContextObject
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
                var writer = Create(level);
                FileWriters[level] = writer;
                writer.CreateFileCallBack = async customName =>
                {
                    await this.SyncContext;
                    foreach(var wt in FileWriters.Values)
                    {
                        var levelName = FileInfoManager.LevelNames[wt.LogLevel];
                        var fileName = $"{customName}_{levelName}.log";
                        if (FileInfoManager.LogFiles.Add(fileName))
                        {
                            try
                            {
                                await wt.CreateFile(customName, levelName, fileName);
                            }
                            catch(Exception e)
                            {
                                Console.Write(e.ToString());
                            }
                        }
                    }
                };
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
