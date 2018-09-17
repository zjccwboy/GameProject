

namespace H6Game.Base
{
    public class WarnLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Warn;

        public WarnLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }
    }
}
