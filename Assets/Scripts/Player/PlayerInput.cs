using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
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
        private InputAction _sprintAction;
        private InputAction _attackAction;
        private InputAction _alternativeAttackAction;

        public Vector2 HorizontalInputRaw => _horizontalInput;
        public Vector3 HorizontalInput => new Vector3(_horizontalInput.x, 0f, _horizontalInput.y).normalized;
        public Vector2 MouseInput => _mouseInput;
        public bool IsJumpPressed { get; private set; }
        public bool IsSprintHeld { get; private set; }
        public bool IsAttackPressed { get; private set; }
        public bool IsAlternativeAttackPressed { get; private set; }
    
        private void OnEnable()
        {
            _input.FindActionMap(PLAYER_MAP_ID).Enable();
        }
        private void Awake()
        {
            _moveInputAction = InputSystem.actions.FindAction("Move");
            _lookInputAction = InputSystem.actions.FindAction("Look");
            _jumpInputAction = InputSystem.actions.FindAction("Jump");
            _sprintAction = InputSystem.actions.FindAction("Sprint");
            _attackAction = InputSystem.actions.FindAction("Attack");
            _alternativeAttackAction = InputSystem.actions.FindAction("AlternativeAttack");
        }
    
        private void Update()
        {
            _horizontalInput = _moveInputAction.ReadValue<Vector2>();
            _mouseInput = _lookInputAction.ReadValue<Vector2>();
            IsJumpPressed = _jumpInputAction.triggered;
            IsSprintHeld = _sprintAction.IsPressed();
            IsAttackPressed = _attackAction.triggered;
            IsAlternativeAttackPressed = _alternativeAttackAction.triggered;
        }

        private void OnDisable()
        {
            _input.FindActionMap(PLAYER_MAP_ID).Disable();
        }
    }
}