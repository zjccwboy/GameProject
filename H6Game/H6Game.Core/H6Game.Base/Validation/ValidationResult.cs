
namespace H6Game.Base.Validation
{
    [ProtoBuf.ProtoContract]
    public class ValidationResult
    {
        [ProtoBuf.ProtoMember(1)]
        public int ResultCode { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public string ErrorMessage { get; set; }
    }
}