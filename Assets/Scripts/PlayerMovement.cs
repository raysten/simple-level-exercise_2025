using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    
    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private float _skinWidth = 0.05f;

    [SerializeField]
    private LayerMask _collisionMask;
    
    private IGroundedStatus _groundedStatus;
    private CollisionHandler _collisionHandler;

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Awake()
    {
        _groundedStatus = GetComponent<IGroundedStatus>();
        _collisionHandler = new CollisionHandler(transform, _capsuleCollider, _collisionMask, _groundedStatus);
    }

    public void Move(Vector3 horizontalMovement, Vector3 verticalMovement)
    {
        var horizontalMovementWithCollisions = CalculateHorizontalMovement(horizontalMovement);
        var movementVectorWithCollisions = ApplyVerticalMovement(verticalMovement, horizontalMovementWithCollisions);
        
        if (movementVectorWithCollisions != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + movementVectorWithCollisions);
        }
    }

    private Vector3 CalculateHorizontalMovement(Vector3 horizontalMovement)
    {
        horizontalMovement = transform.TransformDirection(horizontalMovement);
        horizontalMovement = _collisionHandler.CalculateMovementWithCollideAndSlide(horizontalMovement, transform.position);

        return horizontalMovement;
    }

    private Vector3 ApplyVerticalMovement(Vector3 verticalMovement, Vector3 horizontalMovement)
    {
        return _collisionHandler.CalculateMovementWithCollideAndSlide(verticalMovement, transform.position + horizontalMovement, true);
    }
}
