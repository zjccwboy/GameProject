using System;
using System.Collections.Generic;
using System.Text;

namespace H6Game.Base
{
    public class Game
    {
        public static SceneComponent Scene { get; } = SinglePool.Get<SceneComponent>();
        private static EventComponent Event { get; } = SinglePool.Get<EventComponent>();

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
