using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private LayerMask _collisionMask;

    public bool IsGrounded()
    {
        var groundCheckDistance = _capsuleCollider.height / 2f;

        return Physics.Raycast(transform.position, -transform.up, groundCheckDistance, _collisionMask);
    }
}
