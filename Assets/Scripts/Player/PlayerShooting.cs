using Shooting;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput _playerInput;

        [SerializeField]
        private Gun _primaryGun;

        private void Reset()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (_playerInput.IsAttackPressed)
            {
                _primaryGun.Shoot();
            }
            else if (_playerInput.IsAlternativeAttackPressed)
            {
                _primaryGun.Shoot(); // @todo:
            }
        }
    }
}
