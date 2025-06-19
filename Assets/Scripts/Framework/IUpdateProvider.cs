using System;

namespace Framework
{
    public interface IUpdateProvider
    {
        event Action OnFixedUpdate;
        event Action OnUpdate;
    }
}
