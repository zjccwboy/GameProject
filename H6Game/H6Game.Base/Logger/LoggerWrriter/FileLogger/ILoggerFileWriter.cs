using System.Threading.Tasks;

namespace H6Game.Base
{
    public interface ILoggerFileWriter : ILoggerWriter
    {
        void CreateFile();
        bool IsWorking { get;}
        bool IsCreateNew { get; set;}
        bool Existed { get;}
        bool CanWrite();
    }
}
