using System;
using System.Collections.Generic;
using System.Linq;

namespace H6Game.Base
{
    /// <summary>
    /// 字节数组缓冲区，使用两个队列存放字节数组，WorkerBuffer是读写的缓冲区，在读完队列第一个字节数组后把字节数
    /// 组放到CacheBuffer中，写缓冲区是写到WorkerBuffer最后一条，写完之后再从CacheBuffer中取，这样可以做到循环使
    /// 用字节数组，可以避免频繁分配字节数组带来的GC开销。
    /// </summary>
    public class SegmentBuffer
    {
        /// <summary>
        /// 缓冲区块大小
        /// </summary>
        private int BlockSize { get; } = DefaultBlockSize;

        /// <summary>
        /// 默认缓冲区块大小
        /// </summary>
        public const int DefaultBlockSize = 8192;

        /// <summary>
        /// 读写缓冲区队列队列
        /// </summary>
        private readonly Queue<byte[]> WorkerBuffer = new Queue<byte[]>();

        /// <summary>
        /// 用于复用的缓冲区队列
        /// </summary>
        private readonly Queue<byte[]> CacheBuffer = new Queue<byte[]>();

        /// <summary>
        /// 构造函数，默认分配缓冲区块大小8192字节
        /// </summary>
        public SegmentBuffer()
        {
            //默认分配一块缓冲区
            //WorkerBuffer.Enqueue(new byte[BlockSize]);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blockSize">指定缓冲区块大小</param>
        public SegmentBuffer(int blockSize)
        {
            this.BlockSize = blockSize;
            //默认分配一块缓冲区
            //WorkerBuffer.Enqueue(new byte[blockSize]);
        }

        /// <summary>
        /// 指向缓冲区队列中第一个缓冲区块读取数据的数组下标位置
        /// </summary>
        public int FirstReadOffset;

        /// <summary>
        /// 指向缓冲区队列中最后一个缓冲区块写入数据的数组下标位置
        /// </summary>
        public int LastWriteOffset;

        /// <summary>
        /// 更新读的的缓冲区字节数
        /// </summary>
        /// <param name="addValue"></param>
        public void UpdateRead(int addValue)
        {
            FirstReadOffset += addValue;
            if (FirstReadOffset > BlockSize)
            {
                throw new ArgumentOutOfRangeException("缓冲区索引超出有效范围.");
            }

            if (FirstReadOffset == BlockSize)
            {
                FirstReadOffset = 0;
                CacheBuffer.Enqueue(WorkerBuffer.Dequeue());
            }
        }

        /// <summary>
        /// 更新写的的缓冲区字节数
        /// </summary>
        /// <param name="addValue"></param>
        public void UpdateWrite(int addValue)
        {
            LastWriteOffset += addValue;
            if (LastWriteOffset > BlockSize)
            {
                throw new ArgumentOutOfRangeException("缓冲区索引超出有效范围.");
            }

            if (LastWriteOffset == BlockSize)
            {
                LastWriteOffset = 0;
                if (CacheBuffer.Count > 0)
                {
                    WorkerBuffer.Enqueue(CacheBuffer.Dequeue());
                }
                else
                {
                    WorkerBuffer.Enqueue(new byte[BlockSize]);
                }
            }
        }

        /// <summary>
        /// 缓冲区队列中第一个缓冲区块可写字符数
        /// </summary>
        public int FirstDataSize
        {
            get
            {
                var result = 0;
                if(WorkerBuffer.Count == 1)
                {
                    result = LastWriteOffset - FirstReadOffset;
                }
                else
                {
                    result = BlockSize - FirstReadOffset;
                }

                if(result < 0)
                {
                    throw new ArgumentOutOfRangeException("缓冲区索引超出有效范围.");
                }

                return result;
            }
        }

        /// <summary>
        /// 缓冲区队列中最后一个缓冲区块可写字符数
        /// </summary>
        public int LastCapacity
        {
            get
            {
                return BlockSize - LastWriteOffset;
            }
        }

        /// <summary>
        /// 当前缓冲区中有效数据的字节数
        /// </summary>
        public int DataSize
        {
            get
            {
                int size = 0;
                if (WorkerBuffer.Count == 0)
                {
                    return size;
                }
                else if(WorkerBuffer.Count == 1)
                {
                    size = LastWriteOffset - FirstReadOffset;
                }
                else
                {
                    size = WorkerBuffer.Count * BlockSize - FirstReadOffset - LastCapacity;
                }
                if (size < 0)
                {
                    throw new ArgumentOutOfRangeException("缓冲区索引超出有效范围.");
                }
                return size;
            }
        }

        /// <summary>
        /// 写入一个字节数组到缓冲区中，该方法不需要调用UpdateWrite方法更新缓冲区字节数
        /// </summary>
        /// <param name="bytes"></param>
        public void Write(byte[] bytes)
        {
            Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 写入一个字节数组指定的字节数到缓冲区中，该方法不需要调用UpdateWrite方法更新缓冲区字节数
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        public void Write(byte[] bytes, int index, int length)
        {
            while(length > 0)
            {
                var count = length > LastCapacity ? LastCapacity : length;
                Buffer.BlockCopy(bytes, index, Last, LastWriteOffset, count);
                index += count;
                length -= count;
                UpdateWrite(count);
            }
        }

        /// <summary>
        /// 指向缓冲区队列最后一个缓冲区块指针
        /// </summary>
        public byte[] Last
        {
            get
            {
                if(WorkerBuffer.Count == 0)
                {
                    if(CacheBuffer.Count == 0)
                        WorkerBuffer.Enqueue(new byte[this.BlockSize]);
                    else
                        WorkerBuffer.Enqueue(CacheBuffer.Dequeue());
                }
                return WorkerBuffer.Last();
            }
        }

        /// <summary>
        /// 指向缓冲区队列最后第一个缓冲区块指针
        /// </summary>
        public byte[] First
        {
            get
            {
                if (WorkerBuffer.Count == 0)
                {
                    if (CacheBuffer.Count == 0)
                        WorkerBuffer.Enqueue(new byte[this.BlockSize]);
                    else
                        WorkerBuffer.Enqueue(CacheBuffer.Dequeue());
                }
                return WorkerBuffer.Peek();
            }
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Flush()
        {
            FirstReadOffset = 0;
            LastWriteOffset = 0;
            var segments = WorkerBuffer.Count + CacheBuffer.Count;
            var bytes = segments * BlockSize;
            while (bytes > DefaultBlockSize * 2)
            {
                if(WorkerBuffer.Count > 1)
                    WorkerBuffer.Dequeue();
                else
                    CacheBuffer.Dequeue();

                segments = WorkerBuffer.Count + CacheBuffer.Count;
                bytes = segments * BlockSize;
            }

            while(WorkerBuffer.Count > 1)
            {
                CacheBuffer.Enqueue(WorkerBuffer.Dequeue());
            }
        }
    }
}
