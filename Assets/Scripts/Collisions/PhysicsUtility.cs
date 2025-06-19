using UnityEngine;

namespace Collisions
{
    /// <summary>
    /// Such static class is perfectly fine as a utility, it doesn't keep or modify any state just enables code reuse
    /// </summary>
    public static class PhysicsUtility
    {
        public static (Vector3, Vector3) CalculateCapsulePoints(Transform transform, CapsuleCollider capsuleCollider)
        {
            var currentPosition = transform.position;
            
            return CalculateCapsulePoints(transform, capsuleCollider, currentPosition);
        }
        
        public static (Vector3, Vector3) CalculateCapsulePoints(
            Transform transform, CapsuleCollider capsuleCollider, Vector3 currentPosition)
        {
            var radius = capsuleCollider.radius;
            var height = capsuleCollider.height;
            var up = transform.up;

            var centerInWorldSpace = currentPosition + transform.rotation * capsuleCollider.center;
            var point1 = centerInWorldSpace + up * (height / 2f - radius);
            var point2 = centerInWorldSpace - up * (height / 2f - radius);

            return (point1, point2);
        }
    }
}
