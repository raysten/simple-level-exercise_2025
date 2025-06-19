using System;
using UnityEngine;

namespace Framework
{
    public class GameInitializer : MonoBehaviour, IGameInitializer
    {
        public event Action OnGameInitialized = EventUtility.Empty;
        public event Action OnGameDeinitialized = EventUtility.Empty;

        private void Awake()
        {
            OnGameInitialized.Invoke();
        }

        private void OnDestroy()
        {
            OnGameDeinitialized.Invoke();
        }
    }
}
