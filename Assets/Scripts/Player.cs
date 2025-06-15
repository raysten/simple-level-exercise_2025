using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private const string PLAYER_MAP_ID = "Player";

    [SerializeField]
    private InputActionAsset _input;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _movementSpeed = 10f;

    private InputAction _moveInputAction;
    private Vector2 _movement;
    private Vector3 _collisionMovement;

    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }

    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        _movement = _moveInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var forwardDelta = _movement.y;
        var rightDelta = _movement.x;
        
        var movementDelta = transform.forward * forwardDelta + transform.right * rightDelta;
        
        Debug.DrawRay(transform.position, movementDelta, Color.blue);
        
        if (_movement.sqrMagnitude > 0f)
        {
            Debug.DrawRay(transform.position, _collisionMovement, Color.red);
            movementDelta += _collisionMovement.normalized;
        }
        
        Debug.DrawRay(transform.position, movementDelta, Color.green);

        _rigidbody.MovePosition(_rigidbody.position + movementDelta * (_movementSpeed * Time.fixedDeltaTime));
        
        _collisionMovement = Vector2.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contactPoint in collision.contacts)
        {
            _collisionMovement += new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(contactPoint.point.x, 0f, contactPoint.point.z);
        }
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
