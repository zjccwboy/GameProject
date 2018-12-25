

namespace H6Game.Base.Logger
{
    public interface IConsoleLogger
    {
        LogLevel LogLevel { get; }
        void ShowMessage(TLogger entity);
    }
}
