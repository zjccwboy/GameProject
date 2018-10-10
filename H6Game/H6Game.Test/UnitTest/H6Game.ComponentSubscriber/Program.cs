using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.BaseTest;

namespace H6Game.ComponentSubscriber
{
    class Program
    {

        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnected += (c, t) => { Start(); };
            Game.Scene.AddComponent<ComponentSubscriberTester>();

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

            SubEmptyTask();
            SubIntTask();
            SubUIntTask();
            SubStringTask();
            SubLongTask();
            SubULongTask();
            SubFloatTask();
            SubDoubleTask();
            SubDecimalTask();
            SubByteTask();
            SubSByteTask();
            SubShortTask();
            SubUShortTask();
            SubCharTask();
            SubClassTask();


            SubIntTaskValue();
            SubUIntTaskValue();
            SubLongTaskValue();
            SubULongTaskValue();
            SubFloatTaskValue();
            SubDoubleTaskValue();
            SubDecimalTaskValue();
            SubByteTaskValue();
            SubSByteTaskValue();
            SubShortTaskValue();
            SubUShortTaskValue();
            SubCharTaskValue();
        }

        private static async void SubEmpty()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((int)NetCommandTest.SubEmpty);
            if (1024 == emptyResult)
            {
                Console.WriteLine("SubEmpty success.");
            }
        }

        private static async void SubInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, (int)NetCommandTest.SubInt);
            if (1024 == intResult)
            {
                Console.WriteLine("SubInt success.");
            }
        }

        private static async void SubUInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, (int)NetCommandTest.SubUInt);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUInt success.");
            }
        }

        private static async void SubString()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var stringResult = await network.CallMessageAsync<string, string>("1024", (int)NetCommandTest.SubString);
            if ("1024" == stringResult)
            {
                Console.WriteLine("SubString success.");
            }
        }

        private static async void SubLong()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, (int)NetCommandTest.SubLong);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLong success.");
            }
        }

        private static async void SubULong()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, (int)NetCommandTest.SubULong);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULong success.");
            }
        }

        private static async void SubFloat()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, (int)NetCommandTest.SubFloat);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloat success.");
            }
        }

        private static async void SubDouble()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024d, (int)NetCommandTest.SubDouble);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDouble success.");
            }
        }

        private static async void SubDecimal()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, (int)NetCommandTest.SubDecimal);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimal success.");
            }
        }

        private static async void SubByte()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, (int)NetCommandTest.SubByte);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubSByte()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, (int)NetCommandTest.SubSByte);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubShort()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, (int)NetCommandTest.SubShort);
            if (st == shortResult)
            {
                Console.WriteLine("SubShort success.");
            }
        }

        private static async void SubUShort()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, (int)NetCommandTest.SubUShort);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShort success.");
            }
        }

        private static async void SubChar()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', (int)NetCommandTest.SubChar);
            if ('a' == charResult)
            {
                Console.WriteLine("SubSByte success.");
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
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, (int)NetCommandTest.SubClass);
            if (message.Message == charResult.Message && message.TestId == charResult.TestId)
            {
                Console.WriteLine("SubClass success.");
            }
        }

        private static async void SubEmptyTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((int)NetCommandTest.SubEmptyTask);
            if (1024 == emptyResult)
            {
                Console.WriteLine("SubEmptyTask success.");
            }
        }

        private static async void SubIntTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, (int)NetCommandTest.SubIntTask);
            if (1024 == intResult)
            {
                Console.WriteLine("SubIntTask success.");
            }
        }

        private static async void SubUIntTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, (int)NetCommandTest.SubUIntTask);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUIntTask success.");
            }
        }

        private static async void SubStringTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var stringResult = await network.CallMessageAsync<string, string>("1024", (int)NetCommandTest.SubStringTask);
            if ("1024" == stringResult)
            {
                Console.WriteLine("SubStringTask success.");
            }
        }

        private static async void SubLongTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, (int)NetCommandTest.SubLongTask);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLongTask success.");
            }
        }

        private static async void SubULongTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, (int)NetCommandTest.SubULongTask);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULongTask success.");
            }
        }

        private static async void SubFloatTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, (int)NetCommandTest.SubFloatTask);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloatTask success.");
            }
        }

        private static async void SubDoubleTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024d, (int)NetCommandTest.SubDoubleTask);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDoubleTask success.");
            }
        }

        private static async void SubDecimalTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, (int)NetCommandTest.SubDecimalTask);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimalTask success.");
            }
        }

        private static async void SubByteTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, (int)NetCommandTest.SubByteTask);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByteTask success.");
            }
        }

        private static async void SubSByteTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, (int)NetCommandTest.SubSByteTask);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubSByteTask success.");
            }
        }

        private static async void SubShortTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, (int)NetCommandTest.SubShortTask);
            if (st == shortResult)
            {
                Console.WriteLine("SubShortTask success.");
            }
        }

        private static async void SubUShortTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, (int)NetCommandTest.SubUShortTask);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShortTask success.");
            }
        }

        private static async void SubCharTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', (int)NetCommandTest.SubCharTask);
            if ('a' == charResult)
            {
                Console.WriteLine("SubCharTask success.");
            }
        }

        private static async void SubClassTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var message = new TesterMessage
            {
                Message = "测试消息！",
                TestId = 1024,
            };
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, (int)NetCommandTest.SubClassTask);
            if (message.Message == charResult.Message && message.TestId == charResult.TestId)
            {
                Console.WriteLine("SubClassTask success.");
            }
        }



        
        private static async void SubIntTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, (int)NetCommandTest.SubIntTaskValue);
            if (1024 == intResult)
            {
                Console.WriteLine("SubIntTaskValue success.");
            }
        }

        private static async void SubUIntTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, (int)NetCommandTest.SubUIntTaskValue);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUIntTaskValue success.");
            }
        }

        private static async void SubLongTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, (int)NetCommandTest.SubLongTaskValue);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLongTaskValue success.");
            }
        }

        private static async void SubULongTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, (int)NetCommandTest.SubULongTaskValue);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULongTaskValue success.");
            }
        }

        private static async void SubFloatTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, (int)NetCommandTest.SubFloatTaskValue);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloatTaskValue success.");
            }
        }

        private static async void SubDoubleTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double,double>(10241024.1024d, (int)NetCommandTest.SubDoubleTaskValue);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDoubleTaskValue success.");
            }
        }

        private static async void SubDecimalTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, (int)NetCommandTest.SubDecimalTaskValue);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimalTaskValue success.");
            }
        }

        private static async void SubByteTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, (int)NetCommandTest.SubByteTaskValue);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByteTaskValue success.");
            }
        }

        private static async void SubSByteTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, (int)NetCommandTest.SubSByteTaskValue);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubSByteTaskValue success.");
            }
        }

        private static async void SubShortTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, (int)NetCommandTest.SubShortTaskValue);
            if (st == shortResult)
            {
                Console.WriteLine("SubShortTaskValue success.");
            }
        }

        private static async void SubUShortTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, (int)NetCommandTest.SubUShortTaskValue);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShortTaskValue success.");
            }
        }

        private static async void SubCharTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', (int)NetCommandTest.SubCharTaskValue);
            if ('a' == charResult)
            {
                Console.WriteLine("SubCharTaskValue success.");
            }
        }
    }
}
