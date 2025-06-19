using UnityEngine;

namespace Collisions
{
    public interface ICollisionHandler
    {
        /// <summary>
        /// Collide and slide algorithm
        /// </summary>
        Vector3 CalculateMovement(
            Vector3 movementDelta, Vector3 currentPosition, bool isVerticalMovement = false);
    }
}
