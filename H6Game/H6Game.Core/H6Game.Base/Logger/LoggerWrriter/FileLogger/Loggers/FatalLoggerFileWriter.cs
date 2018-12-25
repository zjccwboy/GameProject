
namespace H6Game.Base.Logger
{
    public class FatalLoggerFileWriter : ALoggerFileWriter
    {
        public override LogLevel LogLevel => LogLevel.Fatal;
    }
}
