using MongoDB.Bson;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace H6Game.Base.Logger
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
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Fatal, stackInfo, message, bllType, null, args);
        }

        public static void Fatal(string message, Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Fatal, stackInfo, message, bllType, exception, args);
        }

        public static void Info(object message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
             [CallerLineNumber] int line = 0,
             [CallerMemberName] string member = null)
        {
            var stackInfo = $"{GetFileInfo(file, line, member)}";
            Write(LogLevel.Info, stackInfo, message.ToString(), bllType, null, args);
        }

        public static void Info(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = $"{GetFileInfo(file, line, member)}";
            Write(LogLevel.Info, stackInfo, message, bllType, null, args);
        }

        public static void Notice(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Notice, stackInfo, message, bllType, null, args);
        }

        public static void Warn(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Warn, stackInfo, message, bllType, null, args);
        }

        public static void Warn(Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
             [CallerLineNumber] int line = 0,
             [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Error, stackInfo, exception.StackTrace, bllType, exception, args);
        }

        public static void Error(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Error, stackInfo, message, bllType, null, args);
        }

        public static void Error(string message, Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Error, stackInfo, message, bllType, exception, args);
        }

        public static void Error(Exception exception, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Error, stackInfo, exception.StackTrace, bllType, exception, args);
        }

        public static void Debug(string message, LoggerBllType bllType, object args = null, [CallerFilePath] string file = null,
                     [CallerLineNumber] int line = 0,
                     [CallerMemberName] string member = null)
        {
            var stackInfo = GetFileInfo(file, line, member);
            Write(LogLevel.Debug, stackInfo, message, bllType, null, args);
        }

        private static string GetFileInfo(string file, int line, string member)
        {
            return $"{file}:{line} - {member}";
        }

        private static async void Write(LogLevel FLogLevel, string stackInfo, string message, LoggerBllType bllType, Exception exception, object args)
        {
            string argsStr = null;
            if (args != null)
                argsStr = args.ToJson();

           await  MyLogger.WriteMessage(FLogLevel, stackInfo, message, argsStr, bllType, exception);
        }
    }
}
