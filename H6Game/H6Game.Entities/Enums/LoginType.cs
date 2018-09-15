
namespace H6Game.Entities.Enums
{
    public enum LoginType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None,
        /// <summary>
        /// 默认是账号密码登陆
        /// </summary>
        Account,

        /// <summary>
        /// 微信登陆
        /// </summary>
        WXLogin,

        /// <summary>
        /// 支付宝登陆
        /// </summary>
        AliPayLogin,

        /// <summary>
        /// 短信登陆
        /// </summary>
        SMSLogin,

    }
}
