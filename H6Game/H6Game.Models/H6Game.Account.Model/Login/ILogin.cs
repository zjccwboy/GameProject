using H6Game.Entitys;
using System.Threading.Tasks;

namespace H6Game.Account.Model.Login
{
    public interface ILogin<TLoginInfo>  where TLoginInfo : BaseLoginInfo
    {
        bool StartLogin(TLoginInfo longinInfo);
    }

    public interface ILogin
    {
        bool StartLogin<TLoginInfo>(TLoginInfo longinInfo) where TLoginInfo : BaseLoginInfo;
    }

}
