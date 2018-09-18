
namespace H6Game.Base
{
    public static class LoggerFactory
    {
        static LoggerFactory()
        {
            Game.Scene.AddComponent<LoggerConfigComponent>();
        }

        public static IMyLog Create()
        {
            return new MyLogger();
        }
    }
}
