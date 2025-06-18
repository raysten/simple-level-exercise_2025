using UnityEngine;

namespace Player
{
    public class PlayerVerticalMovement : MonoBehaviour
    {
        [SerializeField]
        private float _gravity = -14f;

        [SerializeField]
        private float _jumpForce = 6f;

        private float _velocity;

        public Vector3 VerticalMovement => Vector3.up * _velocity * Time.fixedDeltaTime;

        public void ApplyGravity()
        {
            _velocity = Mathf.Max(_velocity + _gravity * Time.deltaTime);
        }

        public void ResetGravity()
        {
            _velocity = 0f;
        }

        public void Jump()
        {
            AddVelocity(_jumpForce);
        }

        private void AddVelocity(float velocity)
        {
            _velocity += velocity;
        }
    }
}