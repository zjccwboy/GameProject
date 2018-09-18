using MongoDB.Bson;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace H6Game.Base
{
    public sealed class TraceSourceLogger : ILogger
    {
        private IMyLog MyLogger { get; set; }

        public void Create()
        {
            this.MyLogger = LoggerFatory.Create();
        }

        public void Fatal(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Fatal, message, null, args);
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            Write(LogLevel.Fatal, message, exception, args);
        }

        public void Info(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Info, message, null, args);
        }

        public void Notice(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Notice, message, null, args);
        }

        public void Warning(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Warn, message, null, args);
        }

        public void Error(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Error, message, null, args);
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            Write(LogLevel.Error, message, exception, args);
        }

        public void Error(Exception exception, params object[] args)
        {
            if (exception == null)
                return;

            Write(LogLevel.Error, null, exception, args);
        }

        public void Debug(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            Write(LogLevel.Debug, message, null, args);
        }

        private void Write(LogLevel FLogLevel, string message, Exception exception = null, params object[] args)
        {
            string argsStr = null;
            if (args.Any())
                argsStr = args.ToJson();

            var stack = BuildStackTraceMessage();
            var msg = $"stack:{stack} message:{message}";

            MyLogger.WriteMessage(FLogLevel, msg, argsStr, exception);
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
