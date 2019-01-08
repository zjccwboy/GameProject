using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.BaseTest
{
    public enum EnumType
    {
        None,
        Test1,
        Test2,
    }

    public enum NetCommandTest : ushort
    {
        SubEmpty = 55555,
        SubString,
        SubInt,
        SubUInt,
        SubLong,
        SubULong,
        SubFloat,
        SubDouble,
        SubDecimal,
        SubByte,
        SubSByte,
        SubShort,
        SubUShort,
        SubChar,
        SubClass,
        SubEnum,

        SubEmptyTask,
        SubStringTask,
        SubIntTask,
        SubUIntTask,
        SubLongTask,
        SubULongTask,
        SubFloatTask,
        SubDoubleTask,
        SubDecimalTask,
        SubByteTask,
        SubSByteTask,
        SubShortTask,
        SubUShortTask,
        SubCharTask,
        SubClassTask,
        SubEnumTask,

        SubEmptyTaskValue,
        SubIntTaskValue,
        SubUIntTaskValue,
        SubLongTaskValue,
        SubULongTaskValue,
        SubFloatTaskValue,
        SubDoubleTaskValue,
        SubDecimalTaskValue,
        SubByteTaskValue,
        SubSByteTaskValue,
        SubShortTaskValue,
        SubUShortTaskValue,
        SubCharTaskValue,

    }
}
