using System;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// Very simple initializer which in a real game would work with loading screens, loading and unloading scenes etc
    /// </summary>
    public class GameInitializer : MonoBehaviour, IGameInitializer
    {
        public event Action OnGameInitialized = EventUtility.Empty;
        public event Action OnGameDeinitialized = EventUtility.Empty;

        private void Awake()
        {
            OnGameInitialized.Invoke();
        }

        private void OnApplicationQuit()
        {
            OnGameDeinitialized.Invoke();
        }
    }
}
