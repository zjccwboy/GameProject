using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class LoggerWriterFatory
    {
        public LoggerFileWriterFatory FileWriterFatory { get; } = new LoggerFileWriterFatory();
        public LoggerMongoDBWriter MongoWriterFatory { get; } = new LoggerMongoDBWriter();

        public void Create()
        {
            FileWriterFatory.Create();
        }
    }
}
