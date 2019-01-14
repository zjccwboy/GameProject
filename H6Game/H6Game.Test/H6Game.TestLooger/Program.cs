using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Config;
using H6Game.Base.Logger;
using System.Threading;

namespace H6Game.TestLooger
{
    class Program
    {
        static void Main(string[] args)
        {

            Game.Scene.AddComponent<MongoConfig>();
            TestWriteLog();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        static void TestWriteLog()
        {
            Game.Scene.AddComponent<TimerComponent>().SetTimer(() =>
            {
                for (var i = 0; i < 200; i++)
                {
                    Log.Debug("Debug", LoggerBllType.System);
                    Log.Info("Info", LoggerBllType.System);
                    //Log.Error("Error", LoggerBllType.System);
                    //Log.Warn("Warning", LoggerBllType.System);
                    //Log.Fatal("Fatal", LoggerBllType.System);
                    //Log.Notice("Notice", LoggerBllType.System);
                }
            }, 2000);
        }
    }
}
