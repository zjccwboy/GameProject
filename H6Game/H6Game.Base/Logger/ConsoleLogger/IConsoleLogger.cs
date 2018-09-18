

namespace H6Game.Base
{
    public interface IConsoleLogger
    {
        LogLevel LogLevel { get; }
        void ShowMessage(TLogger entity);
    }
}
