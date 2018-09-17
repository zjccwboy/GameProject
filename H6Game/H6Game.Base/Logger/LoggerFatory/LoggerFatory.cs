
namespace H6Game.Base
{
    public static class LoggerFatory
    {
        static LoggerFatory()
        {
            Game.Scene.AddComponent<LoggerConfigComponent>();
        }

        public static IMyLog Create()
        {
            return new MyLogger();
        }
    }
}
