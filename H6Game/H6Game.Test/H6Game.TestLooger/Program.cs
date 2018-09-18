using H6Game.Base;
using H6Game.Hotfix.Enums;
using System.Threading;

namespace H6Game.TestLooger
{
    class Program
    {
        static void Main(string[] args)
        {

            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<LoggerConfigComponent>();
            Game.Scene.AddComponent<DistributionsComponent>();
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Player);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Room);
            Game.Scene.AddComponent<ActorPoolComponent, ActorType>(ActorType.Game);

            TestWriteLog();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        static void TestWriteLog()
        {
            while (true)
            {
                Log.Logger.Debug("Debug", LoggerBllType.System);
                Log.Logger.Info("Info", LoggerBllType.System);
                Log.Logger.Error("Error", LoggerBllType.System);
                Log.Logger.Warning("Warning", LoggerBllType.System);
                Log.Logger.Fatal("Fatal", LoggerBllType.System);
                Log.Logger.Notice("Notice", LoggerBllType.System);
            }
        }
    }
}
