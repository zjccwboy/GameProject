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
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<OuterComponent>().OnConnected = c => { Start(); };
            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        static int Count;
        static Stopwatch Swatch = new Stopwatch();

        static void Start()
        {
            Swatch.Start();

            for (var i = 0; i < 2000; i++)
                Test();
        }

        static async void Test()
        {
            while (true)
                await Call();
        }

        private static async Task Call()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var result = await network.CallMessageAsync<int>(8001);
            if (!result.Result)
            {
                Log.Error($"RPC请求失败。", LoggerBllType.System);
            }

            if (result.Content != 100)
            {
                Log.Error($"解包出错。", LoggerBllType.System);
            }

            result = await network.CallMessageAsync<int>(1000, 8002);
            if (!result.Result)
            {
                Log.Error($"RPC请求失败。", LoggerBllType.System);
            }

            if (result.Content != 1000)
            {
                Log.Error($"解包出错。", LoggerBllType.System);
            }
            network.Send(8003);

            result = await network.CallMessageAsync<int>(8004);
            if (!result.Result)
            {
                Log.Error($"RPC请求失败。", LoggerBllType.System);
            }

            if (result.Content != 100)
            {
                Log.Error($"解包出错。", LoggerBllType.System);
            }

            Count++;
            if (Swatch.ElapsedMilliseconds >= 1000)
            {
                Log.Info($"耗时:{Swatch.ElapsedMilliseconds}/ms RPS:{Count}", LoggerBllType.System);
                Swatch.Restart();
                Count = 0;
            }

        }
    }
}
