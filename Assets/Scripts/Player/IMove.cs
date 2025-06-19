using UnityEngine;

namespace Player
{
    public interface IMove
    {
        void Move(Vector3 horizontalMovement, Vector3 verticalMovement);
    }
}
