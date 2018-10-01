
namespace H6Game.Base
{
    public static class LoggerFactory
    {
        public static LoggerConfig Config { get; }

        static LoggerFactory()
        {
            Config = new LoggerConfigComponent().Config;
        }

        public static IMyLog Create()
        {
            return new MyLogger();
        }
    }
}
