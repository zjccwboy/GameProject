using H6Game.Base;

namespace H6Game.TestKcpServerBenckmark
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Scene.AddComponent<InnerComponent>();
            while (true)
            {
                Game.Update();
            }
        }
    }
}
