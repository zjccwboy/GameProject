﻿using System;
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
            Game.Scene.AddComponent<NetConnectorComponent>().OnConnect += (c, t) => { Start(); };
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
            SubEnum();


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
            SubEnumTask();


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

            var emptyResult = await network.CallMessageAsync<int>(NetCommandTest.SubEmpty);
            if (1024 == emptyResult)
            {
                Console.WriteLine("SubEmpty success.");
            }
            else
            {
                Console.WriteLine("SubEmpty error.");
            }
        }

        private static async void SubInt()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, NetCommandTest.SubInt);
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

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, NetCommandTest.SubUInt);
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

            var stringResult = await network.CallMessageAsync<string, string>("1024", NetCommandTest.SubString);
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

            var longResult = await network.CallMessageAsync<long, long>(102410241024, NetCommandTest.SubLong);
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

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, NetCommandTest.SubULong);
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

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, NetCommandTest.SubFloat);
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

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024d, NetCommandTest.SubDouble);
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

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, NetCommandTest.SubDecimal);
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
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, NetCommandTest.SubByte);
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
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, NetCommandTest.SubSByte);
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
            var shortResult = await network.CallMessageAsync<short, short>(st, NetCommandTest.SubShort);
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
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, NetCommandTest.SubUShort);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShort success.");
            }
            else
            {
                Console.WriteLine("SubUShort error.");
            }
        }

        private static async void SubChar()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', NetCommandTest.SubChar);
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
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, NetCommandTest.SubClass);
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
            var enumResult = await network.CallMessageAsync<EnumType, EnumType>(EnumType.Test1, NetCommandTest.SubEnum);
            if (EnumType.Test1 == enumResult)
            {
                Console.WriteLine("SubEnum success.");
            }
            else
            {
                Console.WriteLine("SubEnum error.");
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }








        private static async void SubEmptyTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>(NetCommandTest.SubEmptyTask);
            if (1024 == emptyResult)
            {
                Console.WriteLine("SubEmptyTask success.");
            }
            else
            {
                Console.WriteLine("SubEmptyTask error.");
            }
        }

        private static async void SubIntTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, NetCommandTest.SubIntTask);
            if (1024 == intResult)
            {
                Console.WriteLine("SubIntTask success.");
            }
            else
            {
                Console.WriteLine("SubIntTask error.");
            }
        }

        private static async void SubUIntTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, NetCommandTest.SubUIntTask);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUIntTask success.");
            }
            else
            {
                Console.WriteLine("SubUIntTask error.");
            }
        }

        private static async void SubStringTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var stringResult = await network.CallMessageAsync<string, string>("1024", NetCommandTest.SubStringTask);
            if ("1024" == stringResult)
            {
                Console.WriteLine("SubStringTask success.");
            }
            else
            {
                Console.WriteLine("SubStringTask error.");
            }
        }

        private static async void SubLongTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, NetCommandTest.SubLongTask);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLongTask success.");
            }
            else
            {
                Console.WriteLine("SubLongTask error.");
            }
        }

        private static async void SubULongTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, NetCommandTest.SubULongTask);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULongTask success.");
            }
            else
            {
                Console.WriteLine("SubULongTask error.");
            }
        }

        private static async void SubFloatTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, NetCommandTest.SubFloatTask);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloatTask success.");
            }
            else
            {
                Console.WriteLine("SubFloatTask error.");
            }
        }

        private static async void SubDoubleTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024d, NetCommandTest.SubDoubleTask);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDoubleTask success.");
            }
            else
            {
                Console.WriteLine("SubDoubleTask error.");
            }
        }

        private static async void SubDecimalTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, NetCommandTest.SubDecimalTask);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimalTask success.");
            }
            else
            {
                Console.WriteLine("SubDecimalTask error.");
            }
        }

        private static async void SubByteTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, NetCommandTest.SubByteTask);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByteTask success.");
            }
            else
            {
                Console.WriteLine("SubByteTask error.");
            }
        }

        private static async void SubSByteTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, NetCommandTest.SubSByteTask);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubSByteTask success.");
            }
            else
            {
                Console.WriteLine("SubSByteTask error.");
            }
        }

        private static async void SubShortTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, NetCommandTest.SubShortTask);
            if (st == shortResult)
            {
                Console.WriteLine("SubShortTask success.");
            }
            else
            {
                Console.WriteLine("SubShortTask error.");
            }
        }

        private static async void SubUShortTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, NetCommandTest.SubUShortTask);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShortTask success.");
            }
            else
            {
                Console.WriteLine("SubUShortTask error.");
            }
        }

        private static async void SubCharTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', NetCommandTest.SubCharTask);
            if ('a' == charResult)
            {
                Console.WriteLine("SubCharTask success.");
            }
            else
            {
                Console.WriteLine("SubCharTask error.");
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
            var charResult = await network.CallMessageAsync<TesterMessage, TesterMessage>(message, NetCommandTest.SubClassTask);
            if (message.Message == charResult.Message && message.TestId == charResult.TestId)
            {
                Console.WriteLine("SubClassTask success.");
            }
            else
            {
                Console.WriteLine("SubClassTask error.");
            }
        }

        private static async void SubEnumTask()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var enumResult = await network.CallMessageAsync<EnumType, EnumType>(EnumType.Test2, NetCommandTest.SubEnumTask);
            if (EnumType.Test2 == enumResult)
            {
                Console.WriteLine("SubEnumTask success.");
            }
            else
            {
                Console.WriteLine("SubEnumTask error.");
            }

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }



        //___________________________________________________________________________________________________________________________________


        private static async void SubIntTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, NetCommandTest.SubIntTaskValue);
            if (1024 == intResult)
            {
                Console.WriteLine("SubIntTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubIntTaskValue error.");
            }
        }

        private static async void SubUIntTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024u, NetCommandTest.SubUIntTaskValue);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUIntTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubUIntTaskValue error.");
            }
        }

        private static async void SubLongTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, NetCommandTest.SubLongTaskValue);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLongTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubLongTaskValue error.");
            }
        }

        private static async void SubULongTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024u, NetCommandTest.SubULongTaskValue);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULongTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubULongTaskValue error.");
            }
        }

        private static async void SubFloatTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, NetCommandTest.SubFloatTaskValue);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloatTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubFloatTaskValue error.");
            }
        }

        private static async void SubDoubleTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double,double>(10241024.1024d, NetCommandTest.SubDoubleTaskValue);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDoubleTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubDoubleTaskValue error.");
            }
        }

        private static async void SubDecimalTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, NetCommandTest.SubDecimalTaskValue);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimalTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubDecimalTaskValue error.");
            }
        }

        private static async void SubByteTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte, byte>(bt, NetCommandTest.SubByteTaskValue);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByteTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubByteTaskValue error.");
            }
        }

        private static async void SubSByteTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, NetCommandTest.SubSByteTaskValue);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubSByteTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubSByteTaskValue error.");
            }
        }

        private static async void SubShortTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, NetCommandTest.SubShortTaskValue);
            if (st == shortResult)
            {
                Console.WriteLine("SubShortTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubShortTaskValue error.");
            }
        }

        private static async void SubUShortTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, NetCommandTest.SubUShortTaskValue);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShortTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubUShortTaskValue error.");
            }
        }

        private static async void SubCharTaskValue()
        {
            var network = Game.Scene.GetComponent<NetConnectorComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', NetCommandTest.SubCharTaskValue);
            if ('a' == charResult)
            {
                Console.WriteLine("SubCharTaskValue success.");
            }
            else
            {
                Console.WriteLine("SubCharTaskValue error.");
            }
        }
    }
}
