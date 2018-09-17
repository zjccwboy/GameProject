

namespace H6Game.Base
{
    public interface ILoggerFileWriter : ILoggerWriter
    {
        void CreateOrOpenFile();
    }
}
