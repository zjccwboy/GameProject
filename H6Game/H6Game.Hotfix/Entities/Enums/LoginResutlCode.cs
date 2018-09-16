

namespace H6Game.Hotfix.Enums
{
    public enum LoginResutlCode
    {
        /// <summary>
        /// 未知错误
        /// </summary>
        UnKnown,

        /// <summary>
        /// 登陆成功
        /// </summary>
        Success,

        /// <summary>
        /// 登陆类型错误
        /// </summary>
        LoginTypeError,

        /// <summary>
        /// 微信OpenId错误
        /// </summary>
        OpenIdError,

        /// <summary>
        /// 短信认证码错误
        /// </summary>
        SMSError,

        /// <summary>
        /// 手机号码不存在
        /// </summary>
        PhoneNumNotExist,

        /// <summary>
        /// 未绑定手机号码
        /// </summary>
        UnboundPhone,

        /// <summary>
        /// 支付宝Id错误
        /// </summary>
        AlipayIdError,

        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExist,

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError,

        /// <summary>
        /// 账号被冻结
        /// </summary>
        Freezed,
    }
}
