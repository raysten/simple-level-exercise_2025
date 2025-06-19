using Framework;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInput : IHorizontalInput, ISprintInput, IJumpInput, IMouseInput, IAttackInput, IFlyingInput
    {
        private const string PLAYER_MAP_ID = "Player";

        private InputAction _moveInputAction;
        private Vector2 _horizontalInput;
        private InputAction _lookInputAction;
        private Vector2 _mouseInput;
        private InputAction _jumpInputAction;
        private InputAction _sprintAction;
        private InputAction _attackAction;
        private InputAction _alternativeAttackAction;
        private InputAction _flyInputAction;
        private InputAction _flyUpDownInputAction;
        private Vector2 _flyUpDownInput;

        public Vector2 HorizontalInputRaw => _horizontalInput;
        public Vector3 HorizontalInput => new Vector3(_horizontalInput.x, 0f, _horizontalInput.y).normalized;
        public Vector2 MouseInput => _mouseInput;
        public bool IsJumpPressed { get; private set; }
        public bool IsSprintHeld { get; private set; }
        public bool IsAttackPressed { get; private set; }
        public bool IsAlternativeAttackPressed { get; private set; }
        public bool IsFlyPressed { get; private set; }
        public float FlyUpDownInput { get; private set; }
    
        public PlayerInput(IInputAsset inputAsset, IGameInitializer initializer, IUpdateProvider updateProvider)
        {
            initializer.OnGameInitialized += Initialize;

            void Initialize()
            {
                inputAsset.InputAsset.FindActionMap(PLAYER_MAP_ID).Enable();
                AssignInputActions();
                
                initializer.OnGameInitialized -= Initialize;
                initializer.OnGameDeinitialized += Deinitialize;

                updateProvider.OnUpdate += Update;
            }

            void Deinitialize()
            {
                inputAsset.InputAsset.FindActionMap(PLAYER_MAP_ID).Disable();
                
                initializer.OnGameDeinitialized -= Deinitialize;
                
                updateProvider.OnUpdate -= Update;
            }
        }
        
        private void AssignInputActions()
        {
            _moveInputAction = InputSystem.actions.FindAction("Move");
            _lookInputAction = InputSystem.actions.FindAction("Look");
            _jumpInputAction = InputSystem.actions.FindAction("Jump");
            _sprintAction = InputSystem.actions.FindAction("Sprint");
            _attackAction = InputSystem.actions.FindAction("Attack");
            _alternativeAttackAction = InputSystem.actions.FindAction("AlternativeAttack");
            _flyInputAction = InputSystem.actions.FindAction("Fly");
            _flyUpDownInputAction = InputSystem.actions.FindAction("FlyUpDown");
        }
    
        private void Update()
        {
            _horizontalInput = _moveInputAction.ReadValue<Vector2>();
            _mouseInput = _lookInputAction.ReadValue<Vector2>();
            IsJumpPressed = _jumpInputAction.triggered;
            IsSprintHeld = _sprintAction.IsPressed();
            IsAttackPressed = _attackAction.triggered;
            IsAlternativeAttackPressed = _alternativeAttackAction.triggered;
            IsFlyPressed = _flyInputAction.triggered;;
            FlyUpDownInput = _flyUpDownInputAction.ReadValue<float>();
        }
    }
}