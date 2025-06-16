using UnityEngine;

public class PlayerGrounded : MonoBehaviour
{
    private const float SPHERE_RADIUS = 0.5f;
    private const float OFFSET = 0.05f;

    [SerializeField]
    private PlayerFacade _playerFacade;

    [SerializeField]
    private LayerMask _collisionMask;

    public bool IsGrounded { get; private set; }

    private void Reset()
    {
        _playerFacade = GetComponent<PlayerFacade>();
    }

    private void Awake()
    {
        ChangeIsGrounded(CheckIsGrounded());
    }

    private bool CheckIsGrounded()
    {
        var groundCheckDistance = _playerFacade.CapsuleCollider.height / 2f - SPHERE_RADIUS;

        return Physics.SphereCast(transform.position, SPHERE_RADIUS + OFFSET, -transform.up, out _,
                                  groundCheckDistance, _collisionMask);
    }

    public void ChangeIsGrounded(bool isGrounded)
    {
        IsGrounded = isGrounded;
    }
}
