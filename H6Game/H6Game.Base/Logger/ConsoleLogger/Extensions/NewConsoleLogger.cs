using System;
using Microsoft.Extensions.Logging;

namespace H6Game.Base
{
    public class NewConsoleLogger : Microsoft.Extensions.Logging.Console.ConsoleLogger
    {
        public LogLevel LogLevel { get; set; }
        public NewConsoleLogger(LogLevel LogLevel) : this(string.Empty, (a,b)=>true, true) { this.LogLevel = LogLevel; }

        public NewConsoleLogger(string name, Func<string, Microsoft.Extensions.Logging.LogLevel, bool> filter, bool includeScopes) : base(name, filter, includeScopes) { }

        public override void WriteMessage(Microsoft.Extensions.Logging.LogLevel logLevel, string logName, int eventId, string message, Exception exception)
        {
            this.Write(message, Color);
        }

        public ConsoleColor Color
        {
            get
            {
                switch (LogLevel)
                {
                    case LogLevel.Debug:
                        return ConsoleColor.White;
                    case LogLevel.Info:
                        return ConsoleColor.DarkGreen;
                    case LogLevel.Notice:
                        return ConsoleColor.DarkBlue;
                    case LogLevel.Warn:
                        return ConsoleColor.DarkYellow;
                    case LogLevel.Error:
                        return ConsoleColor.Red;
                    case LogLevel.Fatal:
                        return ConsoleColor.DarkRed;
                }
                return ConsoleColor.DarkRed;
            }
        }
    }

    public static class ShowLoggerExtensions
    {
        internal static void Write(this NewConsoleLogger consoleLogger,  string message, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(message);
        }

        public static void ShowMessage(this NewConsoleLogger consoleLogger, string message)
        {
            switch (consoleLogger.LogLevel)
            {
                case LogLevel.Debug:
                    Debug(consoleLogger, message);
                    break;
                case LogLevel.Info:
                case LogLevel.Notice:
                    Trace(consoleLogger, message);
                    break;
                case LogLevel.Warn:
                    Warn(consoleLogger, message);
                    break;
                case LogLevel.Error:
                    Error(consoleLogger, message);
                    break;
                case LogLevel.Fatal:
                    Fatal(consoleLogger, message);
                    break;
                default:
                    Fatal(consoleLogger, $"日志级别:{(int)consoleLogger.LogLevel}错误。");
                    break;
            }
        }

        private static void Debug(this NewConsoleLogger consoleLogger, string message)
        {
            consoleLogger.LogDebug(message);
        }
        
        private static void Trace(this NewConsoleLogger consoleLogger, string message)
        {
            consoleLogger.LogTrace(message);
        }

        private static void Warn(this NewConsoleLogger consoleLogger, string message)
        {
            consoleLogger.LogWarning(message);
        }

        private static void Error(this NewConsoleLogger consoleLogger, string message)
        {
            consoleLogger.LogError(message);
        }

        private static void Fatal(this NewConsoleLogger consoleLogger, string message)
        {
            consoleLogger.LogCritical(message);
        }
    }
}
