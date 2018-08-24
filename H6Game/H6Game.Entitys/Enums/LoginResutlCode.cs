using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Entitys.Enums
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
        /// 微信OpenId错误
        /// </summary>
        OpenIdError,

        /// <summary>
        /// 手机号码错误
        /// </summary>
        PhoneNumError,

        /// <summary>
        /// 手机号码不存在
        /// </summary>
        PhoneNumNotExist,

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
