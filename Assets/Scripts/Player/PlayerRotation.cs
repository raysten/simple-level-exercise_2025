using Framework;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerRotation
    {
        private readonly Rigidbody _rigidbody;
        private readonly IMouseInput _mouseInput;
        private readonly IRotationSettings _rotationSettings;

        public PlayerRotation(
            IGameInitializer initializer, IUpdateProvider updateProvider, Rigidbody rigidbody, IMouseInput mouseInput,
            IRotationSettings rotationSettings)
        {
            _rigidbody = rigidbody;
            _mouseInput = mouseInput;
            _rotationSettings = rotationSettings;
            
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
            var mouseSensitivity = _rotationSettings.MouseSensitivity;
            var yRotation = _mouseInput.MouseInput.x * mouseSensitivity * Time.fixedDeltaTime;
            var yRotationClamp = _rotationSettings.YAxisRotationClamp;
            var yRotationClamped = Mathf.Clamp(yRotation, -yRotationClamp, yRotationClamp);
            var deltaRotation = Quaternion.Euler(0f, yRotationClamped, 0f);
            
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }
    }
}
