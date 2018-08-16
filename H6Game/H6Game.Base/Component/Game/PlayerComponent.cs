using H6Game.Entitys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class PlayerComponent : BaseComponent
    {
        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType AType { get; set; }

        /// <summary>
        /// 房间类型
        /// </summary>
        public RoomType RType { get; set; }

        /// <summary>
        /// 场景类型
        /// </summary>
        public SceneType SType { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 用于检测会话安全的Key
        /// </summary>
        public string NetSessionKey { get; set; }

        /// <summary>
        /// 下注
        /// </summary>
        /// <param name="god"></param>
        public void Bet(decimal god)
        {

        }

        /// <summary>
        /// 说话
        /// </summary>
        /// <param name="content"></param>
        public void Talk(string content)
        {

        }
    }
}
