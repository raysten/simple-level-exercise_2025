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
    private Vector2 _movementInput;
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
        _movementInput = _moveInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var forwardDelta = _movementInput.y;
        var rightDelta = _movementInput.x;
        
        var movementDelta = (transform.forward * forwardDelta + transform.right * rightDelta).normalized;
        
        Debug.DrawRay(transform.position, movementDelta, Color.blue, 1f);
        
        if (_movementInput.sqrMagnitude > 0f)
        {
            // Debug.LogError($"moveDelta: {movementDelta.magnitude}, collisionDelta: {_collisionMovement.normalized.magnitude}");
            Debug.DrawRay(transform.position, _collisionMovement.normalized, Color.red, 1f);
            movementDelta += _collisionMovement.normalized;
        }
        
        Debug.DrawRay(transform.position, movementDelta, Color.black, 1f);

        _rigidbody.MovePosition(_rigidbody.position + movementDelta * (_movementSpeed * Time.fixedDeltaTime));
        
        // _collisionMovement = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionMovement = Vector3.zero;
        
        Debug.LogError($"contacts count: {collision.contacts.Length}");
        foreach (var contactPoint in collision.contacts)
        {
            _collisionMovement += new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(contactPoint.point.x, 0f, contactPoint.point.z);
            
            // if normal >= 0.9f
            // _collisionMovement += new Vector3(contactPoint.normal.x, 0f, contactPoint.normal.z);
        }

        // var contactPoint = collision.contacts[0];
        // _collisionMovement = new Vector3(transform.position.x, 0f, transform.position.z) - new Vector3(contactPoint.point.x, 0f, contactPoint.point.z);
    }

    private void OnCollisionExit(Collision other)
    {
        _collisionMovement = Vector3.zero;
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
