﻿using H6Game.Base;
using H6Game.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6Game.Rpository
{
    [SingletCase]
    public sealed class RoomRpository : BaseRpository<TRoom>
    {
        public async Task<TRoom> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}