using System.Collections.Generic;

namespace H6Game.Base.Message
{
    public class ParserStorage
    {
        private static Dictionary<int, Queue<PacketParser>> ParserCache { get; } = new Dictionary<int, Queue<PacketParser>>();

        public static PacketParser GetParser()
        {
            return GetParser(SegmentBuffer.DefaultBlockSize);
        }

        public static PacketParser GetParser(int bufferBlockSize)
        {
            if (!ParserCache.TryGetValue(bufferBlockSize, out Queue<PacketParser> queue))
            {
                queue = new Queue<PacketParser>();
                ParserCache[bufferBlockSize] = queue;
            }

            if (queue.Count == 0)
                return new PacketParser(bufferBlockSize);

            return queue.Dequeue();
        }

        public static void Push(PacketParser parser)
        {
            parser.Clear();
            if (!ParserCache.TryGetValue(parser.BlockSize, out Queue<PacketParser> queue))
            {
                queue = new Queue<PacketParser>();
                ParserCache[parser.BlockSize] = queue;
            }
            queue.Enqueue(parser);
        }
    }
}
