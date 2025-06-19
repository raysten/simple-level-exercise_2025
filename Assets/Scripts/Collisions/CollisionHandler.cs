using UnityEngine;

namespace Collisions
{
    public class CollisionHandler : ICollisionHandler
    {
        private const float SKIN_WIDTH = 0.02f;
        private const int MAX_ITERATIONS = 10;
        private const float MIN_MOVEMENT_DELTA = 0.001f;
        private const float MAX_SLOPE_ANGLE = 40f;

        private readonly CapsuleCollider _capsuleCollider;
        private readonly Transform _transform;
        private readonly LayerMask _collisionMask;

        public CollisionHandler(
            Transform transform, CapsuleCollider capsuleCollider, LayerMask collisionMask)
        {
            _collisionMask = collisionMask;
            _transform = transform;
            _capsuleCollider = capsuleCollider;
        }

        /// <summary>
        /// Collide and slide algorithm
        /// </summary>
        public Vector3 CalculateMovement(
            Vector3 movementDelta, Vector3 currentPosition, bool isVerticalMovement = false)
        {
            for (var i = 0; i < MAX_ITERATIONS && movementDelta.magnitude >= MIN_MOVEMENT_DELTA; i++)
            {
                if (DetectCollisionFrom(currentPosition, movementDelta, out var hit))
                {
                    var distanceToCollision = hit.distance - SKIN_WIDTH;

                    if (distanceToCollision > SKIN_WIDTH)
                    {
                        currentPosition = MoveUpToCollision(distanceToCollision);
                    }

                    if (isVerticalMovement)
                    {
                        if (IsGroundOrSmallSlope(hit))
                        {
                            break;
                        }
                    }
                
                    movementDelta = FindRemainingMovementDeltaAlongCollisionSurface(hit);
                }
                else
                {
                    currentPosition += movementDelta;
                
                    break;
                }
            }

            return currentPosition - _transform.position;

            Vector3 MoveUpToCollision(float distanceToCollision)
            {
                currentPosition += movementDelta.normalized * distanceToCollision;
                return currentPosition;
            }

            Vector3 FindRemainingMovementDeltaAlongCollisionSurface(RaycastHit hit)
            {
                return Vector3.ProjectOnPlane(movementDelta, hit.normal);
            }
        }

        private bool DetectCollisionFrom(Vector3 currentPosition, Vector3 direction, out RaycastHit hit)
        {
            var radius = _capsuleCollider.radius;
            var (point1, point2) = PhysicsUtility.CalculateCapsulePoints(_transform, _capsuleCollider, currentPosition);
        
            return Physics.CapsuleCast(point1, point2, radius, direction.normalized, out hit, 
                                       direction.magnitude + SKIN_WIDTH, _collisionMask,
                                       QueryTriggerInteraction.Ignore);
        }

        private bool IsGroundOrSmallSlope(RaycastHit hit)
        {
            var angle = Vector3.Angle(Vector3.up, hit.normal);

            return angle <= MAX_SLOPE_ANGLE;
        }
    }
}
