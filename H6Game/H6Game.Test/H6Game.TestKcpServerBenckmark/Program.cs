using H6Game.Base;

namespace H6Game.TestKcpServerBenckmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<DistributionsComponent>();
            while (true)
            {
                Game.Update();
            }
        }
    }
}
