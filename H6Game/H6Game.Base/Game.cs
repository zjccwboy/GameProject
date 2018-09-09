using H6Game.Entitys.Enums;
using System;
using System.Threading;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneManager Scene { get; }
        public static EventManager Event { get;}
        public static ActorManager Actor { get; }

        private static bool IsInitialized;

        static Game()
        {
            ComponentPool.Load();
            HandlerMsgPool.Load();

            Scene = new SceneManager();
            Event = new EventManager();
            Actor = new ActorManager();

            Scene.AddComponent<NetConfigComponent>();
        }

        public static void Init()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;

#if SERVER
            MongoDBManager.Init();
            Game.Scene.AddComponent<InnerComponent>();
            Game.Scene.AddComponent<ActorComponent>().ActorType = ActorType.Player;
            Game.Scene.AddComponent<ActorComponent>().ActorType = ActorType.Room;
            Game.Scene.AddComponent<ActorComponent>().ActorType = ActorType.Game;
#endif
            for (var i = 0; i < 2000; i++)
            {
                Update();
            }
        }

        public static void Update()
        {
            try
            {
                Event.Update();
            }
            catch(Exception e)
            {
                Log.Logger.Fatal("未捕获的异常", e);
            }
        }
    }
}
