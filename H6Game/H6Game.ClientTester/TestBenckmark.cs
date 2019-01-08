using H6Game.Base;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using H6Game.Hotfix.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace H6Game.ClientTester
{
    public static class TestBenckmark
    {
        private static Stopwatch stopWatch = new Stopwatch();
        private static int Count = 0;

        public static void Start(Network network)
        {
            stopWatch.Start();
            for (var i = 0; i < 1000; i++)
                Benckmark(network);
        }

        private static async void Benckmark(Network network)
        {
            while(true)
                await Call(network);
        }

        static TestMessage send = new TestMessage
        {
            ActorId = 10001,
            Message = "Message",
            LongData = 29999,
            ULongData = 30000011,
            ByteData = 200,
            SByteData = 80,
            UIntData = 191919191,
            ListIntData = new List<int> { 1, 2, 3, 4, 5, 6, },
        };

        public static async Task Call(Network network)
        {
            await network.CallMessageAsync<TestMessage, TestMessage>(send, (ushort)TestCMD.TestCmd);

            Count++;
            if (stopWatch.ElapsedMilliseconds > 1000)
            {
                Log.Debug($"每{stopWatch.ElapsedMilliseconds}/毫秒 RPS:{Count}/条", LoggerBllType.System);
                stopWatch.Restart();
                Count = 0;
            }
        }
    }

    public enum TestCMD
    {
        TestCmd = 47777,
    }

    [ProtoBuf.ProtoContract]
    [NetMessageType(NetMessageType.TestDistributedTestMessage)]
    public class TestMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public int ActorId { get; set; }
        [ProtoBuf.ProtoMember(2)]
        public string Message { get; set; }
        [ProtoBuf.ProtoMember(3)]
        public long LongData { get; set; }
        [ProtoBuf.ProtoMember(4)]
        public ulong ULongData { get; set; }    
        [ProtoBuf.ProtoMember(5)]
        public byte ByteData { get; set; }
        [ProtoBuf.ProtoMember(6)]
        public sbyte SByteData { get; set; }
        [ProtoBuf.ProtoMember(7)]
        public uint UIntData { get; set; }
        [ProtoBuf.ProtoMember(8)]
        public List<int> ListIntData { get; set; }
    }
}
