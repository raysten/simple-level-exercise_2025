using UnityEngine;

public class WallCollisions
{
    private const float SKIN_WIDTH = 0.05f;
    private const int MAX_ITERATIONS = 5;
    private const float MIN_MOVEMENT_DELTA = 0.001f;

    private CapsuleCollider _capsuleCollider;
    private Transform _transform;
    private LayerMask _collisionMask;

    public WallCollisions(Transform transform, CapsuleCollider capsuleCollider, LayerMask collisionMask)
    {
        _collisionMask = collisionMask;
        _transform = transform;
        _capsuleCollider = capsuleCollider;
    }

    public Vector3 FindMovePositionWithCollideAndSlide(Vector3 movementDelta)
    {
        var currentPosition = _transform.position;

        for (var i = 0; i < MAX_ITERATIONS && movementDelta.magnitude >= MIN_MOVEMENT_DELTA; i++)
        {
            if (CapsuleCast(currentPosition, movementDelta, out var hit))
            {
                var distanceToCollision = hit.distance - SKIN_WIDTH;
                
                if (distanceToCollision > 0f)
                {
                    currentPosition = MoveUpToWall(distanceToCollision);
                }

                movementDelta = FindRemainingMovementDeltaAlongWall(hit);
            }
            else
            {
                currentPosition += movementDelta;
                break;
            }
        }

        return currentPosition;

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

    private bool CapsuleCast(Vector3 from, Vector3 direction, out RaycastHit hit)
    {
        var radius = _capsuleCollider.radius;
        var height = Mathf.Max(_capsuleCollider.height, radius * 2f);
        var up = _transform.up;

        var center = from + _transform.rotation * _capsuleCollider.center;
        var point1 = center + up * (height / 2f - radius);
        var point2 = center - up * (height / 2f - radius);

        return Physics.CapsuleCast(
                                   point1, point2, radius,
                                   direction.normalized,
                                   out hit,
                                   direction.magnitude + SKIN_WIDTH,
                                   _collisionMask,
                                   QueryTriggerInteraction.Ignore
                                  );
    }
}