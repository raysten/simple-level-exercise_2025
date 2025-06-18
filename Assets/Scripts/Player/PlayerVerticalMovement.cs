using UnityEngine;

// @todo: refactor
namespace Player
{
    public class PlayerVerticalMovement : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -14f;

        [SerializeField]
        private float _jumpForce = 6f;

        private bool _isGravityActive;
        private float _velocity;

        public Vector3 VerticalMovement => Vector3.up * _velocity * Time.fixedDeltaTime;
    
        private void FixedUpdate()
        {
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (_isGravityActive)
            {
                _velocity = Mathf.Max(_velocity + _gravity * Time.deltaTime);
            }
        }

        public void ActivateGravity()
        {
            _isGravityActive = true;
        }

        public void DeactivateGravity()
        {
            _velocity = 0f;
            _isGravityActive = false;
        }

        public void Jump()
        {
            AddVelocity(_jumpForce);
        }

        public void AddVelocity(float velocity)
        {
            _velocity += velocity;
        }
    }
}