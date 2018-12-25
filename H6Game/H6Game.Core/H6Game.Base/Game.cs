using H6Game.Base.Component;
using H6Game.Base.Logger;
using H6Game.Base.Message;
using System;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneStorage Scene { get; }
        public static EventManager Event { get;}

        static Game()
        {
            LoadObject();
            Scene = new SceneStorage();
            Event = new EventManager();
        }

        public static void Start()
        {
            LoadMessage();
        }

        public static void Update()
        {
            try
            {
                Event.Update();
                ThreadCallbackContext.Instance.Update();
            }
            catch(Exception e)
            {
                Log.Fatal("未捕获的异常", e, LoggerBllType.System);
            }
        }

        private static void LoadObject()
        {
            ObjectTypeStorage.Load();
            ObjectStorage.Load();
        }

        private static void LoadMessage()
        {
            MessageCommandStorage.Load();
            MessageSubscriberStorage.Load();
        }
    }
}
