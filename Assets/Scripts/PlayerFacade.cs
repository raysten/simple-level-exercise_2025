using UnityEngine;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private PlayerGroundCheck _playerGroundCheck;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private PlayerVerticalMovement _playerVerticalMovement;

    public Rigidbody Rigidbody => _rigidbody;

    public CapsuleCollider CapsuleCollider => _capsuleCollider;

    public PlayerGroundCheck PlayerGroundCheck => _playerGroundCheck;

    public PlayerInput PlayerInput => _playerInput;

    public PlayerVerticalMovement PlayerVerticalMovement => _playerVerticalMovement;

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _playerGroundCheck = GetComponent<PlayerGroundCheck>();
        _playerInput = GetComponent<PlayerInput>();
        _playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
    }
}
