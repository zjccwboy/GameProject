
namespace H6Game.Base
{
    public class DebugLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Debug;

        public DebugLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }
    }
}
