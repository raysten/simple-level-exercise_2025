using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFacade : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [FormerlySerializedAs("_playerGroundCheck"),SerializeField]
    private PlayerGrounded _playerGrounded;

    [SerializeField]
    private PlayerInput _playerInput;

    [SerializeField]
    private PlayerVerticalMovement _playerVerticalMovement;

    public Rigidbody Rigidbody => _rigidbody;

    public CapsuleCollider CapsuleCollider => _capsuleCollider;

    public PlayerGrounded PlayerGrounded => _playerGrounded;

    public PlayerInput PlayerInput => _playerInput;

    public PlayerVerticalMovement PlayerVerticalMovement => _playerVerticalMovement;

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _playerGrounded = GetComponent<PlayerGrounded>();
        _playerInput = GetComponent<PlayerInput>();
        _playerVerticalMovement = GetComponent<PlayerVerticalMovement>();
    }
}
