
namespace H6Game.Base
{
    public interface ILoggerFileWriter : ILoggerWriter
    {
        void CreateNewFile();
        bool IsWorking { get;}
        bool IsCreateNew { get; set;}
        bool CanWrite();
    }
}
