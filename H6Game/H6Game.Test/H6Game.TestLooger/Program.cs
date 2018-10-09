using H6Game.Base;
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
            Log.Debug("Debug", LoggerBllType.System);
            Log.Info("Info", LoggerBllType.System);
            Log.Error("Error", LoggerBllType.System);
            Log.Warn("Warning", LoggerBllType.System);
            Log.Fatal("Fatal", LoggerBllType.System);
            Log.Notice("Notice", LoggerBllType.System);
        }
    }
}
