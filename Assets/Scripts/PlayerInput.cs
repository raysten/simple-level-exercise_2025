using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER_MAP_ID = "Player";

    [SerializeField]
    private InputActionAsset _input;
    
    private InputAction _moveInputAction;
    private Vector2 _horizontalInput;
    private InputAction _lookInputAction;
    private Vector2 _mouseInput;
    private InputAction _jumpInputAction;

    public Vector2 HorizontalInput => _horizontalInput;
    public Vector2 MouseInput => _mouseInput;
    public bool IsJumpPressed { get; private set; }
    
    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }
    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
        _lookInputAction = InputSystem.actions.FindAction("Look");
        _jumpInputAction = InputSystem.actions.FindAction("Jump");
    }
    
    private void Update()
    {
        _horizontalInput = _moveInputAction.ReadValue<Vector2>();
        _mouseInput = _lookInputAction.ReadValue<Vector2>();
        IsJumpPressed = _jumpInputAction.triggered;
    }
    
    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}