using System.Collections.Concurrent;
using System.Collections.Generic;

namespace H6Game.Base.Message
{
    public class ParserStorage
    {
        private static ConcurrentDictionary<int, ConcurrentQueue<PacketParser>> ParserCache { get; } = new ConcurrentDictionary<int, ConcurrentQueue<PacketParser>>();

        public static PacketParser GetParser()
        {
            return GetParser(SegmentBuffer.DefaultBlockSize);
        }

        public static PacketParser GetParser(int bufferBlockSize)
        {
            if (!ParserCache.TryGetValue(bufferBlockSize, out ConcurrentQueue<PacketParser> queue))
            {
                queue = new ConcurrentQueue<PacketParser>();
                ParserCache[bufferBlockSize] = queue;
            }

            if(!queue.TryDequeue(out PacketParser packet))
            {
                packet = new PacketParser(bufferBlockSize);
            }
            return packet;
        }

        public static void Push(PacketParser parser)
        {
            parser.Clear();
            if (!ParserCache.TryGetValue(parser.BlockSize, out ConcurrentQueue<PacketParser> queue))
            {
                queue = new ConcurrentQueue<PacketParser>();
                ParserCache[parser.BlockSize] = queue;
            }
            queue.Enqueue(parser);
        }
    }
}
