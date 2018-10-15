using H6Game.Base;
using H6Game.BaseTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.TesterSubscriber
{
    [NetCommand(NetCommandTest.SubEmpty)]
    public class SubEmpty : NetSubscriber
    {
        protected override void Subscribe(Network network, int netCommand)
        {
            network.Response(1024);
        }
    }

    [NetCommand(NetCommandTest.SubInt)]
    public class SubInt : NetSubscriber<int>
    {
        protected override void Subscribe(Network network, int message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubUInt)]
    public class SubUInt : NetSubscriber<uint>
    {
        protected override void Subscribe(Network network, uint message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubString)]
    public class SubString : NetSubscriber<string>
    {
        protected override void Subscribe(Network network, string message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubLong)]
    public class SubLong : NetSubscriber<long>
    {
        protected override void Subscribe(Network network, long message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubULong)]
    public class SubULong : NetSubscriber<ulong>
    {
        protected override void Subscribe(Network network, ulong message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubFloat)]
    public class SubFloat : NetSubscriber<float>
    {
        protected override void Subscribe(Network network, float message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubDouble)]
    public class SubDouble : NetSubscriber<double>
    {
        protected override void Subscribe(Network network, double message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubDecimal)]
    public class SubDecimal : NetSubscriber<decimal>
    {
        protected override void Subscribe(Network network, decimal message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubByte)]
    public class SubByte : NetSubscriber<byte>
    {
        protected override void Subscribe(Network network, byte message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubSByte)]
    public class SubSByte : NetSubscriber<sbyte>
    {
        protected override void Subscribe(Network network, sbyte message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubShort)]
    public class SubShort : NetSubscriber<short>
    {
        protected override void Subscribe(Network network, short message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubUShort)]
    public class SubUShort : NetSubscriber<ushort>
    {
        protected override void Subscribe(Network network, ushort message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubChar)]
    public class SubUChar : NetSubscriber<char>
    {
        protected override void Subscribe(Network network, char message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubClass)]
    public class SubObject : NetSubscriber<TesterMessage>
    {
        protected override void Subscribe(Network network, TesterMessage message, int netCommand)
        {
            network.Response(message);
        }
    }

    [NetCommand(NetCommandTest.SubEnum)]
    public class SubEnum : NetSubscriber<EnumType>
    {
        protected override void Subscribe(Network network, EnumType message, int netCommand)
        {
            network.Response(message);
        }
    }
}
