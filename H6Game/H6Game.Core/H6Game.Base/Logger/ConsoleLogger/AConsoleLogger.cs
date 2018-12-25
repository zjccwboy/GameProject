using H6Game.Base.Config;
using System;
using System.Text;

namespace H6Game.Base.Logger
{
    public abstract class AConsoleLogger : IConsoleLogger
    {
        private LoggerColor ConsoleLogger { get; }
        private StringBuilder MessageBuilder { get; } = new StringBuilder();
        private LoggerConfigEntity Config { get; }
        public abstract LogLevel LogLevel { get; }

        public AConsoleLogger()
        {
            this.Config = LoggerFactory.Config;
            this.ConsoleLogger = new LoggerColor(this.LogLevel);
        }

        public void ShowMessage(TLogger entity)
        {
#if SERVER
            if (entity.FLogLevel != this.LogLevel)
                return;
#endif

            if (!CanShow(this.LogLevel))
                return;

            MessageBuilder.Clear();
            var last = entity.FStackInfo.LastIndexOf('\\');
            string lastString = null;
            if(!string.IsNullOrEmpty(entity.FStackInfo) && last < entity.FStackInfo.Length)
                lastString = entity.FStackInfo.Substring(entity.FStackInfo.LastIndexOf('\\') + 1);
            MessageBuilder.Append($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ")}{Enum.GetName(typeof(LogLevel), this.LogLevel)} {lastString}");
            if (!string.IsNullOrEmpty(entity.FMessage))
            {
                MessageBuilder.Append($" {entity.FMessage}");
            }
            if (!string.IsNullOrEmpty(entity.FArgs))
            {
                MessageBuilder.Append($" args:{entity.FArgs}");
            }
            if (!string.IsNullOrEmpty(entity.FExceptionName))
            {
                MessageBuilder.AppendLine();
                MessageBuilder.AppendLine($"name:{entity.FExceptionName}");
                MessageBuilder.AppendLine($"message:{entity.FExceptionMessage}");
                MessageBuilder.AppendLine($"exception:{entity.FExceptionInfo.Trim()}");
            }
            ConsoleLogger.ShowMessage(MessageBuilder.ToString());
        }

        public bool CanShow(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return Config.Debug.IsShowConsole;
                case LogLevel.Info:
                    return Config.Info.IsShowConsole;
                case LogLevel.Notice:
                    return Config.Notice.IsShowConsole;
                case LogLevel.Warn:
                    return Config.Warn.IsShowConsole;
                case LogLevel.Error:
                    return Config.Error.IsShowConsole;
                case LogLevel.Fatal:
                    return Config.Fatal.IsShowConsole;
            }
            return true;
        }
    }
}