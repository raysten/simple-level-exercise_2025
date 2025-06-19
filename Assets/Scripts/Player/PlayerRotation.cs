using DependencyInjection;
using Framework;
using UnityEngine;

namespace Player
{
    public class PlayerRotation
    {
        private readonly Rigidbody _rigidbody;
        private readonly IMouseInput _mouseInput;
        private readonly IMouseSettings _mouseSettings;

        public PlayerRotation(
            IGameInitializer initializer, IUpdateProvider updateProvider, Rigidbody rigidbody, IMouseInput mouseInput,
            IMouseSettings mouseSettings)
        {
            _rigidbody = rigidbody;
            _mouseInput = mouseInput;
            _mouseSettings = mouseSettings;
            
            initializer.OnGameInitialized += Initialize;

            void Initialize()
            {
                Cursor.lockState = CursorLockMode.Locked;
                
                initializer.OnGameInitialized -= Initialize;
                initializer.OnGameDeinitialized += Deinitialize;

                updateProvider.OnFixedUpdate += FixedUpdate;
            }

            void Deinitialize()
            {
                initializer.OnGameDeinitialized -= Deinitialize;
                
                updateProvider.OnFixedUpdate -= FixedUpdate;
            }
        }

        private void FixedUpdate()
        {
            var mouseSensitivity = _mouseSettings.MouseSensitivity;
            var deltaRotation = Quaternion.Euler(0f, _mouseInput.MouseInput.x * mouseSensitivity * Time.fixedDeltaTime, 0f);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
    }
}
