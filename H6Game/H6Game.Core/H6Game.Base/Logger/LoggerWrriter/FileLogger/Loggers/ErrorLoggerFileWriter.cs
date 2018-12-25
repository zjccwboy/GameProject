
namespace H6Game.Base.Logger
{
    public class ErrorLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Error;
    }
}
