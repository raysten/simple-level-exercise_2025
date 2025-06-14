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
        _movement = _moveInputAction.ReadValue<Vector2>() * _movementSpeed;
    }

    private void FixedUpdate()
    {
        var movementDelta = transform.forward * (_movement.y * Time.fixedDeltaTime) + transform.right * (_movement.x * Time.fixedDeltaTime);

        _rigidbody.MovePosition(_rigidbody.position + movementDelta);
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
