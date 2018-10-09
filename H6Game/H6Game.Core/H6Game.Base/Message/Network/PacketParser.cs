using System;
using System.IO;
using System.Net;

namespace H6Game.Base
{   
    /// <summary>
    /// 包状态
    /// </summary>
    public class ParseState
    {
        /// <summary>
        /// 开始数据包大小标志，包头32位
        /// </summary>
        public const int Size = 1;

        /// <summary>
        /// 消息Msg字段
        /// </summary>
        public const int Cmd = 2;

        /// <summary>
        /// 消息类型
        /// </summary>
        public const int MsgType = 3;

        /// <summary>
        /// RPC消息字段
        /// </summary>
        public const int Rpc = 4;

        /// <summary>
        /// 消息包体
        /// </summary>
        public const int Body = 5;
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
        public BufferQueue Buffer { get; }
        public Packet Packet { get; }
        public int BlockSize { get;} = 8192;

        private int ReadLength = 0;
        private int PacketSize = 0;
        private int State = ParseState.Size;
        private bool IsOk;

        /// <summary>
        /// 包头协议中表示数据包大小的第一个协议字节数，4个字节
        /// </summary>
        public const int LengthFlagSize = sizeof(int);
        /// <summary>
        /// 包头协议中标志位的字节数，1个字节
        /// </summary>
        public const int BitFlagSize = sizeof(byte);
        /// <summary>
        /// 包头协议中Msg消息Id字节数，4个字节
        /// </summary>
        public const int NetCommandFlagSize = sizeof(int);
        /// <summary>
        /// 消息类型,4个字节
        /// </summary>
        public const int MsgTypeSize = sizeof(int);
        /// <summary>
        /// 包头协议中RPC请求Id的字节数，4个字节
        /// </summary>
        public const int RpcFlagSize = sizeof(int);
        /// <summary>
        /// 包头大小
        /// </summary>
        public const int HeadSize = LengthFlagSize + BitFlagSize + NetCommandFlagSize + MsgTypeSize + RpcFlagSize;

        /// <summary>
        /// NetCommand偏移地址
        /// </summary>
        private const int NetCommandOffset = LengthFlagSize + BitFlagSize;
        /// <summary>
        /// MsgType偏移地址
        /// </summary>
        private const int MsgTypeOffset = LengthFlagSize + BitFlagSize + NetCommandFlagSize;
        /// <summary>
        /// RpcIdO偏移地址
        /// </summary>
        private const int RpcIdOffset = LengthFlagSize + BitFlagSize + NetCommandFlagSize + MsgTypeSize;

        /// <summary>
        /// 解析数据包核心函数
        /// </summary>
        private void Parse()
        {
            var tryCount = 0;
            while (true)
            {
                if (tryCount > 5)
                    throw new PacketParserException("解包错误，数据包非法.");

                tryCount++;
                switch (State)
                {
                    case ParseState.Size:
                        {
                            if (ReadLength == 0 && Buffer.DataSize < HeadSize)
                            {
                                return;
                            }

                            if (Buffer.FirstDataSize >= LengthFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, 0, LengthFlagSize);
                                Buffer.UpdateRead(LengthFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, 0, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, count, LengthFlagSize - count);
                                Buffer.UpdateRead(LengthFlagSize - count);
                            }
                            ReadLength += LengthFlagSize;
                            PacketSize = BitConverter.ToInt32(Packet.HeadBytes, 0);

                            System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, LengthFlagSize, BitFlagSize);
                            Buffer.UpdateRead(BitFlagSize);
                            ReadLength += BitFlagSize;
                            SetBitFlag(Packet.HeadBytes[LengthFlagSize]);
                            State = ParseState.Cmd;
                            break;
                        }                        
                    case ParseState.Cmd:
                        {
                            var offset = NetCommandOffset;
                            if (Buffer.FirstDataSize >= NetCommandFlagSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, NetCommandFlagSize);
                                Buffer.UpdateRead(NetCommandFlagSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, NetCommandFlagSize - count);
                                Buffer.UpdateRead(NetCommandFlagSize - count);
                            }
                            ReadLength += NetCommandFlagSize;
                            Packet.NetCommand = BitConverter.ToInt32(Packet.HeadBytes, offset);
                            State = ParseState.MsgType;
                            break;
                        }
                    case ParseState.MsgType:
                        {
                            var offset = MsgTypeOffset;
                            if (Buffer.FirstDataSize >= MsgTypeSize)
                            {
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, MsgTypeSize);
                                Buffer.UpdateRead(MsgTypeSize);
                            }
                            else
                            {
                                var count = Buffer.FirstDataSize;
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                                Buffer.UpdateRead(count);
                                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, MsgTypeSize - count);
                                Buffer.UpdateRead(MsgTypeSize - count);
                            }
                            ReadLength += MsgTypeSize;
                            Packet.MsgTypeCode = BitConverter.ToInt32(Packet.HeadBytes, offset);
                            State = ParseState.Rpc;
                            break;
                        }
                    case ParseState.Rpc:
                        {
                            var offset = RpcIdOffset;
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
                            ReadLength += RpcFlagSize;
                            Packet.RpcId = BitConverter.ToInt32(Packet.HeadBytes, offset);
                            State = ParseState.Body;
                            break;
                        }
                    case ParseState.Body:
                        {
                            var needSize = PacketSize - ReadLength;
                            if (Buffer.DataSize >= needSize)
                            {
                                if (Buffer.FirstDataSize >= needSize)
                                {
                                    Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, needSize);
                                    Buffer.UpdateRead(needSize);
                                    ReadLength += needSize;
                                }
                                else
                                {
                                    while (ReadLength < PacketSize)
                                    {
                                        var count = Buffer.FirstDataSize;
                                        Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, count);
                                        Buffer.UpdateRead(count);
                                        ReadLength += count;
                                        needSize -= count;
                                        count = needSize > Buffer.FirstDataSize ? Buffer.FirstDataSize : needSize;
                                        Packet.BodyStream.Write(Buffer.First, Buffer.FirstReadOffset, count);
                                        Buffer.UpdateRead(count);
                                        ReadLength += count;
                                    }
                                }
                            }
                            break;
                        }                        
                }

                if (ReadLength == PacketSize)
                {
                    Packet.BodyStream.Seek(0, SeekOrigin.Begin);
                    Packet.BodyStream.SetLength(PacketSize - HeadSize);
                    State = ParseState.Size;
                    IsOk = true;
                    return;
                }
                else if (Buffer.DataSize == 0)
                {
                    return;
                }
                else if (Buffer.DataSize < PacketSize - ReadLength)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 设置解析标志位结果
        /// </summary>
        /// <param name="flagByte"></param>
        private void SetBitFlag(byte flagByte)
        {
            Packet.IsHeartbeat = Convert.ToBoolean(flagByte & 1);
            Packet.IsCompress = Convert.ToBoolean(flagByte >> 1 & 1);
            Packet.IsEncrypt = Convert.ToBoolean(flagByte >> 2 & 1);
            Packet.KcpProtocal = (byte)(flagByte >> 4 & 3);
        }

        /// <summary>
        /// 重置当前解析器所有状态
        /// </summary>
        internal void Clear()
        {
            State = ParseState.Size;
            Flush();
            Buffer.Flush();
        }

        /// <summary>
        /// 重置解析器
        /// </summary>
        private void Flush()
        {
            Packet.Flush();
            ReadLength = 0;
            PacketSize = 0;
            IsOk = false;
        }

        /// <summary>
        /// 从缓冲区中读数据包
        /// </summary>
        /// <returns></returns>
        internal bool TryRead()
        {
            if (IsOk)
                Flush();

            Parse();
            return IsOk;
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
