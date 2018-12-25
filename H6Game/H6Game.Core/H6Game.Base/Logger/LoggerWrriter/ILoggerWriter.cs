using System.Threading.Tasks;

namespace H6Game.Base.Logger
{
    public interface ILoggerWriter
    {
        bool CanWrite(LogLevel logLevel);
        Task WriteMessage(TLogger entity);
    }
}
