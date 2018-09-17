﻿
namespace H6Game.Base
{
    public class ErrorLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Error;

        public ErrorLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }
    }
}