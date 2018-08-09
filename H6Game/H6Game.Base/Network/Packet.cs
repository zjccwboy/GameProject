using System;
using System.IO;
using System.Net;

namespace H6Game.Base
{
    /// <summary>
    /// 数据包
    /// </summary>
    public class Packet : IDisposable
    {
        /// <summary>
        /// Rpc请求标志
        /// </summary>
        internal bool IsRpc { get { return RpcId > 0; } }

        /// <summary>
        /// 心跳标志
        /// </summary>
        internal bool IsHeartbeat;

        /// <summary>
        /// 压缩标志
        /// </summary>
        public bool IsCompress;

        /// <summary>
        /// 加密标志
        /// </summary>
        public bool IsEncrypt;

        /// <summary>
        /// Kcp包协议
        /// </summary>
        public byte KcpProtocal;

        /// <summary>
        /// MessageId
        /// </summary>
        public int MessageId;

        /// <summary>
        /// Rpc请求标识
        /// </summary>
        public int RpcId;

        /// <summary>
        /// Actor消息
        /// </summary>
        public int ActorId;

        /// <summary>
        /// 包头字节数组
        /// </summary>
        public byte[] HeadBytes = new byte[PacketParser.HeadSize];

        /// <summary>
        /// BodyStream
        /// </summary>
        public MemoryStream BodyStream { get; }

        /// <summary>
        /// 解包缓冲区
        /// </summary>
        private PacketParser Parser { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parser"></param>
        public Packet(PacketParser parser)
        {
            this.BodyStream = new MemoryStream(parser.BlockSize);
            this.Parser = parser;
        }

        public void WriteBuffer()
        {
            this.Parser.WriteBuffer(this);
            this.Flush();
        }

        /// <summary>
        /// 重置数据包
        /// </summary>
        internal void Flush()
        {
            IsEncrypt = false;
            IsCompress = false;
            IsHeartbeat = false;
            KcpProtocal = 0;
            MessageId = 0;
            RpcId = 0;
            ActorId = 0;

            this.BodyStream.Seek(0, SeekOrigin.Begin);
            this.BodyStream.SetLength(0);
        }

        /// <summary>
        /// 获取包头的字节数组
        /// </summary>
        /// <returns></returns>
        internal byte[] GetHeadBytes(int bodySize)
        {
            //写包大小
            int PacketSize = PacketParser.HeadSize + bodySize;
            var bodySizeBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt32(PacketSize)));
            HeadBytes[0] = bodySizeBytes[0];
            HeadBytes[1] = bodySizeBytes[1];
            HeadBytes[2] = bodySizeBytes[2];
            HeadBytes[3] = bodySizeBytes[3];

            HeadBytes[4] = 0;
            //写标志位
            if (IsHeartbeat)
            {
                HeadBytes[4] |= 1;
            }
            if (IsCompress)
            {
                HeadBytes[4] |= 1 << 1;
            }
            if (IsEncrypt)
            {
                HeadBytes[4] |= 1 << 2;
            }
            if (KcpProtocal > 0)
            {
                HeadBytes[4] |= (byte)(KcpProtocal << 4);
            }

            //写MessageId
            var rpcBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt32(MessageId)));
            HeadBytes[5] = rpcBytes[0];
            HeadBytes[6] = rpcBytes[1];
            HeadBytes[7] = rpcBytes[2];
            HeadBytes[8] = rpcBytes[3];

            //写RpcId
            var snBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt32(RpcId)));
            HeadBytes[9] = snBytes[0];
            HeadBytes[10] = snBytes[1];
            HeadBytes[11] = snBytes[2];
            HeadBytes[12] = snBytes[3];

            //写ActorId
            var actorBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(Convert.ToInt32(ActorId)));
            HeadBytes[13] = actorBytes[0];
            HeadBytes[14] = actorBytes[1];
            HeadBytes[15] = actorBytes[2];
            HeadBytes[16] = actorBytes[3];

            return HeadBytes;
        }

        public void Dispose()
        {
            this.BodyStream.Close();
            this.BodyStream.Dispose();
        }
    }
}
