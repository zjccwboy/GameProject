using System;
using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public class MyLogger : IMyLog
    {
#if SERVER
        public LoggerWriterFactory LoggerWriter { get; } = new LoggerWriterFactory();
#endif
        public ConsoleLoggerFactory ConsoleLogger { get; } = new ConsoleLoggerFactory();

        public MyLogger()
        {
            ConsoleLogger.Create();
#if SERVER
            LoggerWriter.Create();
#endif
        }

        public async Task WriteMessage(LogLevel FLogLevel, string stackInfo, string message, string args, LoggerBllType bllType, Exception exception)
        {
            var entity = new TLogger
            {
                FBllType = bllType,
                FLogLevel = FLogLevel,
                FStackInfo = stackInfo,
                FMessage = message,
                FArgs = args,
            };

            if(exception != null)
            {
                entity.FExceptionName = exception.GetType().Name;
                entity.FExceptionMessage = exception.Message;
                entity.FExceptionInfo = exception.StackTrace;
            }

            entity.SetCreator("Sys");
            ConsoleLogger.WriteMessage(entity);

#if SERVER
            await LoggerWriter.FileWriterFatory.WriteMessage(entity);
            await LoggerWriter.MongoWriterFatory.WriteMessage(entity);
#endif
            await Task.CompletedTask;
        }
    }
}
