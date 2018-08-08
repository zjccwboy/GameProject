﻿using System;
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

    /// <summary>
    /// 包状态
    /// </summary>
    public enum ParseState
    {
        /// <summary>
        /// 开始数据包大小标志，包头16位
        /// </summary>
        Size,

        /// <summary>
        /// 消息Msg字段
        /// </summary>
        Msg,

        /// <summary>
        /// RPC消息字段
        /// </summary>
        Rpc,

        /// <summary>
        /// ActorId消息字段
        /// </summary>
        Actor,

        /// <summary>
        /// 消息包体
        /// </summary>
        Body,
    }

    /// <summary>
    /// 包解析类
    /// </summary>
    public class PacketParser
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PacketParser()
        {
            Buffer = new BufferQueue();
            Packet = new Packet(this);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blockSize">指定缓冲区块大小</param>
        public PacketParser(int blockSize)
        {
            this.BlockSize = blockSize;
            Buffer = new BufferQueue(blockSize);
            Packet = new Packet(this);
        }

        /// <summary>
        /// 缓冲区对象
        /// </summary>
        public BufferQueue Buffer { get; private set; }
        public Packet Packet { get; private set; }
        public int BlockSize { get;} = 8192;

        private int readLength = 0;
        private int packetSize = 0;
        private ParseState state;
        private bool isOk;
        private bool finish;

        /// <summary>
        /// 包头协议中表示数据包大小的第一个协议字节数，2个字节
        /// </summary>
        public static readonly int PacketFlagSize = sizeof(int);
        /// <summary>
        /// 包头协议中标志位的字节数，1个字节
        /// </summary>
        public static readonly int BitFlagSize = sizeof(byte);
        /// <summary>
        /// 包头协议中RPC请求Id的字节数，4个字节
        /// </summary>
        public static readonly int RpcFlagSize = sizeof(int);
        /// <summary>
        /// 包头协议中Msg消息Id字节数，4个字节
        /// </summary>
        public static readonly int MessageIdFlagSize = sizeof(int);
        /// <summary>
        /// Actor消息Id字节数，4个字节
        /// </summary>
        public static readonly int ActorIdFlagSize = sizeof(int);
        /// <summary>
        /// 包头大小
        /// </summary>
        public static readonly int HeadSize = PacketFlagSize + BitFlagSize + RpcFlagSize + MessageIdFlagSize + ActorIdFlagSize;

        /// <summary>
        /// 解析数据包核心函数
        /// </summary>
        private void Parse()
        {
            var tryCount = 0;
            isOk = false;
            while (true)
            {
                if (tryCount > 5)
                    throw new Exception("解包错误，数据包非法.");

                tryCount++;
                switch (state)
                {
                    case ParseState.Size:
                        {
                            if (readLength == 0 && Buffer.DataSize < HeadSize)
                            {
                                finish = true;
                                return;
                            }

                            if (Buffer.FirstDataSize >= PacketFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, 0, PacketFlagSize);
                                Buffer.UpdateRead(PacketFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, 0, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, count, PacketFlagSize - count);
                                Buffer.UpdateRead(PacketFlagSize - count);
                            }
                            readLength += PacketFlagSize;
                            packetSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Packet.HeadBytes, 0));

                            System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, PacketFlagSize, BitFlagSize);
                            Buffer.UpdateRead(BitFlagSize);
                            readLength += BitFlagSize;
                            SetBitFlag(Packet.HeadBytes[PacketFlagSize]);
                            state = ParseState.Msg;
                            break;
                        }                        
                    case ParseState.Msg:
                        {
                            var offset = PacketFlagSize + BitFlagSize;
                            if (Buffer.FirstDataSize >= MessageIdFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, MessageIdFlagSize);
                                Buffer.UpdateRead(MessageIdFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, MessageIdFlagSize - count);
                                Buffer.UpdateRead(MessageIdFlagSize - count);
                            }
                            readLength += MessageIdFlagSize;
                            Packet.MessageId = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Packet.HeadBytes, offset));
                            state = ParseState.Rpc;
                            break;
                        }
                    case ParseState.Rpc:
                        {
                            var offset = PacketFlagSize + BitFlagSize + MessageIdFlagSize;
                            if (Buffer.FirstDataSize >= RpcFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, RpcFlagSize);
                                Buffer.UpdateRead(RpcFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, RpcFlagSize - count);
                                Buffer.UpdateRead(RpcFlagSize - count);
                            }
                            readLength += RpcFlagSize;
                            Packet.RpcId = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Packet.HeadBytes, offset));
                            state = ParseState.Actor;
                            break;
                        }
                    case ParseState.Actor:
                        {
                            var offset = PacketFlagSize + BitFlagSize + MessageIdFlagSize + ActorIdFlagSize;
                            if (Buffer.FirstDataSize >= ActorIdFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, ActorIdFlagSize);
                                Buffer.UpdateRead(RpcFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, ActorIdFlagSize - count);
                                Buffer.UpdateRead(RpcFlagSize - count);
                            }
                            readLength += ActorIdFlagSize;
                            Packet.ActorId = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Packet.HeadBytes, offset));
                            state = ParseState.Body;
                            break;
                        }
                    case ParseState.Body:
                        {
                            var needSize = packetSize - readLength;
                            if (Buffer.DataSize >= needSize)
                            {
                                Packet.BodyStream.Seek(0, SeekOrigin.Begin);
                                if (Buffer.FirstDataSize >= needSize)
                                {
                                    Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, needSize);
                                    Buffer.UpdateRead(needSize);
                                    readLength += needSize;
                                }
                                else
                                {
                                    while (readLength < packetSize)
                                    {
                                        var count = Buffer.FirstDataSize;
                                        Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, count);
                                        Buffer.UpdateRead(count);
                                        readLength += count;
                                        needSize -= count;
                                        count = needSize > Buffer.FirstDataSize ? Buffer.FirstDataSize : needSize;
                                        Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, count);
                                        Buffer.UpdateRead(count);
                                        readLength += count;
                                    }
                                }
                                Packet.BodyStream.Seek(0, SeekOrigin.Begin);
                            }
                            break;
                        }                        
                }

                if (Buffer.DataSize == 0)
                    finish = true;

                if (Buffer.DataSize < packetSize - readLength)
                    finish = true;

                if (readLength == packetSize)
                    isOk = true;

                if (isOk)
                {
                    state = ParseState.Size;
                    break;
                }

                if (finish)
                    break;
            }
        }

        /// <summary>
        /// 设置解析标志位结果
        /// </summary>
        /// <param name="flagByte"></param>
        private void SetBitFlag(byte flagByte)
        {
            Packet.IsHeartbeat = Convert.ToBoolean(flagByte >> 1 & 1);
            Packet.IsCompress = Convert.ToBoolean(flagByte >> 2 & 1);
            Packet.IsEncrypt = Convert.ToBoolean(flagByte >> 3 & 1);
            Packet.KcpProtocal = (byte)(flagByte >> 4 & 3);
        }

        /// <summary>
        /// 重置当前解析器所有状态
        /// </summary>
        internal void Clear()
        {
            state = ParseState.Size;
            isOk = false;
            finish = false;
            Flush();
            Buffer.Flush();
        }

        /// <summary>
        /// 重置解析器
        /// </summary>
        private void Flush()
        {
            Packet.Flush();
            readLength = 0;
            packetSize = 0;
        }

        /// <summary>
        /// 从缓冲区中读数据包
        /// </summary>
        /// <returns></returns>
        internal bool TryRead()
        {
            if (isOk)
                Flush();

            finish = false;
            while (!finish)
            {
                Parse();
                if (isOk)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 写一个字节数组到缓冲区中
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void WriteBuffer(byte[] bytes, int offset, int length)
        {
            Buffer.Write(bytes, offset, length);
        }

        /// <summary>
        /// 写一个包到缓冲区中
        /// </summary>
        /// <param name="packet"></param>
        public void WriteBuffer(Packet packet)
        {
            var bodySize = (int)packet.BodyStream.Length;
            Buffer.Write(Packet.GetHeadBytes(bodySize));
            if (bodySize > 0)
            {
                var bytes = packet.BodyStream.GetBuffer();
                Buffer.Write(bytes, 0, bodySize);
            }
        }
    }
}
