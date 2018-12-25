using System.Threading;

namespace H6Game.Base.Message
{
    /// <summary>
    /// 通讯通道Id生成器，只适应TCP
    /// </summary>
    public class ChannelIdCreator
    {
        private static int id;
        public static int CreateId()
        {
            Interlocked.Increment(ref id);
            Interlocked.CompareExchange(ref id, 1, int.MaxValue);
            return id;
        }
    }

    /// <summary>
    /// KCP连接确认号Conv生成器
    /// </summary>
    public class KcpConvIdCreator
    {
        private static int id = 100000;
        public static int CreateId()
        {
            Interlocked.Increment(ref id);
            Interlocked.CompareExchange(ref id, 100000, int.MaxValue);
            return id;
        }
    }
}
