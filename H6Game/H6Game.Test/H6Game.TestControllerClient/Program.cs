using H6Game.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TestControllerClient
{
    class Program
    {
        //这个测试Demo主要演示如何使用框架向服务端NetController控制器订阅者发送消息。
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnected = (c,t) => { Start(); };
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
          
        static async void Start()
        {
            Get();
            await GetInt();

            GetTask();
            await GetTaskInt();
        }

        private static void  Get()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            network.Send(8001);
            Log.Info($"GetTaskInt send:{1024} cmd:{8001}", LoggerBllType.System);
        }

        private static async Task GetInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var result = await network.CallMessageAsync<int, int>(1024, 8002);
            Log.Info($"GetTaskInt send:{1024} recv:{result} cmd:{8002}", LoggerBllType.System);
        }

        private static void GetTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            network.Send(8003);
            Log.Info($"GetTaskInt send:{1024} cmd:{8003}", LoggerBllType.System);
        }

        private static async Task GetTaskInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var result = await network.CallMessageAsync<int, int>(1024, 8004);
            Log.Info($"GetTaskInt send:{1024} recv:{result} cmd:{8004}", LoggerBllType.System);
        }
    }
}
