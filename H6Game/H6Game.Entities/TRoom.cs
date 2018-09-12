﻿using H6Game.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace H6Game.Entities
{
    public class TRoom : BaseEntity
    {
        /// <summary>
        /// 房间类型
        /// </summary>
        [BsonElement("RT")]
        public RoomType FRoomType { get; set; }

        /// <summary>
        /// 房间游戏类型
        /// </summary>
        [BsonElement("GT")]
        public GameType FGameType { get; set; }
    }
}