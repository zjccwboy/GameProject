using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Message
{
    public enum RoomType
    {
        None,
        /// <summary>
        /// 金币房
        /// </summary>
        God,
        /// <summary>
        /// VIP金币房
        /// </summary>
        VIPGod,
        /// <summary>
        /// 房卡房
        /// </summary>
        Card,
        /// <summary>
        /// VIP房卡房
        /// </summary>
        VIPCard,
        /// <summary>
        /// 代理金币
        /// </summary>
        AgentGod,
        /// <summary>
        /// 代理房卡
        /// </summary>
        AgentCard,
    }
}
