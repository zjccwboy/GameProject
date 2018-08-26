using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneManager Scene { get; }
        public static EventManager Event { get;}

        static Game()
        {
            ComponentPool.Load();
            HandlerMsgPool.Load();

            Scene = new SceneManager();
            Event = new EventManager();

            Scene.AddComponent<NetConfigComponent>();

            MongoDBManager.Instance.Init();
        }

        public static void Update()
        {
            try
            {
                Event.Update();
            }
            catch(Exception e)
            {
                LogRecord.Log(LogLevel.Fatal, "Game/Update", e.ToString());
            }
        }
    }
}
