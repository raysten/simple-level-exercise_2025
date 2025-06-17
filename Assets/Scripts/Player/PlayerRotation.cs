using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField]
        private PlayerFacade _playerFacade;

        [SerializeField]
        private float _mouseSensitivity = 20f;

        private Vector2 MouseInput => _playerFacade.PlayerInput.MouseInput;
        private Rigidbody Rigidbody => _playerFacade.Rigidbody;

        private void Reset()
        {
            _playerFacade = GetComponent<PlayerFacade>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            var deltaRotation = Quaternion.Euler(0f, MouseInput.x * _mouseSensitivity * Time.fixedDeltaTime, 0f);
            Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
        }
    }
}
