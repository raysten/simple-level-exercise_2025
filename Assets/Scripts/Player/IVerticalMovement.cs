using UnityEngine;

namespace Player
{
    public interface IVerticalMovement
    {
        Vector3 VerticalMovementDelta { get; }
        void ApplyGravity();
        void ResetToDefaultGravity();
        void Jump();
        void ZeroGravity();
    }
}
