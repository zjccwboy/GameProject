using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using H6Game.Base;
using H6Game.Base.Message;
using H6Game.Base.Config;
using H6Game.Base.Component;

namespace H6Game.BaseTest
{
    //[NetCommand(1000)]
    //public class TestClass1 : NetSubscriber
    //{
    //    protected override void Subscribe(Network network, int netCommand)
    //    {

    //    }
    //}

    //[NetCommand(1000)]
    //public class TestClass2 : NetSubscriber
    //{
    //    protected override void Subscribe(Network network, int netCommand)
    //    {

    //    }
    //}

    [NetCommand(2001)]
    public class TestClass3 : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, ushort netCommand)
        {
            throw new NotImplementedException();
        }
    }

    //[NetCommand(2000)]
    //public class TestClass4 : NetSubscriber<int>
    //{
    //    protected override void Subscribe(Network network, int message, int netCommand)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    


    public class MessageSubscriberPoolTest
    {
        [Fact]
        public void TestInsert()
        {
            Game.Scene.AddComponent<MongoConfig>();
            Game.Scene.AddComponent<NetDistributionsComponent>();
        }
    }
}
