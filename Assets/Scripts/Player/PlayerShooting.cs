using System;
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

        [SerializeField]
        private Gun _secondaryGun;

        private Gun _currentGun;

        private void Reset()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Awake()
        {
            _currentGun = _primaryGun;
        }

        private void Update()
        {
            if (_playerInput.IsAttackPressed)
            {
                TrySwitchGuns(_primaryGun);
                _primaryGun.Shoot();
            }
            else if (_playerInput.IsAlternativeAttackPressed)
            {
                TrySwitchGuns(_secondaryGun);
                _secondaryGun.Shoot();
            }
        }

        private void TrySwitchGuns(Gun chosenGun)
        {
            if (_currentGun != chosenGun)
            {
                _currentGun.gameObject.SetActive(false);
                chosenGun.gameObject.SetActive(true);
                _currentGun = chosenGun;
            }
        }
    }
}
