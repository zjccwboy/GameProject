using H6Game.Base;
using H6Game.Base.Component;
using H6Game.Base.Config;
using H6Game.BaseTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TesterSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnected += (c, t) => { Start(); };
            Game.Start();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        private static void Start()
        {
            SubEmpty();
            SubInt();
            SubUInt();
            SubString();
            SubLong();
            SubULong();
            SubFloat();
            SubDouble();
            SubDecimal();
            SubByte();
            SubSByte();
            SubShort();
            SubUShort();
            SubChar();
            SubClass();
            SubEnum();
        }

        private static async void SubEmpty()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((ushort)NetCommandTest.SubEmpty);
            if(1024 == emptyResult)
            {
                Console.WriteLine("SubEmpty success.");
            }
            else
            {
                Console.WriteLine("SubInt error.");
            }
        }

        private static async void SubInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, (ushort)NetCommandTest.SubInt);
            if (1024 == intResult)
            {
                Console.WriteLine("SubInt success.");
            }
            else
            {
                Console.WriteLine("SubInt error.");
            }
        }

        private static async void SubUInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024, (ushort)NetCommandTest.SubUInt);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUInt success.");
            }
            else
            {
                Console.WriteLine("SubUInt error.");
            }
        }

        private static async void SubString()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var stringResult = await network.CallMessageAsync<string, string>("1024", (ushort)NetCommandTest.SubString);
            if ("1024" == stringResult)
            {
                Console.WriteLine("SubString success.");
            }
            else
            {
                Console.WriteLine("SubString error.");
            }
        }

        private static async void SubLong()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, (ushort)NetCommandTest.SubLong);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLong success.");
            }
            else
            {
                Console.WriteLine("SubLong error.");
            }
        }

        private static async void SubULong()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024, (ushort)NetCommandTest.SubULong);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULong success.");
            }
            else
            {
                Console.WriteLine("SubULong error.");
            }
        }

        private static async void SubFloat()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, (ushort)NetCommandTest.SubFloat);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloat success.");
            }
            else
            {
                Console.WriteLine("SubFloat error.");
            }
        }

        private static async void SubDouble()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024, (ushort)NetCommandTest.SubDouble);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDouble success.");
            }
            else
            {
                Console.WriteLine("SubDouble error.");
            }
        }

        private static async void SubDecimal()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, (ushort)NetCommandTest.SubDecimal);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimal success.");
            }
            else
            {
                Console.WriteLine("SubDecimal error.");
            }
        }

        private static async void SubByte()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte,byte>(bt, (ushort)NetCommandTest.SubByte);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByte success.");
            }
            else
            {
                Console.WriteLine("SubByte error.");
            }
        }

        private static async void SubSByte()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, (ushort)NetCommandTest.SubSByte);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubSByte success.");
            }
            else
            {
                Console.WriteLine("SubSByte error.");
            }
        }

        private static async void SubShort()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, (ushort)NetCommandTest.SubShort);
            if (st == shortResult)
            {
                Console.WriteLine("SubShort success.");
            }
            else
            {
                Console.WriteLine("SubShort error.");
            }
        }

        private static async void SubUShort()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, (ushort)NetCommandTest.SubUShort);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShort success.");
            }
        }

        private static async void SubChar()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', (ushort)NetCommandTest.SubChar);
            if ('a' == charResult)
            {
                Console.WriteLine("SubChar success.");
            }
            else
            {
                Console.WriteLine("SubChar error.");
            }
        }

        private static async void SubClass()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var message = new TesterMessage
            {
                Message = "测试消息！",
                TestId = 1024,
            };
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, (ushort)NetCommandTest.SubClass);
            if (message.Message == charResult.Message && message.TestId == charResult.TestId)
            {
                Console.WriteLine("SubClass success.");
            }
            else
            {
                Console.WriteLine("SubClass error.");
            }
        }

        private static async void SubEnum()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var enumResult = await network.CallMessageAsync<EnumType, EnumType>(EnumType.Test1, (ushort)NetCommandTest.SubEnum);
            if (EnumType.Test1 == enumResult)
            {
                Console.WriteLine("SubEnum success.");
            }
            else
            {
                Console.WriteLine("SubEnum error.");
            }
        }
    }
}
