
namespace H6Game.Base
{
    public class FatalLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Fatal;
    }
}
