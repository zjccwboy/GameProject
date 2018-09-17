using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class FatalLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Fatal;

        public FatalLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }
    }
}
