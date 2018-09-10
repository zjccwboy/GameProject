using System;
using System.Threading.Tasks;
using H6Game.Entities;

namespace H6Game.Account.Model.Login
{
    public abstract class BaseLogin<TLoginInfo1> : ILogin<TLoginInfo1>, ILogin where TLoginInfo1 : BaseLoginInfo
    {
        public abstract bool StartLogin(TLoginInfo1 longinInfo);

        public Task<TAccount> GetAccountInfo<TLoginInfo>(TLoginInfo longinInfo) where TLoginInfo : BaseLoginInfo
        {
            return GetAccountInfo(longinInfo);
        }

        public bool StartLogin<TLoginInfo>(TLoginInfo longinInfo) where TLoginInfo : BaseLoginInfo
        {
            return StartLogin(longinInfo);
        }
    }
}
