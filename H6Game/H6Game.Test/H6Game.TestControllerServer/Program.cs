using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;

namespace H6Game.TestControllerServer
{
    public class TestController : NetController
    {
        [NetCommand(8001)]
        public int TestGetInt()
        {
            return 100;
        }

        [NetCommand(8002)]
        public int TestGetInt(int data)
        {
            return data;
        }

        [NetCommand(8003)]
        public Task TestGetTask()
        {
            return Task.CompletedTask;
        }

        [NetCommand(8004)]
        public Task<int> TestGetTaskInt(int data)
        {
            return Task.FromResult(data);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Log.Fatal("未处理异常。", e.ExceptionObject as Exception, LoggerBllType.System);
            };

            try
            {
                Game.Scene.AddComponent<MongoConfig>();
                Game.Scene.AddComponent<DistributionsComponent>();
                Game.Scene.AddComponent<TestController>();

                while (true)
                {
                    Game.Update();
                    Thread.Sleep(1);
                }
            }
            catch(Exception e)
            {
                Log.Fatal("未处理异常。", e, LoggerBllType.System);
                Console.ReadKey();
            }
        }
    }
}
