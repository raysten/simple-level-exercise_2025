using System;
using UnityEngine;

namespace Framework
{
    public class Updater : MonoBehaviour, IUpdateProvider
    {
        public event Action OnFixedUpdate = EventUtility.Empty;
        public event Action OnUpdate = EventUtility.Empty;

        private void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }

        private void Update()
        {
            OnUpdate.Invoke();
        }

        private void OnDestroy()
        {
            OnFixedUpdate = null;
            OnUpdate = null;
        }
    }
}
