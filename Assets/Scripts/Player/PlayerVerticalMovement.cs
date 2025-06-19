using DependencyInjection;
using Player.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerVerticalMovement : IVerticalMovement
    {
        private float _velocity;
        
        private readonly IPowerupsProvider _powerupsProvider;
        private readonly IVerticalMovementConfig _verticalMovementConfig;
        
        private float Gravity => _verticalMovementConfig.Gravity;
        private float JumpForce => _verticalMovementConfig.JumpForce;

        public PlayerVerticalMovement(
            IPowerupsProvider powerupsProvider, IVerticalMovementConfig verticalMovementConfig)
        {
            _powerupsProvider = powerupsProvider;
            _verticalMovementConfig = verticalMovementConfig;
        }

        public Vector3 VerticalMovementDelta => Vector3.up * _velocity * Time.fixedDeltaTime;

        public void ApplyGravity()
        {
            _velocity = Mathf.Max(Gravity, _velocity + Gravity * Time.deltaTime);
        }

        public void ResetToDefaultGravity()
        {
            _velocity = Gravity;
        }

        public void Jump()
        {
            var powerupsMultiplier = _powerupsProvider.FindSumOfMultipliersOf(EPlayerStatistic.Jump);
            ChangeVelocity(JumpForce * powerupsMultiplier);
        }

        private void ChangeVelocity(float velocity)
        {
            _velocity = velocity;
        }

        public void ZeroGravity()
        {
            ChangeVelocity(0f);
        }
    }
}