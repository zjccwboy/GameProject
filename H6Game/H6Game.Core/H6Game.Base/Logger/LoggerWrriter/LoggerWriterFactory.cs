
namespace H6Game.Base.Logger
{
    public class LoggerWriterFactory
    {
        public LoggerFileWriterFactory FileWriterFatory { get; } = new LoggerFileWriterFactory();
        public LoggerMongoDBWriter MongoWriterFatory { get; } = new LoggerMongoDBWriter();

        public void Create()
        {
            FileWriterFatory.Create();
        }
    }
}
