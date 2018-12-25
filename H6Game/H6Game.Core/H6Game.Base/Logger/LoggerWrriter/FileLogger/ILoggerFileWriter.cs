
namespace H6Game.Base.Logger
{
    public interface ILoggerFileWriter : ILoggerWriter
    {
        void CreateNewFile();
        bool IsWorking { get;}
        bool IsCreateNew { get; set;}
        bool CanWrite();
    }
}
