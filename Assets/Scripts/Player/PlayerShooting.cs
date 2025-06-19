using Player.Powerups;
using Shooting;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField]
        private Gun _primaryGun;

        [SerializeField]
        private Gun _secondaryGun;

        private Gun _currentGun;
        
        private IAttackInput _attackInput;
        private IPowerupsProvider _powerupsProvider;

        [Inject]
        private void Construct(IAttackInput attackInput, IPowerupsProvider powerupsProvider)
        {
            _attackInput = attackInput;
            _powerupsProvider = powerupsProvider;
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
            var damageMultiplier = _powerupsProvider.FindSumOfMultipliersOf(EPlayerStatistic.Damage);

            if (_attackInput.IsAttackPressed)
            {
                TrySwitchGuns(_primaryGun);
                _primaryGun.Shoot(damageMultiplier);
            }
            else if (_attackInput.IsAlternativeAttackPressed)
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
