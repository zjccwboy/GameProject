using System;

namespace H6Game.Base.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : AValidationAttribute
    {
        public RequiredAttribute(string errorMsg)
        {
            this.ErrorMsg = errorMsg;
        }
    }
}
