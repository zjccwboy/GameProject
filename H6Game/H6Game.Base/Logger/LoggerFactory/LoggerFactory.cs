﻿
namespace H6Game.Base
{
    public static class LoggerFactory
    {
        public static LoggerConfigEntity Config { get; }

        static LoggerFactory()
        {
            Config = new LoggerConfig().Config;
        }

        public static IMyLog Create()
        {
            return new MyLogger();
        }
    }
}
