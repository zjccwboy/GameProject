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
            if (id == int.MaxValue)
                id = 0;
            id++;
            return id;
        }
    }

    /// <summary>
    /// KCP连接确认号Conv生成器
    /// </summary>
    public class KcpConvIdCreator
    {
        private const int MinSize = 100000;
        private const int MaxSize = 0xFFFFFF;
        private static int id = MinSize;
        public static int CreateId()
        {
            if (id == 0xFFFFFF)
                id = MinSize;
            id++;
            return id;
        }
    }
}
