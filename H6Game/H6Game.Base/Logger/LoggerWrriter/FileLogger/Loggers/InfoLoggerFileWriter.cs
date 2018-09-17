using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class InfoLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Info;

        public InfoLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }

    }
}
