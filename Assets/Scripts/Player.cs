using System;
using System.Collections.Generic;
using System.Linq;
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
    private CapsuleCollider _capsuleCollider;

    [SerializeField]
    private float _skinWidth = 0.05f;
    
    [SerializeField]
    private LayerMask _collisionMask;

    [SerializeField]
    private float _movementSpeed = 10f;

    private InputAction _moveInputAction;
    private Vector2 _movementInput;
    private WallCollisions _wallCollisions;

    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }

    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
        _wallCollisions = new WallCollisions(transform, _capsuleCollider, _collisionMask);
    }

    private void Update()
    {
        _movementInput = _moveInputAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        var moveInput = new Vector3(_movementInput.x, 0, _movementInput.y).normalized;
        var movementDelta = transform.TransformDirection(moveInput) * (_movementSpeed * Time.fixedDeltaTime);
        
        if (movementDelta.magnitude > 0f)
        {
            var movePosition = _wallCollisions.FindMovePositionWithCollideAndSlide(movementDelta);
            _rigidbody.MovePosition(movePosition);
        }
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
