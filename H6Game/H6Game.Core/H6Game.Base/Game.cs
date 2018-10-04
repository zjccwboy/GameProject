﻿using System;

namespace H6Game.Base
{
    public class Game
    {
        public static Scene Scene { get; }
        public static EventManager Event { get;}

        static Game()
        {
            ObjectPool.Load();
            ComponentPool.Load();
            MessageSubscriberPool.Load();

            Scene = new Scene();
            Event = new EventManager();
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