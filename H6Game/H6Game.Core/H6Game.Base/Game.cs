using System;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneStorage Scene { get; }
        public static EventManager Event { get;}

        static Game()
        {
            ObjectTypeStorage.Load();
            ObjectStorage.Load();

            Scene = new SceneStorage();
            Event = new EventManager();
        }

        public static void Start()
        {
            MessageCommandStorage.Load();
            MessageSubscriberStorage.Load();
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
    }
}
