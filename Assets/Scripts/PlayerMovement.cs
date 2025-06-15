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

    // @todo: refactor
    private void FixedUpdate()
    {
        var movePosition = _playerFacade.Rigidbody.position;
        
        var horizontalMoveInput = new Vector3(PlayerInput.MovementInput.x, 0, PlayerInput.MovementInput.y).normalized;
        var horizontalMovementDelta = transform.TransformDirection(horizontalMoveInput) * (_movementSpeed * Time.fixedDeltaTime);
        var hasHorizontalMovement = horizontalMovementDelta.magnitude > 0f;

        if (hasHorizontalMovement)
        {
            movePosition = _wallCollisions.FindMovePositionWithCollideAndSlide(horizontalMovementDelta);
        }

        var hasVerticalMovement = _playerFacade.PlayerVerticalMovement.ShouldApplyVerticalMovement;

        if (hasVerticalMovement)
        {
            movePosition += _playerFacade.PlayerVerticalMovement.VerticalMovement * Time.fixedDeltaTime;
        }

        if (hasHorizontalMovement || hasVerticalMovement)
        {
            _playerFacade.Rigidbody.MovePosition(movePosition);
        }
    }
}
