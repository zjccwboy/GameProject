using System.Threading.Tasks;

namespace H6Game.Base
{
    public class LoggerMongoDBWriter : ILoggerWriter
    {
        private LoggerConfigEntity Config { get; set; }
        public LoggerMongoDBWriter()
        {
            this.Config = Game.Scene.GetComponent<LoggerConfigComponent>().Config;
        }

        public async Task WriteMessage(TLogger entity)
        {
            if (!CanWrite(entity.FLogLevel))
                return;

            await Game.Scene.GetComponent<LoggerRpository>().WriteLogger(entity);
        }

        public bool CanWrite(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return Config.Debug.IsWriteDB;
                case LogLevel.Info:
                    return Config.Info.IsWriteDB;
                case LogLevel.Notice:
                    return Config.Notice.IsWriteDB;
                case LogLevel.Warn:
                    return Config.Warn.IsWriteDB;
                case LogLevel.Error:
                    return Config.Error.IsWriteDB;
                case LogLevel.Fatal:
                    return Config.Fatal.IsWriteDB;
            }
            return true;
        }

        public void Dispose()
        {

        }

    }
}
