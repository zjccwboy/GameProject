﻿using System;
using System.Text;

namespace H6Game.Base
{
    public abstract class AConsoleLogger : IConsoleLogger
    {
        private LoggerColor ConsoleLogger { get; }
        private StringBuilder MessageBuilder { get; } = new StringBuilder();
        private LoggerConfigEntity Config { get; }
        public abstract LogLevel LogLevel { get; }

        public AConsoleLogger()
        {
            this.Config = Game.Scene.GetComponent<LoggerConfigComponent>().Config;
            this.ConsoleLogger = new LoggerColor(this.LogLevel);
        }

        public void ShowMessage(TLogger entity)
        {
            if (entity.FLogLevel != this.LogLevel)
                return;

            if (!CanShow(this.LogLevel))
                return;

            MessageBuilder.Clear();
            MessageBuilder.Append($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ")}{Enum.GetName(typeof(LogLevel), this.LogLevel)}");
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
                MessageBuilder.AppendLine($"exception name:{entity.FExceptionName}");
                MessageBuilder.AppendLine($"exception:{ entity.FExceptionInfo}");
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