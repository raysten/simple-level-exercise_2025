using System;

namespace Framework
{
    public interface IGameInitializer
    {
        event Action OnGameInitialized;
        event Action OnGameDeinitialized;
    }
}
