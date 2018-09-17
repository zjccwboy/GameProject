﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace H6Game.Base
{
    public class MyLogger : IMyLog
    {
        public LoggerWriterFatory LoggerWriter { get; } = new LoggerWriterFatory();
        public ConsoleLoggerFatory ConsoleLogger { get; } = new ConsoleLoggerFatory();

        public MyLogger()
        {
            ConsoleLogger.Create();
        }

        public async Task WriteMessage(LogLevel FLogLevel, string message, EventId eventId, string args = null, Exception exception = null)
        {
            var entity = new LoggerEntity
            {
                FLogLevel = FLogLevel,
                FMessage = message,
                FEventId = eventId.Id,
                FArgs = args,
            };

            if(exception != null)
            {
                entity.FExceptionName = exception.GetType().Name;
                entity.FExceptionInfo = exception.StackTrace;
            }

            ConsoleLogger.WriteMessage(entity);

            await LoggerWriter.FileWriterFatory.WriteMessage(entity);
            await LoggerWriter.MongoWriterFatory.WriteMessage(entity);
        }
    }
}