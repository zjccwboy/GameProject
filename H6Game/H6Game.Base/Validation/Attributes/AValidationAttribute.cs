using System;

namespace H6Game.Base.Validation.Attributes
{
    public abstract class AValidationAttribute : Attribute
    {
        public string ErrorMsg { get; set; }
    }
}
