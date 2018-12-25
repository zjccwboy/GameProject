

namespace H6Game.Base.Logger
{
    public class ErrorConsoleLogger : AConsoleLogger
    {
        public override LogLevel LogLevel => LogLevel.Error;
    }
}
