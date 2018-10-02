using H6Game.Base;
using H6Game.Hotfix.Enums;
using H6Game.Hotfix.Messages.Enums;
using ProtoBuf;

namespace H6Game.Hotfix.Messages.OutNet
{
    [MessageType(OutMessageType.LoginResponeMessage)]
    [ProtoContract]
    public class LoginResponeMessage : LoginMessage
    {
        [ProtoMember(2)]
        public LoginResutlCode LoginResult { get; set; }

        [ProtoMember(3)]
        public string SessionKey { get; set; }

        [ProtoMember(4)]
        public bool IsNewAccount { get; set; }
        
        [ProtoMember(5)]
        public int AccountId { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        [ProtoMember(6)]
        public AccountType FType { get; set; }

        /// <summary>
        /// 账号名
        /// </summary>
        [ProtoMember(7)]
        public string FAccountName { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        [ProtoMember(8)]
        public VIPLevel FVIPLevel { get; set; }

        /// <summary>
        /// 微信登陆OpenId
        /// </summary>
        [ProtoMember(9)]
        public string FWXOpenId { get; set; }

        /// <summary>
        /// 微信头像地址
        /// </summary>
        [ProtoMember(10)]
        public string FWXHeadImgurl { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [ProtoMember(11)]
        public string FNikeNmae { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [ProtoMember(12)]
        public string FEmail { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [ProtoMember(13)]
        public string FPhoneNumber { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        [ProtoMember(14)]
        public decimal FBalance { get; set; }

    }


    [MessageType(OutMessageType.LoginRequestMessage)]
    [ProtoContract]
    public class LoginRequestMessage : LoginMessage
    {
        [ProtoMember(2)]
        public string OpenId { get; set; }

        [ProtoMember(3)]
        public string PhoneNumber { get; set; }

        [ProtoMember(4)]
        public string SMSNumber { get; set; }

        [ProtoMember(5)]
        public string AlipayId { get; set; }

        [ProtoMember(6)]
        public string Account { get; set; }

        [ProtoMember(7)]
        public string Password { get; set; }

    }

    [MessageType(BasicMessageType.Ignore)]
    public class LoginMessage : IMessage
    {
        [ProtoMember(1)]
        public LoginType LoginType { get; set; }
    }


    [MessageType(OutMessageType.LogoutRequestMessage)]
    [ProtoContract]
    public class LogoutRequestMessage : IMessage
    {
        [ProtoMember(1)]
        public string AccountId { get; set; }

        [ProtoMember(2)]
        public string SessionKey { get; set; }
    }
}
