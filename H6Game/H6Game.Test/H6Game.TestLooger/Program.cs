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
                Log.Logger.Debug("Debug");
                Log.Logger.Info("Info");
                Log.Logger.Error("Error");
                Log.Logger.Warning("Warning");
                Log.Logger.Fatal("Fatal");
                Log.Logger.Notice("Notice");
            }
        }
    }
}
