using System;

namespace Player
{
    public interface IPlayerEvents
    {
        event Action<string> OnPlayerStateChanged;
        void InvokePlayerStateChanged(string state);
    }
}
