
using H6Game.Entitys;
using H6Game.Message;

namespace H6Game.Base
{
    public interface IPalyer : ICompoentPlayer
    {
        /// <summary>
        /// 账号类型
        /// </summary>
        AccountType AType { get; set; }

        /// <summary>
        /// 房间类型
        /// </summary>
        RoomType RType { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        SceneType SType { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        int AccountId { get; set; }

        /// <summary>
        /// 用于检测会话安全的Key
        /// </summary>
        string NetSessionKey { get; set; }

        /// <summary>
        /// 登陆
        /// </summary>
        void Login();

        /// <summary>
        /// 下线
        /// </summary>
        void Logout();

        /// <summary>
        /// 充值
        /// </summary>
        void Recharge(decimal amt);

        /// <summary>
        /// 下注
        /// </summary>
        /// <param name="god"></param>
        void Bet(decimal god);

        /// <summary>
        /// 说话
        /// </summary>
        /// <param name="content"></param>
        void Talk(string content);

        /// <summary>
        /// 进入房间
        /// </summary>
        /// <param name="room"></param>
        void EnterRoom(BaseRommComponent room);

        /// <summary>
        /// 离开房间
        /// </summary>
        /// <param name="roomId"></param>
        void GoAway(BaseRommComponent room);
    }

    public interface ICompoentPlayer
    {

    }
}
