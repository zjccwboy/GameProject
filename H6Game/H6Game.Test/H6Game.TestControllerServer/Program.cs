using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;

namespace H6Game.TestControllerServer
{
    public class TestController : H6Controller
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
        public Task<int> TestGetTaskInt()
        {
            return Task.FromResult(100);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<DistributionsComponent>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
