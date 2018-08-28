using System.Threading;
using H6Game.Account.Model;
using H6Game.Base;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Init();
            AccountManager.Instance.Init();


            while (true)
            {
                Game.Update();
                Thread.Sleep(1);
            }
        }
    }
}
