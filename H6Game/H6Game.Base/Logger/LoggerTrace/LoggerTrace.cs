using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace H6Game.Base
{
    public sealed class LoggerTrace : ILoggerTrace
    {
        private IMyLog MyLogger { get; set; }

        public void Create()
        {
            this.MyLogger = LoggerFatory.Create();
        }

        public void Fatal(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Fatal, message, bllType, null, args);
        }

        public void Fatal(string message, Exception exception, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            Write(LogLevel.Fatal, message, bllType, exception, args);
        }

        public void Info(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Info, message, bllType, null, args);
        }

        public void Notice(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Notice, message, bllType, null, args);
        }

        public void Warning(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Warn, message, bllType, null, args);
        }

        public void Error(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Error, message, bllType, null, args);
        }

        public void Error(string message, Exception exception, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            Write(LogLevel.Error, message, bllType, exception, args);
        }

        public void Error(Exception exception, LoggerBllType bllType, params object[] args)
        {
            if (exception == null)
                return;

            Write(LogLevel.Error, null, bllType, exception, args);
        }

        public void Debug(string message, LoggerBllType bllType, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Debug, message, bllType, null, args);
        }

        private void Write(LogLevel FLogLevel, string message, LoggerBllType bllType, Exception exception, params object[] args)
        {
            string argsStr = null;
            if (args.Any())
                argsStr = args.ToJson();

            var stack = BuildStackTraceMessage();
            var msg = $"stack:{stack} message:{message}";

            MyLogger.WriteMessage(FLogLevel, msg, argsStr, bllType, exception);
        }

        private string BuildStackTraceMessage()
        {
            StackTrace trace = new StackTrace(true);
            return BuildStackTraceMessage(trace);
        }

        private string BuildStackTraceMessage(StackTrace stackTrace)
        {
            if (stackTrace != null)
            {
                var frameList = stackTrace.GetFrames();
                var realFrameList = frameList.Where(i => i.GetMethod().DeclaringType != this.GetType() && i.GetFileLineNumber() > 0);
                if (realFrameList.Any())
                {
                    StringBuilder builder = new StringBuilder();
                    realFrameList = realFrameList.Reverse();
                    var lastFrame = realFrameList.Last();
                    builder.AppendFormat($"{lastFrame.GetMethod().DeclaringType.FullName}\\{lastFrame.GetMethod().Name} Line:{lastFrame.GetFileLineNumber()}");
                    return builder.ToString();
                }
            }
            return "没有堆栈信息";
        }
    }
}
