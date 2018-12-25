using System;

namespace H6Game.Base.Logger
{
    public class LoggerColor 
    {
        public LogLevel LogLevel { get; set; }

        public LoggerColor(LogLevel logLevel)
        {
            this.LogLevel = logLevel;
        }

        public ConsoleColor Color
        {
            get
            {
                switch (LogLevel)
                {
                    case LogLevel.Debug:
                        return ConsoleColor.DarkBlue;
                    case LogLevel.Info:
                        return ConsoleColor.DarkGreen;
                    case LogLevel.Notice:
                        return ConsoleColor.White;
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
        public static void ShowMessage(this LoggerColor consoleLogger, string message)
        {
            Console.ForegroundColor = consoleLogger.Color;
            Console.WriteLine(message);
        }
    }
}
