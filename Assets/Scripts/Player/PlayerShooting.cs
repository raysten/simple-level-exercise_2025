using Player.Powerups;
using Shooting;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput _playerInput;

        [SerializeField]
        private PowerupsController _powerupsController;
        
        [SerializeField]
        private Gun _primaryGun;

        [SerializeField]
        private Gun _secondaryGun;

        private Gun _currentGun;

        private void Reset()
        {
            _playerInput = GetComponent<PlayerInput>();
            _powerupsController = GetComponent<PowerupsController>();
        }

        private void Awake()
        {
            _currentGun = _primaryGun;
        }

        private void Update()
        {
            TryShoot();
        }

        private void TryShoot()
        {
            var damageMultiplier = _powerupsController.FindSumOfMultipliersOf(EPlayerStatistic.Damage);

            if (_playerInput.IsAttackPressed)
            {
                TrySwitchGuns(_primaryGun);
                _primaryGun.Shoot(damageMultiplier);
            }
            else if (_playerInput.IsAlternativeAttackPressed)
            {
                TrySwitchGuns(_secondaryGun);
                _secondaryGun.Shoot(damageMultiplier);
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
