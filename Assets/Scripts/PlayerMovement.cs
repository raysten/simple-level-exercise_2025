using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerFacade _playerFacade;

    [SerializeField]
    private float _skinWidth = 0.05f;

    [SerializeField]
    private LayerMask _collisionMask;

    [SerializeField]
    private float _movementSpeed = 10f;
    
    private WallCollisions _wallCollisions;

    private PlayerInput PlayerInput => _playerFacade.PlayerInput;

    private void Reset()
    {
        _playerFacade = GetComponent<PlayerFacade>();
    }

    private void Awake()
    {
        _wallCollisions = new WallCollisions(transform, _playerFacade.CapsuleCollider, _collisionMask);
    }

    private void FixedUpdate()
    {
        var moveInput = new Vector3(PlayerInput.MovementInput.x, 0, PlayerInput.MovementInput.y).normalized;
        var movementDelta = transform.TransformDirection(moveInput) * (_movementSpeed * Time.fixedDeltaTime);

        if (movementDelta.magnitude > 0f)
        {
            var movePosition = _wallCollisions.FindMovePositionWithCollideAndSlide(movementDelta);
            _playerFacade.Rigidbody.MovePosition(movePosition);
        }
    }
}
