using Player.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerHorizontalSpeed : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput _playerInput;
        
        [SerializeField]
        private PowerupsController _powerupsController;

        [SerializeField]
        private PlayerSettings _playerSettings;

        private void Reset()
        {
            _playerInput = GetComponent<PlayerInput>();
            _powerupsController = GetComponent<PowerupsController>();
            _playerSettings = GetComponent<PlayerSettings>();
            
        }

        public float CalculateGroundedSpeed()
        {
            var isAccelerate = _playerInput.IsSprintHeld;
            var speed = isAccelerate ? _playerSettings.SprintSpeed : _playerSettings.HorizontalMoveSpeed;

            var powerupsMultiplier = _powerupsController.FindSumOfMultipliersOf(EPlayerStatistic.HorizontalSpeed);
            
            return speed * powerupsMultiplier;
        }

        public float CalculateSpeedWhenFalling()
        {
            var powerupsMultiplier = _powerupsController.FindSumOfMultipliersOf(EPlayerStatistic.HorizontalSpeed);
            
            return _playerSettings.HorizontalSpeedWhenFalling * powerupsMultiplier;
        }
    }
}
