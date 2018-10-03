

namespace H6Game.Base
{
    public class FatalConsoleLogger : AConsoleLogger
    {
        public override LogLevel LogLevel => LogLevel.Fatal;
    }
}
