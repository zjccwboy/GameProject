﻿using H6Game.Base;
using H6Game.BaseTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace H6Game.TesterSubscriber
{
    [NetCommand(NetCommandTest.SubEmpty)]
    public class SubEmpty : NetSubscriber
    {
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(1024);
        }
    }

    [NetCommand(NetCommandTest.SubInt)]
    public class SubInt : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubUInt)]
    public class SubUInt : NetSubscriber<uint>
    {
        protected override void Subscribe(Network network, uint message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubString)]
    public class SubString : NetSubscriber<string>
    {
        protected override void Subscribe(Network network, string message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubLong)]
    public class SubLong : NetSubscriber<long>
    {
        protected override void Subscribe(Network network, long message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubULong)]
    public class SubULong : NetSubscriber<ulong>
    {
        protected override void Subscribe(Network network, ulong message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubFloat)]
    public class SubFloat : NetSubscriber<float>
    {
        protected override void Subscribe(Network network, float message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubDouble)]
    public class SubDouble : NetSubscriber<double>
    {
        protected override void Subscribe(Network network, double message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubDecimal)]
    public class SubDecimal : NetSubscriber<decimal>
    {
        protected override void Subscribe(Network network, decimal message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubByte)]
    public class SubByte : NetSubscriber<byte>
    {
        protected override void Subscribe(Network network, byte message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubSByte)]
    public class SubSByte : NetSubscriber<sbyte>
    {
        protected override void Subscribe(Network network, sbyte message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubShort)]
    public class SubShort : NetSubscriber<short>
    {
        protected override void Subscribe(Network network, short message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubUShort)]
    public class SubUShort : NetSubscriber<ushort>
    {
        protected override void Subscribe(Network network, ushort message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubChar)]
    public class SubUChar : NetSubscriber<char>
    {
        protected override void Subscribe(Network network, char message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubClass)]
    public class SubObject : NetSubscriber<TesterMessage>
    {
        protected override void Subscribe(Network network, TesterMessage message, int netCommand)
        {
            network.Response(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
            Game.Scene.AddComponent<NetConnectingComponent>().OnConnected += (c, t) => { Start(); };

            while(true)
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
        }

        private static async void SubEmpty()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var emptyResult = await network.CallMessageAsync<int>((int)NetCommandTest.SubEmpty);
            if(1024 == emptyResult)
            {
                Console.WriteLine("SubEmpty success.");
            }
        }

        private static async void SubInt()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;
            var intResult = await network.CallMessageAsync<int, int>(1024, (int)NetCommandTest.SubInt);
            if (1024 == intResult)
            {
                Console.WriteLine("SubInt success.");
            }
        }

        private static async void SubUInt()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var uintResult = await network.CallMessageAsync<uint, uint>(1024, (int)NetCommandTest.SubUInt);
            if (1024 == uintResult)
            {
                Console.WriteLine("SubUInt success.");
            }
        }

        private static async void SubString()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var stringResult = await network.CallMessageAsync<string, string>("1024", (int)NetCommandTest.SubString);
            if ("1024" == stringResult)
            {
                Console.WriteLine("SubString success.");
            }
        }

        private static async void SubLong()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var longResult = await network.CallMessageAsync<long, long>(102410241024, (int)NetCommandTest.SubLong);
            if (102410241024 == longResult)
            {
                Console.WriteLine("SubLong success.");
            }
        }

        private static async void SubULong()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var ulongResult = await network.CallMessageAsync<ulong, ulong>(102410241024, (int)NetCommandTest.SubULong);
            if (102410241024 == ulongResult)
            {
                Console.WriteLine("SubULong success.");
            }
        }

        private static async void SubFloat()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var floatResult = await network.CallMessageAsync<float, float>(1024.1024f, (int)NetCommandTest.SubFloat);
            if (1024.1024 == floatResult)
            {
                Console.WriteLine("SubFloat success.");
            }
        }

        private static async void SubDouble()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var doubleResult = await network.CallMessageAsync<double, double>(10241024.1024, (int)NetCommandTest.SubDouble);
            if (10241024.1024 == doubleResult)
            {
                Console.WriteLine("SubDouble success.");
            }       
        }

        private static async void SubDecimal()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            var decimalResult = await network.CallMessageAsync<decimal, decimal>(10241024.1024m, (int)NetCommandTest.SubDecimal);
            if (10241024.1024m == decimalResult)
            {
                Console.WriteLine("SubDecimal success.");
            }            
        }

        private static async void SubByte()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            byte bt = 127;
            var byteResult = await network.CallMessageAsync<byte,byte>(bt, (int)NetCommandTest.SubByte);
            if (bt == byteResult)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubSByte()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            sbyte sbt = 127;
            var sbyteResult = await network.CallMessageAsync<sbyte, sbyte>(sbt, (int)NetCommandTest.SubSByte);
            if (sbt == sbyteResult)
            {
                Console.WriteLine("SubByte success.");
            }
        }

        private static async void SubShort()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            short st = 30000;
            var shortResult = await network.CallMessageAsync<short, short>(st, (int)NetCommandTest.SubShort);
            if (st == shortResult)
            {
                Console.WriteLine("SubShort success.");
            }
        }

        private static async void SubUShort()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;

            ushort ust = 65535;
            var ushortResult = await network.CallMessageAsync<ushort, ushort>(ust, (int)NetCommandTest.SubUShort);
            if (ust == ushortResult)
            {
                Console.WriteLine("SubUShort success.");
            }
        }

        private static async void SubChar()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;
            var charResult = await network.CallMessageAsync<char, char>('a', (int)NetCommandTest.SubSByte);
            if ('a' == charResult)
            {
                Console.WriteLine("SubSByte success.");
            }
        }

        private static async void SubClass()
        {
            var network = Game.Scene.GetComponent<NetConnectingComponent>().Network;
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
    }
}
