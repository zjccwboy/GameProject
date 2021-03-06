﻿using H6Game.Base;
using System.Linq;
using System.Threading.Tasks;
using H6Game.Hotfix.Entities;
using H6Game.Base.Component;

namespace H6Game.Rpository
{
    [SingleCase]
    public class GameRpository : DBRpository<TGame>
    {
        public async Task<TGame> GetById(string objectId)
        {
            var q = await this.DBContext.FindByIdAsync(objectId);

            if (q.Any())
                return q.FirstOrDefault();

            return null;
        }
    }
}
