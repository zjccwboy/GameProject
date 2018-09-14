﻿using System;
using System.IO;

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
        public int MessageCmd;

        /// <summary>
        /// 消息类型指定代码
        /// </summary>
        public int MsgTypeCode;

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
        public byte[] HeadBytes { get; } = new byte[PacketParser.HeadSize];

        /// <summary>
        /// BodyStream
        /// </summary>
        public MemoryStream BodyStream { get; }

        /// <summary>
        /// 解包缓冲区
        /// </summary>
        private PacketParser Parser { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parser"></param>
        public Packet(PacketParser parser)
        {
            this.BodyStream = new MemoryStream(parser.BlockSize);
            this.Parser = parser;

            this.IsCompress = PacketConfig.IsCompress;
            this.IsEncrypt = PacketConfig.IsEncrypt;
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
            MessageCmd = 0;
            RpcId = 0;
            ActorId = 0;
            MsgTypeCode = 0;

            this.BodyStream.Seek(0, SeekOrigin.Begin);
            this.BodyStream.SetLength(0);
        }

        public void Dispose()
        {
            this.BodyStream.Close();
            this.BodyStream.Dispose();
        }
    }

    public class PacketConfig
    {
        public static bool IsCompress { get; set; }
        public static bool IsEncrypt { get; set; }
    }
}
