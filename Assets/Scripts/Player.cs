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

    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }

    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
    }

    private void FixedUpdate()
    {
        var movement = _moveInputAction.ReadValue<Vector2>() * (Time.fixedDeltaTime * _movementSpeed);
        var movementDelta = transform.forward * movement.y + transform.right * movement.x;

        _rigidbody.MovePosition(_rigidbody.position + movementDelta);
    }

    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}
