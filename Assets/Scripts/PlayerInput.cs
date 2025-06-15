using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private const string PLAYER_MAP_ID = "Player";

    [SerializeField]
    private InputActionAsset _input;
    
    private InputAction _moveInputAction;
    private Vector2 _movementInput;
    private InputAction _lookInputAction;
    private Vector2 _mouseInput;

    public Vector2 MovementInput => _movementInput;
    public Vector2 MouseInput => _mouseInput;
    
    private void OnEnable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Enable();
    }
    private void Awake()
    {
        _moveInputAction = InputSystem.actions.FindAction("Move");
        _lookInputAction = InputSystem.actions.FindAction("Look");
    }
    
    private void Update()
    {
        _movementInput = _moveInputAction.ReadValue<Vector2>();
        _mouseInput = _lookInputAction.ReadValue<Vector2>();
    }
    
    private void OnDisable()
    {
        _input.FindActionMap(PLAYER_MAP_ID).Disable();
    }
}