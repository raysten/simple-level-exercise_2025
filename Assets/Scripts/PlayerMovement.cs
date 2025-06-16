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
    private float _horizontalSpeed = 10f;

    [SerializeField]
    private float _horizontalSpeedMultiplierWhenFalling = 0.2f;
    
    private CollisionHandler _collisionHandler;

    private PlayerInput PlayerInput => _playerFacade.PlayerInput;

    private void Reset()
    {
        _playerFacade = GetComponent<PlayerFacade>();
    }

    private void Awake()
    {
        _collisionHandler = new CollisionHandler(transform, _playerFacade.CapsuleCollider, _collisionMask);
    }

    private void FixedUpdate()
    {
        var movementVector = Vector3.zero;
        movementVector += CalculateHorizontalMovement();
        movementVector += CalculateVerticalMovement();

        if (movementVector != Vector3.zero)
        {
            _playerFacade.Rigidbody.MovePosition(_playerFacade.Rigidbody.position + movementVector);
        }
    }

    private Vector3 CalculateHorizontalMovement()
    {
        var horizontalMoveInput = new Vector3(PlayerInput.MovementInput.x, 0, PlayerInput.MovementInput.y).normalized;
        var horizontalMovementDelta = transform.TransformDirection(horizontalMoveInput) * (_horizontalSpeed * Time.fixedDeltaTime);

        if (_playerFacade.PlayerVerticalMovement.IsGravityActive)
        {
            horizontalMovementDelta *= _horizontalSpeedMultiplierWhenFalling;
        }

        horizontalMovementDelta = _collisionHandler.CalculateMovementWithCollideAndSlide(horizontalMovementDelta);

        return horizontalMovementDelta;
    }

    private Vector3 CalculateVerticalMovement()
    {
        var verticalMovement = Vector3.zero;

        if (_playerFacade.PlayerVerticalMovement.ShouldApplyVerticalMovement)
        {
            verticalMovement += _playerFacade.PlayerVerticalMovement.VerticalMovement * Time.fixedDeltaTime;
            verticalMovement = _collisionHandler.CalculateMovementWithCollideAndSlide(verticalMovement, true, _playerFacade.PlayerGroundCheck);
        }

        return verticalMovement;
    }
}
