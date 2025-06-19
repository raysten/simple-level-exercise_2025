using System;
using Framework;

namespace Player
{
    public class PlayerEventBus : IPlayerEvents
    {
        public event Action<string> OnPlayerStateChanged = EventUtility.Empty;
        
        public void InvokePlayerStateChanged(string state)
        { 
            OnPlayerStateChanged.Invoke(state);
        }
    }
}
