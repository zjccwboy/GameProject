

namespace H6Game.Base
{
    public class NoticeLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Notice;

        public NoticeLoggerFileWriter(LoggerFileWriterFatory writerFatory) : base(writerFatory) { }
    }
}
