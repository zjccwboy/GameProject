using System;

namespace H6Game.Base.Validation.Attributes
{
    /// <summary>
    /// 校验值类型大小
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SizeAttribute : AValidationAttribute
    {
        public dynamic Max { get; }
        public dynamic Min { get; }

        public SizeAttribute(double max, double min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(float max, float min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(int max, int min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(uint max, uint min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(decimal max, decimal min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(long max, long min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(ulong max, ulong min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(short max, short min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }

        public SizeAttribute(ushort max, ushort min, string errorMsg)
        {
            this.Max = max;
            this.Min = min;
            this.ErrorMsg = errorMsg;
        }
    }
}
