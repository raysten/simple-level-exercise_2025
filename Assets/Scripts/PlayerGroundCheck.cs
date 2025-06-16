using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private const float SPHERE_RADIUS = 0.5f;
    private const float OFFSET = 0.05f;
    
    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private LayerMask _collisionMask;

    public bool IsGrounded()
    {
        var groundCheckDistance = _capsuleCollider.height / 2f - SPHERE_RADIUS;
        
        return Physics.SphereCast(transform.position, 
                                  SPHERE_RADIUS + OFFSET, 
                                  -transform.up,
                                  out _,
                                  groundCheckDistance, 
                                  _collisionMask);
    }
}
