using H6Game.Base;
using H6Game.BaseTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.ComponentSubscriber
{
    public class ComponentSubscriberTester : NetComponentSubscriber
    {
        [NetCommand(NetCommandTest.SubEmpty)]
        public int SubEmpty()
        {
            return 1024;
        }

        [NetCommand(NetCommandTest.SubInt)]
        public int SubInt(int data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubUInt)]
        public uint SubUInt(uint data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubString)]
        public string SubString(string data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubLong)]
        public long SubLong(long data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubULong)]
        public ulong SubULong(ulong data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubFloat)]
        public float SubFloat(float data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubDouble)]
        public double SubDouble(double data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubDecimal)]
        public decimal SubDecimal(decimal data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubByte)]
        public byte SubByte(byte data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubSByte)]
        public sbyte SubSByte(sbyte data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubShort)]
        public short SubShort(short data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubUShort)]
        public ushort SubUShort(ushort data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubChar)]
        public char SubChar(char data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubClass)]
        public TesterMessage SubClass(TesterMessage data)
        {
            return data;
        }

        [NetCommand(NetCommandTest.SubEnum)]
        public EnumType SubClass(EnumType data)
        {
            return data;
        }

        

        //-----------------------------------------------------------------------------------------



        [NetCommand(NetCommandTest.SubEmptyTask)]
        public Task<int> SubEmptyTask()
        {
            return Task.FromResult(1024);
        }

        [NetCommand(NetCommandTest.SubIntTask)]
        public Task<int> SubIntTask(int data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubUIntTask)]
        public Task<uint> SubUIntTask(uint data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubStringTask)]
        public Task<string> SubStringTask(string data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubLongTask)]
        public Task<long> SubLongTask(long data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubULongTask)]
        public Task<ulong> SubULongTask(ulong data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubFloatTask)]
        public Task<float> SubFloatTask(float data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubDoubleTask)]
        public Task<double> SubDoubleTask(double data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubDecimalTask)]
        public Task<decimal> SubDecimalTask(decimal data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubByteTask)]
        public Task<byte> SubByteTask(byte data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubSByteTask)]
        public Task<sbyte> SubSByteTask(sbyte data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubShortTask)]
        public Task<short> SubShortTask(short data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubUShortTask)]
        public Task<ushort> SubUShortTask(ushort data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubCharTask)]
        public Task<char> SubCharTask(char data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubClassTask)]
        public Task<TesterMessage> SubClassTask(TesterMessage data)
        {
            return Task.FromResult(data);
        }

        [NetCommand(NetCommandTest.SubEnumTask)]
        public Task<EnumType> SubEnumTask(EnumType data)
        {
            return Task.FromResult(data);
        }



        //-----------------------------------------------------------------------------------------




        [NetCommand(NetCommandTest.SubIntTaskValue)]
        public Task<int> SubIntTask(MyInt32 data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubUIntTaskValue)]
        public Task<uint> SubUIntTask(MyUInt32 data)
        {
            return Task.FromResult(data.Data);
        }


        [NetCommand(NetCommandTest.SubLongTaskValue)]
        public Task<long> SubLongTask(MyLong data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubULongTaskValue)]
        public Task<ulong> SubULongTask(MyULong data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubFloatTaskValue)]
        public Task<float> SubFloatTask(MyFloat data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubDoubleTaskValue)]
        public Task<double> SubDoubleTask(MyDouble data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubDecimalTaskValue)]
        public Task<decimal> SubDecimalTask(MyDecimal data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubByteTaskValue)]
        public Task<byte> SubByteTask(MyByte data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubSByteTaskValue)]
        public Task<sbyte> SubSByteTask(MySByte data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubShortTaskValue)]
        public Task<short> SubShortTask(MyShort data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubUShortTaskValue)]
        public Task<ushort> SubUShortTask(MyUShort data)
        {
            return Task.FromResult(data.Data);
        }

        [NetCommand(NetCommandTest.SubCharTaskValue)]
        public Task<char> SubCharTask(MyChar data)
        {
            return Task.FromResult(data.Data);
        }
    }

    public enum EnumType
    {
        None,
        Test1,
        Test2,
    }

}
