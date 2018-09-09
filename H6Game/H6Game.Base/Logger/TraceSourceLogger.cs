using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;

namespace H6Game.Base
{
    public sealed class TraceSourceLogger : ILogger
    {
        public void Fatal(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceStack(LogLevel.Fatal, messageToTrace);
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceException(LogLevel.Fatal, exception, messageToTrace);
        }

        public void Info(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceStack(LogLevel.Info, messageToTrace);
        }

        public void Warning(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            Trace(LogLevel.Warn, messageToTrace);
        }

        public void Error(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceStack(LogLevel.Error, messageToTrace);
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message) || exception == null)
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceException(LogLevel.Error, exception, messageToTrace);
        }

        public void Error(Exception exception, params object[] args)
        {
            if (exception == null)
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, "", args);
            TraceException(LogLevel.Error, exception, messageToTrace);
        }

        public void Debug(string message, params object[] args)
        {
            if (String.IsNullOrWhiteSpace(message))
                return;

            string messageToTrace = String.Format(CultureInfo.InvariantCulture, message, args);
            TraceStack(LogLevel.Debug, messageToTrace);
        }

        private void Trace(LogLevel logLevel, string message)
        {
            try
            {
                //_source.TraceEvent(eventType, (int)eventType, message);
                LogRecord.WriteLog(logLevel, message);
            }
            catch (SecurityException)
            {
                //这里处理写入是出现的安全问题，如文件没有写入权限。
            }
        }

        private void TraceStack(LogLevel logLevel, string message)
        {
            string stackMessage = BuildStackTraceMessage();
            string messageToTrace = $"{stackMessage} Info:{message}";
            Trace(logLevel, messageToTrace);
        }

        private void TraceException(LogLevel logLevel, Exception ex, string message)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("错误信息：{0}", message).AppendLine();
            builder.AppendFormat("异常信息：{0}", ex.Message).AppendLine();
            builder.AppendFormat("异常类型：{0}", ex.GetType().Name).AppendLine();

            string stackMessage = BuildStackTraceMessage();
            builder.Append(stackMessage);

            Trace(logLevel, builder.ToString());
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
                    builder.AppendFormat($"Stack:{lastFrame.GetMethod().DeclaringType.FullName}\\{lastFrame.GetMethod().Name} Line:{lastFrame.GetFileLineNumber()}");
                    return builder.ToString();
                }
            }
            return "没有堆栈信息";
        }
    }
}
