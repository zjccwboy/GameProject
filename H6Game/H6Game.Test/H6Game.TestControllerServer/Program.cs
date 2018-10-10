using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;

namespace H6Game.TestControllerServer
{
    //控制器订阅消息Demo
    public class TestController : NetComponentSubscriber
    {
        [NetCommand(8001)]
        public void OnGet()
        {
            Log.Info($"OnGet cmd:{this.CurrentNetwrok.RecvPacket.NetCommand}", LoggerBllType.System);
        }

        [NetCommand(8002)]
        public int OnGetInt(int data)
        {
            Log.Info($"OnGetInt recv:{data} cmd:{this.CurrentNetwrok.RecvPacket.NetCommand}", LoggerBllType.System);
            return data;
        }

        [NetCommand(8003)]
        public Task OnGetTask()
        {
            Log.Info($"OnGetTask cmd:{this.CurrentNetwrok.RecvPacket.NetCommand}", LoggerBllType.System);
            return Task.CompletedTask;
        }

        [NetCommand(8004)]
        public Task<int> OnGetTaskInt(int data)
        {
            Log.Info($"OnGetTaskInt recv:{data} cmd:{this.CurrentNetwrok.RecvPacket.NetCommand}", LoggerBllType.System);
            return Task.FromResult(data);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            Game.Scene.AddComponent<TestController>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
