using Collisions;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : IMove
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly ICollisionHandler _collisionHandler;

        public PlayerMovement(
            Transform transform, Rigidbody rigidbody, ICollisionHandler collisionHandler)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _collisionHandler = collisionHandler;
        }

        public void Move(Vector3 horizontalMovement, Vector3 verticalMovement)
        {
            var horizontalMovementWithCollisions = CalculateHorizontalMovement(horizontalMovement);
            var movementVectorWithCollisions = ApplyVerticalMovement(verticalMovement, horizontalMovementWithCollisions);
        
            if (movementVectorWithCollisions != Vector3.zero)
            {
                _rigidbody.MovePosition(_rigidbody.position + movementVectorWithCollisions);
            }
        }

        private Vector3 CalculateHorizontalMovement(Vector3 horizontalMovement)
        {
            horizontalMovement = _collisionHandler.CalculateMovement(horizontalMovement, _transform.position);

            return horizontalMovement;
        }

        private Vector3 ApplyVerticalMovement(Vector3 verticalMovement, Vector3 horizontalMovement)
        {
            var currentPosition = _transform.position + horizontalMovement;
            return _collisionHandler.CalculateMovement(verticalMovement, currentPosition, true);
        }
    }
}
