using H6Game.Entitys.Enums;
using System;

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
        }

        public static void Update()
        {
            try
            {
                Event.Update();
            }
            catch(Exception e)
            {
                LogRecord.Log(LogLevel.Fatal, e);
            }
        }
    }
}
