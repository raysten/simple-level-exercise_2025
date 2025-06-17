using UnityEngine;

namespace Player
{
    public class PlayerGrounded : MonoBehaviour, IGroundedStatus
    {
        public bool IsGrounded { get; private set; }

        public void ChangeIsGrounded(bool isGrounded)
        {
            IsGrounded = isGrounded;
        }
    }
}