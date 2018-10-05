using H6Game.Base.Validation;

namespace H6Game.Base
{
    [ProtoBuf.ProtoContract]
    public abstract class ValidationMessage : IMessage
    {
        [ProtoBuf.ProtoMember(1)]
        public ValidationResult Validation { get; set; }
    }
}
