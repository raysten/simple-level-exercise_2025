using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    
    [SerializeField]
    private CapsuleCollider _capsuleCollider;
    
    [SerializeField]
    private PlayerGroundCheck _playerGroundCheck;

    public Rigidbody Rigidbody => _rigidbody;

    public CapsuleCollider CapsuleCollider => _capsuleCollider;
    
    public PlayerGroundCheck PlayerGroundCheck => _playerGroundCheck;
}