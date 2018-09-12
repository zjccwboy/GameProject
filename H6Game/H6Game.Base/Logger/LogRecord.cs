
#if SERVER
using log4net;
using log4net.Config;
using log4net.Core;
#endif

using System;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

#if WINDOWS
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Bson;
using System.Text;
#endif

#if SERVER
[assembly: XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
#endif

namespace H6Game.Base
{
#if WINDOWS
    public class LogEntity
    {
        public Type LogType { get; set; }
        public Level LogLevel { get; set; }
        public string Logs { get; set; }
    }

    public sealed class WriteQueue : IDisposable
    {
        private readonly ConcurrentQueue<Action> actionsQueue = new ConcurrentQueue<Action>();
        private bool dispose;
        
        private WriteQueue()
        {
            DoWrite();
        }

        public static WriteQueue Instance { get; } = new WriteQueue();

        public void Post(SendOrPostCallback callback, object state)
        {
            actionsQueue.Enqueue(() => { callback(state); });
        }

        private void DoWrite()
        {
            Task.Run(() =>
            {
                while (!dispose)
                {
                    while(actionsQueue.TryDequeue(out Action action))
                    {
                        action();
                    }
                    Thread.Sleep(1);
                }
            });
        }

        public void Dispose()
        {
            dispose = true;
        }
    }
#endif

    public static class LogRecord
    {
#if SERVER
        private static log4net.Core.ILogger logger;
        private static Type logType;
        static LogRecord()
        {
            GlobalContext.Properties["InstanceName"] = Process.GetCurrentProcess().Id;
            var assembly = Assembly.GetCallingAssembly();
            var loggers = LoggerManager.GetCurrentLoggers(assembly);
            var loggerSystem = loggers.SingleOrDefault(f => f.Name.Equals("LoggerNameSystem", StringComparison.OrdinalIgnoreCase));
            if (loggerSystem == null)
            {
                loggerSystem = loggers.FirstOrDefault(t => t.Name.Equals("LoggerNameRecordData", StringComparison.InvariantCultureIgnoreCase) == false);
            }
            logger = loggerSystem;
            logType = typeof(LogRecord);
        }

        private static Level GetLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Notice:
                    return Level.Notice;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
            }
            return Level.Log4Net_Debug;
        }
#endif

        public static void WriteLog(LogLevel level, string message)
        {
            var logLevel = GetLogLevel(level);
#if SERVER
#if WINDOWS
            WriteQueue.Instance.Post(WriteHandler, new LogEntity
            {
                LogType = logType,
                LogLevel = logLevel,
                Logs = message
            });
#else
            logger.Log(logType, logLevel, message, null);
#endif
#endif
        }

#if WINDOWS
        private static void WriteHandler(object obj)
        {
            var logEntity = obj as LogEntity;
            logger.Log(logEntity.LogType, logEntity.LogLevel, logEntity.Logs, null);
        }
#endif

    }
}