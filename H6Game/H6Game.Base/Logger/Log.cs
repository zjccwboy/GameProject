using MongoDB.Bson;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace H6Game.Base
{
    public static class Log
    {
        private static IMyLog MyLogger { get; set; }
        private static string CurrentPath { get; set; }

        static Log()
        {
            MyLogger = LoggerFactory.Create();
            CurrentPath = Path.GetDirectoryName(typeof(Log).Assembly.Location);
        }


        public static void Fatal(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Fatal, logMsg, bllType, null, args);
        }

        public static void Fatal(string message, Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Fatal, logMsg, bllType, exception, args);
        }

        public static void Info(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Info, logMsg, bllType, null, args);
        }

        public static void Notice(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Notice, logMsg, bllType, null, args);
        }

        public static void Warning(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Warn, logMsg, bllType, null, args);
        }

        public static void Error(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Error, logMsg, bllType, null, args);
        }

        public static void Error(string message, Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Error, logMsg, bllType, exception, args);
        }

        public static void Error(Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)}";
            Write(LogLevel.Error, logMsg, bllType, exception, args);
        }

        public static void Debug(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var logMsg = $"{GetFileInfo(file, line, member)} {message}";
            Write(LogLevel.Debug, logMsg, bllType, null, args);
        }

        private static string GetFileInfo(string file, int line, string member)
        {
            return $"{file}:{line} - {member}";
        }

        private static void Write(LogLevel FLogLevel, string message, LoggerBllType bllType, Exception exception, object args)
        {
            string argsStr = null;
            if (args != null)
                argsStr = args.ToJson();

            MyLogger.WriteMessage(FLogLevel, message, argsStr, bllType, exception);
        }
    }
}
