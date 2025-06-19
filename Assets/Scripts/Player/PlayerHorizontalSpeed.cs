using DependencyInjection;
using Player.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerHorizontalSpeed : IHorizontalSpeed
    {
        private readonly ISprintInput _sprintInput;
        private readonly IPowerupsProvider _powerupsProvider;
        private readonly IHorizontalSpeedConfig _settings;

        public PlayerHorizontalSpeed(
            ISprintInput sprintInput, IPowerupsProvider powerupsProvider, IHorizontalSpeedConfig horizontalSpeedConfig)
        {
            _sprintInput = sprintInput;
            _powerupsProvider = powerupsProvider;
            _settings = horizontalSpeedConfig;
        }

        public float CalculateGroundedSpeed()
        {
            var isAccelerate = _sprintInput.IsSprintHeld;
            var speed = isAccelerate ? _settings.SprintSpeed : _settings.HorizontalMovementSpeed;

            var powerupsMultiplier = _powerupsProvider.FindSumOfMultipliersOf(EPlayerStatistic.HorizontalSpeed);
            
            return speed * powerupsMultiplier;
        }

        public float CalculateSpeedWhenFalling()
        {
            var powerupsMultiplier = _powerupsProvider.FindSumOfMultipliersOf(EPlayerStatistic.HorizontalSpeed);
            
            return _settings.HorizontalSpeedWhenFalling * powerupsMultiplier;
        }

        public float CalculateFlyingSpeed()
        {
            return CalculateGroundedSpeed();
        }
    }
}
