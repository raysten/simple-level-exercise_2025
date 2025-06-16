using UnityEngine;

public class CollisionHandler
{
    private const float SKIN_WIDTH = 0.05f;
    private const int MAX_ITERATIONS = 10;
    private const float MIN_MOVEMENT_DELTA = 0.001f;
    private const float MAX_SLOPE_ANGLE = 40f;

    private readonly CapsuleCollider _capsuleCollider;
    private readonly Transform _transform;
    private readonly LayerMask _collisionMask;
    private PlayerGrounded _playerGrounded;

    public CollisionHandler(
        Transform transform, CapsuleCollider capsuleCollider, LayerMask collisionMask, PlayerGrounded playerGrounded)
    {
        _collisionMask = collisionMask;
        _transform = transform;
        _capsuleCollider = capsuleCollider;
        _playerGrounded = playerGrounded;
    }

    public Vector3 CalculateMovementWithCollideAndSlide(Vector3 movementDelta, bool isVerticalMovement = false)
    {
        var currentPosition = _transform.position;

        for (var i = 0; i < MAX_ITERATIONS && movementDelta.magnitude >= MIN_MOVEMENT_DELTA; i++)
        {
            if (CapsuleCast(movementDelta, out var hit))
            {
                var distanceToCollision = hit.distance - SKIN_WIDTH;

                if (distanceToCollision > SKIN_WIDTH)
                {
                    currentPosition = MoveUpToWall(distanceToCollision);
                }

                if (isVerticalMovement)
                {
                    if (ShouldStandOnSlope(hit))
                    {
                        _playerGrounded.ChangeIsGrounded(true);
                        break;
                    }
                    else
                    {
                        _playerGrounded.ChangeIsGrounded(false);
                    }
                }
                
                movementDelta = FindRemainingMovementDeltaAlongWall(hit);
            }
            else
            {
                currentPosition += movementDelta;

                if (isVerticalMovement)
                {
                    _playerGrounded.ChangeIsGrounded(false);
                }
                
                break;
            }
        }

        return currentPosition - _transform.position;

        Vector3 MoveUpToWall(float distanceToCollision)
        {
            currentPosition += movementDelta.normalized * distanceToCollision;
            return currentPosition;
        }

        Vector3 FindRemainingMovementDeltaAlongWall(RaycastHit hit)
        {
            return Vector3.ProjectOnPlane(movementDelta, hit.normal);
        }
    }

    private bool CapsuleCast(Vector3 direction, out RaycastHit hit)
    {
        var radius = _capsuleCollider.radius;
        var height = _capsuleCollider.height;
        var up = _transform.up;

        var center = _transform.TransformPoint(_capsuleCollider.center);
        var point1 = center + up * (height / 2f - radius);
        var point2 = center - up * (height / 2f - radius);

        return Physics.CapsuleCast(point1, point2, radius, direction.normalized, out hit, 
                                   direction.magnitude + SKIN_WIDTH, _collisionMask,
                                   QueryTriggerInteraction.Ignore);
    }

    private bool ShouldStandOnSlope(RaycastHit hit)
    {
        var angle = Vector3.Angle(Vector3.up, hit.normal);
        Debug.LogError(angle);

        return angle <= MAX_SLOPE_ANGLE;
    }
}
