using System;

namespace H6Game.Base.Validation.Attributes
{
    /// <summary>
    /// 校验集合、字符串长度。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class LengthAttribute : AValidationAttribute
    {
        public dynamic Max { get; }
        public dynamic Min { get; }

        public LengthAttribute(int max, int min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public LengthAttribute(long max, long min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }
    }
}
