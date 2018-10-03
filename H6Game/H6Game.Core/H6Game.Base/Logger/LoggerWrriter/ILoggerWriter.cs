using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface ILoggerWriter
    {
        bool CanWrite(LogLevel logLevel);
        Task WriteMessage(TLogger entity);
    }
}
