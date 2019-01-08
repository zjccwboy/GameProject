using H6Game.Base.Exceptions;
using System;
using System.IO;

namespace H6Game.Base.Message
{   
    public class ParseState
    {
        public const int PacketSize = 1;
        public const int Command = 2;
        public const int MsgType = 3;
        public const int RpcId = 4;
        public const int Body = 5;
    }

    /// <summary>
    /// 包解析类
    /// </summary>
    public class PacketParser
    {
        public PacketParser(int blockSize)
        {
            this.BlockSize = blockSize;
            Buffer = new SegmentBuffer(blockSize);
            Packet = new Packet(this);
        }

        public SegmentBuffer Buffer { get; }
        public Packet Packet { get; }
        public int BlockSize { get;}

        private int ReadLength = 0;
        private int PacketSize = 0;
        private int State = ParseState.PacketSize;
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
        /// 包头协议中Msg消息Id字节数，2个字节
        /// </summary>
        public const int NetCommandFlagSize = sizeof(ushort);
        /// <summary>
        /// 消息类型,2个字节
        /// </summary>
        public const int MsgTypeFlagSize = sizeof(ushort);
        /// <summary>
        /// 包头协议中RPC请求Id的字节数，2个字节
        /// </summary>
        public const int RpcFlagSize = sizeof(ushort);
        /// <summary>
        /// 包头大小
        /// </summary>
        public const int HeadSize = LengthFlagSize + BitFlagSize + NetCommandFlagSize + MsgTypeFlagSize + RpcFlagSize;

        /// <summary>
        /// NetCommand偏移地址
        /// </summary>
        private const int NetCommandOffset = LengthFlagSize + BitFlagSize;
        /// <summary>
        /// MsgType偏移地址
        /// </summary>
        private const int MsgTypeOffset = LengthFlagSize + BitFlagSize + NetCommandFlagSize;
        /// <summary>
        /// RpcId偏移地址
        /// </summary>
        private const int RpcIdOffset = LengthFlagSize + BitFlagSize + NetCommandFlagSize + MsgTypeFlagSize;

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
                    case ParseState.PacketSize:
                        {
                            if (ReadLength == 0 && Buffer.DataSize < HeadSize)
                                return;

                            ReadPacketSize();
                            State = ParseState.Command;
                        }
                        break;
                    case ParseState.Command:
                        {
                            ReadCommand();
                            State = ParseState.MsgType;
                        }
                        break;
                    case ParseState.MsgType:
                        {
                            ReadMsgType();
                            State = ParseState.RpcId;
                        }
                        break;
                    case ParseState.RpcId:
                        {
                            ReadRpcId();
                            State = ParseState.Body;
                        }
                        break;
                    case ParseState.Body:
                        {
                            ReadBody();
                        }
                        break;
                }

                if (ReadLength == PacketSize)
                {
                    Packet.BodyStream.Seek(0, SeekOrigin.Begin);
                    Packet.BodyStream.SetLength(PacketSize - HeadSize);
                    State = ParseState.PacketSize;
                    IsOk = true;
                    return;
                }
                else if (Buffer.DataSize == 0)
                    return;
                else if (Buffer.DataSize < PacketSize - ReadLength)
                    return;
            }
        }

        private void ReadPacketSize()
        {
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
        }

        private void ReadCommand()
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
            Packet.NetCommand = BitConverter.ToUInt16(Packet.HeadBytes, offset);
        }

        private void ReadMsgType()
        {
            var offset = MsgTypeOffset;
            if (Buffer.FirstDataSize >= MsgTypeFlagSize)
            {
                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, MsgTypeFlagSize);
                Buffer.UpdateRead(MsgTypeFlagSize);
            }
            else
            {
                var count = Buffer.FirstDataSize;
                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset, count);
                Buffer.UpdateRead(count);
                System.Buffer.BlockCopy(Buffer.First, Buffer.FirstReadOffset, Packet.HeadBytes, offset + count, MsgTypeFlagSize - count);
                Buffer.UpdateRead(MsgTypeFlagSize - count);
            }
            ReadLength += MsgTypeFlagSize;
            Packet.MsgTypeCode = BitConverter.ToUInt16(Packet.HeadBytes, offset);
        }

        private void ReadRpcId()
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
            Packet.RpcId = BitConverter.ToUInt16(Packet.HeadBytes, offset);
        }

        private void ReadBody()
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
        }

        private void SetBitFlag(byte flagByte)
        {
            Packet.IsHeartbeat = Convert.ToBoolean(flagByte & 1);
            Packet.IsCompress = Convert.ToBoolean(flagByte >> 1 & 1);
            Packet.IsEncrypt = Convert.ToBoolean(flagByte >> 2 & 1);
            Packet.KcpProtocal = (byte)(flagByte >> 4 & 3);
        }

        private void Flush()
        {
            Packet.Flush();
            ReadLength = 0;
            PacketSize = 0;
            IsOk = false;
        }

        internal void Clear()
        {
            State = ParseState.PacketSize;
            Flush();
            Buffer.Flush();
        }

        internal virtual bool TryRead()
        {
            if (IsOk)
                Flush();

            Parse();
            return IsOk;
        }

        internal void WriteBuffer(byte[] bytes, int offset, int length)
        {
            Buffer.Write(bytes, offset, length);
        }

        internal void WriteBuffer(Packet packet)
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
