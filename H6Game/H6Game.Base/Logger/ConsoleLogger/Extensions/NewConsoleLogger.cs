using System;

namespace H6Game.Base
{
    public class NewConsoleLogger 
    {
        public LogLevel LogLevel { get; set; }

        public NewConsoleLogger(LogLevel logLevel)
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
        public static void ShowMessage(this NewConsoleLogger consoleLogger, string message)
        {
            Console.ForegroundColor = consoleLogger.Color;
            Console.WriteLine(message);
        }
    }
}
