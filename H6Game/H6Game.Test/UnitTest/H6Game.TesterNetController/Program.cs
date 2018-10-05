using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H6Game.Base;
using H6Game.BaseTest;

namespace H6Game.TesterNetController
{
    class Program
    {

        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<DistributionsComponent>();
            Game.Scene.AddComponent<OuterComponent>().OnConnected += c => { Start(); };
            Game.Scene.AddComponent<TesterNetController>();

            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }

        public class TesterNetController : NetController
        {
            [NetCommand(NetCommandTest.SubEmpty)]
            public int SubEmpty()
            {
                return 1024;
            }

            [NetCommand(NetCommandTest.SubInt)]
            public int SubInt(int data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubUInt)]
            public uint SubUInt(uint data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubString)]
            public string SubString(string data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubLong)]
            public long SubLong(long data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubULong)]
            public ulong SubULong(ulong data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubFloat)]
            public float SubFloat(float data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubDouble)]
            public double SubDouble(double data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubDecimal)]
            public decimal SubDecimal(decimal data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubByte)]
            public byte SubByte(byte data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubSByte)]
            public sbyte SubSByte(sbyte data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubShort)]
            public short SubShort(short data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubUShort)]
            public ushort SubUShort(ushort data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubChar)]
            public char SubChar(char data)
            {
                return data;
            }

            [NetCommand(NetCommandTest.SubClass)]
            public TesterMessage SubClass(TesterMessage data)
            {
                return data;
            }





            [NetCommand(NetCommandTest.SubEmptyTask)]
            public Task<int> SubEmptyTask()
            {
                return Task.FromResult(1024);
            }

            [NetCommand(NetCommandTest.SubIntTask)]
            public Task<int> SubIntTask(int data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubUIntTask)]
            public Task<uint> SubUIntTask(uint data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubStringTask)]
            public Task<string> SubStringTask(string data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubLongTask)]
            public Task<long> SubLongTask(long data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubULongTask)]
            public Task<ulong> SubULongTask(ulong data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubFloatTask)]
            public Task<float> SubFloatTask(float data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubDoubleTask)]
            public Task<double> SubDoubleTask(double data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubDecimalTask)]
            public Task<decimal> SubDecimalTask(decimal data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubByteTask)]
            public Task<byte> SubByteTask(byte data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubSByteTask)]
            public Task<sbyte> SubSByteTask(sbyte data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubShortTask)]
            public Task<short> SubShortTask(short data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubUShortTask)]
            public Task<ushort> SubUShortTask(ushort data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubCharTask)]
            public Task<char> SubCharTask(char data)
            {
                return Task.FromResult(data);
            }

            [NetCommand(NetCommandTest.SubClassTask)]
            public Task<TesterMessage> SubClassTask(TesterMessage data)
            {
                return Task.FromResult(data);
            }

            


            [NetCommand(NetCommandTest.SubIntTaskValue)]
            public Task<int> SubIntTask(Value<int> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubUIntTaskValue)]
            public Task<uint> SubUIntTask(Value<uint> data)
            {
                return Task.FromResult(data.Data);
            }


            [NetCommand(NetCommandTest.SubLongTaskValue)]
            public Task<long> SubLongTask(Value<long> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubULongTaskValue)]
            public Task<ulong> SubULongTask(Value<ulong> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubFloatTaskValue)]
            public Task<float> SubFloatTask(Value<float> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubDoubleTaskValue)]
            public Task<double> SubDoubleTask(Value<double> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubDecimalTaskValue)]
            public Task<decimal> SubDecimalTask(Value<decimal> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubByteTaskValue)]
            public Task<byte> SubByteTask(Value<byte> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubSByteTaskValue)]
            public Task<sbyte> SubSByteTask(Value<sbyte> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubShortTaskValue)]
            public Task<short> SubShortTask(Value<short> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubUShortTaskValue)]
            public Task<ushort> SubUShortTask(Value<ushort> data)
            {
                return Task.FromResult(data.Data);
            }

            [NetCommand(NetCommandTest.SubCharTaskValue)]
            public Task<char> SubCharTask(Value<char> data)
            {
                return Task.FromResult(data.Data);
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
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((int)NetCommandTest.SubEmpty);
            if (1024 == emptyResult.Content)
            {
                Console.WriteLine("SubEmpty success.");
            }
        }

        private static async void SubInt()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var intResult = await network.CallMessageAsync<int>(1024, (int)NetCommandTest.SubInt);
            if (1024 == intResult.Content)
            {
                Console.WriteLine("SubInt success.");
            }
        }

        private static async void SubUInt()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint>(1024u, (int)NetCommandTest.SubUInt);
            if (1024 == uintResult.Content)
            {
                Console.WriteLine("SubUInt success.");
            }
        }

        private static async void SubString()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var stringResult = await network.CallMessageAsync<string>("1024", (int)NetCommandTest.SubString);
            if ("1024" == stringResult.Content)
            {
                Console.WriteLine("SubString success.");
            }
        }

        private static async void SubLong()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var longResult = await network.CallMessageAsync<long>(102410241024, (int)NetCommandTest.SubLong);
            if (102410241024 == longResult.Content)
            {
                Console.WriteLine("SubLong success.");
            }
        }

        private static async void SubULong()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong>(102410241024u, (int)NetCommandTest.SubULong);
            if (102410241024 == ulongResult.Content)
            {
                Console.WriteLine("SubULong success.");
            }
        }

        private static async void SubFloat()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var floatResult = await network.CallMessageAsync<float>(1024.1024f, (int)NetCommandTest.SubFloat);
            if (1024.1024 == floatResult.Content)
            {
                Console.WriteLine("SubFloat success.");
            }
        }

        private static async void SubDouble()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double>(10241024.1024d, (int)NetCommandTest.SubDouble);
            if (10241024.1024 == doubleResult.Content)
            {
                Console.WriteLine("SubDouble success.");
            }
        }

        private static async void SubDecimal()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal>(10241024.1024m, (int)NetCommandTest.SubDecimal);
            if (10241024.1024m == decimalResult.Content)
            {
                Console.WriteLine("SubDecimal success.");
            }
        }

        private static async void SubByte()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte>(bt, (int)NetCommandTest.SubByte);
            if (bt == byteResult.Content)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubSByte()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte>(sbt, (int)NetCommandTest.SubSByte);
            if (sbt == sbyteResult.Content)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubShort()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short>(st, (int)NetCommandTest.SubShort);
            if (st == shortResult.Content)
            {
                Console.WriteLine("SubShort success.");
            }
        }

        private static async void SubUShort()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort>(ust, (int)NetCommandTest.SubUShort);
            if (ust == ushortResult.Content)
            {
                Console.WriteLine("SubUShort success.");
            }
        }

        private static async void SubChar()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var charResult = await network.CallMessageAsync<char>('a', (int)NetCommandTest.SubChar);
            if ('a' == charResult.Content)
            {
                Console.WriteLine("SubSByte success.");
            }
        }

        private static async void SubClass()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var message = new TesterMessage
            {
                Message = "测试消息！",
                TestId = 1024,
            };
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, (int)NetCommandTest.SubClass);
            if (message.Message == charResult.Content.Message && message.TestId == charResult.Content.TestId)
            {
                Console.WriteLine("SubClass success.");
            }
        }

        private static async void SubEmptyTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((int)NetCommandTest.SubEmptyTask);
            if (1024 == emptyResult.Content)
            {
                Console.WriteLine("SubEmptyTask success.");
            }
        }

        private static async void SubIntTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var intResult = await network.CallMessageAsync<int>(1024, (int)NetCommandTest.SubIntTask);
            if (1024 == intResult.Content)
            {
                Console.WriteLine("SubIntTask success.");
            }
        }

        private static async void SubUIntTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint>(1024u, (int)NetCommandTest.SubUIntTask);
            if (1024 == uintResult.Content)
            {
                Console.WriteLine("SubUIntTask success.");
            }
        }

        private static async void SubStringTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var stringResult = await network.CallMessageAsync<string>("1024", (int)NetCommandTest.SubStringTask);
            if ("1024" == stringResult.Content)
            {
                Console.WriteLine("SubStringTask success.");
            }
        }

        private static async void SubLongTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var longResult = await network.CallMessageAsync<long>(102410241024, (int)NetCommandTest.SubLongTask);
            if (102410241024 == longResult.Content)
            {
                Console.WriteLine("SubLongTask success.");
            }
        }

        private static async void SubULongTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong>(102410241024u, (int)NetCommandTest.SubULongTask);
            if (102410241024 == ulongResult.Content)
            {
                Console.WriteLine("SubULongTask success.");
            }
        }

        private static async void SubFloatTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var floatResult = await network.CallMessageAsync<float>(1024.1024f, (int)NetCommandTest.SubFloatTask);
            if (1024.1024 == floatResult.Content)
            {
                Console.WriteLine("SubFloatTask success.");
            }
        }

        private static async void SubDoubleTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double>(10241024.1024d, (int)NetCommandTest.SubDoubleTask);
            if (10241024.1024 == doubleResult.Content)
            {
                Console.WriteLine("SubDoubleTask success.");
            }
        }

        private static async void SubDecimalTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal>(10241024.1024m, (int)NetCommandTest.SubDecimalTask);
            if (10241024.1024m == decimalResult.Content)
            {
                Console.WriteLine("SubDecimalTask success.");
            }
        }

        private static async void SubByteTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte>(bt, (int)NetCommandTest.SubByteTask);
            if (bt == byteResult.Content)
            {
                Console.WriteLine("SubByteTask success.");
            }
        }

        private static async void SubSByteTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte>(sbt, (int)NetCommandTest.SubSByteTask);
            if (sbt == sbyteResult.Content)
            {
                Console.WriteLine("SubSByteTask success.");
            }
        }

        private static async void SubShortTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short>(st, (int)NetCommandTest.SubShortTask);
            if (st == shortResult.Content)
            {
                Console.WriteLine("SubShortTask success.");
            }
        }

        private static async void SubUShortTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort>(ust, (int)NetCommandTest.SubUShortTask);
            if (ust == ushortResult.Content)
            {
                Console.WriteLine("SubUShortTask success.");
            }
        }

        private static async void SubCharTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var charResult = await network.CallMessageAsync<char>('a', (int)NetCommandTest.SubCharTask);
            if ('a' == charResult.Content)
            {
                Console.WriteLine("SubCharTask success.");
            }
        }

        private static async void SubClassTask()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var message = new TesterMessage
            {
                Message = "测试消息！",
                TestId = 1024,
            };
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, (int)NetCommandTest.SubClassTask);
            if (message.Message == charResult.Content.Message && message.TestId == charResult.Content.TestId)
            {
                Console.WriteLine("SubClassTask success.");
            }
        }



        
        private static async void SubIntTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var intResult = await network.CallMessageAsync<int>(1024, (int)NetCommandTest.SubIntTaskValue);
            if (1024 == intResult.Content)
            {
                Console.WriteLine("SubIntTaskValue success.");
            }
        }

        private static async void SubUIntTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint>(1024u, (int)NetCommandTest.SubUIntTaskValue);
            if (1024 == uintResult.Content)
            {
                Console.WriteLine("SubUIntTaskValue success.");
            }
        }

        private static async void SubLongTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var longResult = await network.CallMessageAsync<long>(102410241024, (int)NetCommandTest.SubLongTaskValue);
            if (102410241024 == longResult.Content)
            {
                Console.WriteLine("SubLongTaskValue success.");
            }
        }

        private static async void SubULongTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong>(102410241024u, (int)NetCommandTest.SubULongTaskValue);
            if (102410241024 == ulongResult.Content)
            {
                Console.WriteLine("SubULongTaskValue success.");
            }
        }

        private static async void SubFloatTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var floatResult = await network.CallMessageAsync<float>(1024.1024f, (int)NetCommandTest.SubFloatTaskValue);
            if (1024.1024 == floatResult.Content)
            {
                Console.WriteLine("SubFloatTaskValue success.");
            }
        }

        private static async void SubDoubleTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double>(10241024.1024d, (int)NetCommandTest.SubDoubleTaskValue);
            if (10241024.1024 == doubleResult.Content)
            {
                Console.WriteLine("SubDoubleTaskValue success.");
            }
        }

        private static async void SubDecimalTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal>(10241024.1024m, (int)NetCommandTest.SubDecimalTaskValue);
            if (10241024.1024m == decimalResult.Content)
            {
                Console.WriteLine("SubDecimalTaskValue success.");
            }
        }

        private static async void SubByteTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte>(bt, (int)NetCommandTest.SubByteTaskValue);
            if (bt == byteResult.Content)
            {
                Console.WriteLine("SubByteTaskValue success.");
            }
        }

        private static async void SubSByteTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte>(sbt, (int)NetCommandTest.SubSByteTaskValue);
            if (sbt == sbyteResult.Content)
            {
                Console.WriteLine("SubSByteTaskValue success.");
            }
        }

        private static async void SubShortTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short>(st, (int)NetCommandTest.SubShortTaskValue);
            if (st == shortResult.Content)
            {
                Console.WriteLine("SubShortTaskValue success.");
            }
        }

        private static async void SubUShortTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort>(ust, (int)NetCommandTest.SubUShortTaskValue);
            if (ust == ushortResult.Content)
            {
                Console.WriteLine("SubUShortTaskValue success.");
            }
        }

        private static async void SubCharTaskValue()
        {
            var network = Game.Scene.GetComponent<OuterComponent>().Network;
            var charResult = await network.CallMessageAsync<char>('a', (int)NetCommandTest.SubCharTaskValue);
            if ('a' == charResult.Content)
            {
                Console.WriteLine("SubCharTaskValue success.");
            }
        }
    }
}
