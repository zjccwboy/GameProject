
namespace H6Game.Base
{
    public enum EventType
    {
        None,
        Awake = 1,
        Start = 1 << 1,
        Update = 1 << 2,
        Dispose = 1 << 3,
    }
}
