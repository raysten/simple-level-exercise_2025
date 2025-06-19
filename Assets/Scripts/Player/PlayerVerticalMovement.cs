using System;
using Player.Powerups;
using UnityEngine;

namespace Player
{
    public class PlayerVerticalMovement : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -14f;

        [SerializeField]
        private float _jumpForce = 7f;
        
        [SerializeField]
        private PowerupsController _powerupsController;

        private float _velocity;

        public Vector3 VerticalMovementDelta => Vector3.up * _velocity * Time.fixedDeltaTime;

        private void Reset()
        {
            _powerupsController = GetComponent<PowerupsController>();
        }

        public void ApplyGravity()
        {
            _velocity = Mathf.Max(_gravity, _velocity + _gravity * Time.deltaTime);
        }

        public void ResetToDefaultGravity()
        {
            _velocity = _gravity;
        }

        public void Jump()
        {
            var powerupsMultiplier = _powerupsController.FindSumOfMultipliersOf(EPlayerStatistic.Jump);
            ChangeVelocity(_jumpForce * powerupsMultiplier);
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