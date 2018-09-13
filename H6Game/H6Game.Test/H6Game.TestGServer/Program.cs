using H6Game.Base;

namespace TestGServer
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
