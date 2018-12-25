

namespace H6Game.Base.Logger
{
    public class FatalConsoleLogger : AConsoleLogger
    {
        public override LogLevel LogLevel => LogLevel.Fatal;
    }
}
