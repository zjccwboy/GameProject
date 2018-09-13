using H6Game.Entities.Enums;
using System;
using System.Threading;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneManager Scene { get; }
        public static EventManager Event { get;}
        public static ActorManager Actor { get; }

        static Game()
        {
            ComponentPool.Load();
            HandlerMsgPool.Load();

            Scene = new SceneManager();
            Event = new EventManager();
            Actor = new ActorManager();

            Scene.AddComponent<NetConfigComponent>();
        }

        public static void Update()
        {
            try
            {
                Event.Update();
                ThreadCallbackContext.Instance.Update();
                Thread.Sleep(1);
            }
            catch(Exception e)
            {
                Log.Logger.Fatal("未捕获的异常", e);
            }
        }
    }
}
