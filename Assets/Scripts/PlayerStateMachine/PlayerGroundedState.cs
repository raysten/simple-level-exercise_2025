using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerGroundedState : PlayerStateBase
    {
        private const float GROUND_CHECK_VERTICAL_VELOCITY = 0.1f;
        
        public PlayerGroundedState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override EPlayerState State => EPlayerState.Grounded;

        public override void StateEntered()
        {
            Debug.LogError("PlayerGroundedState: Entered");
            _playerFacade.PlayerVerticalMovement.DeactivateGravity();
        }

        public override void FixedUpdateState()
        {
            var horizontalMovement = CalculateHorizontalMovement();
            var verticalMovement = _playerFacade.PlayerVerticalMovement.VerticalMovement;
            
            _playerFacade.PlayerMovement.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var horizontalInput = _playerFacade.PlayerInput.HorizontalInput;
            var speed = _playerFacade.PlayerSettings.HorizontalMoveSpeed;
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        public override void UpdateState()
        {
            if (_playerFacade.PlayerInput.IsJumpPressed)
            {
                _playerFacade.PlayerVerticalMovement.Jump();
            }
        }

        public override void StateExited()
        {
            Debug.LogError("PlayerGroundedState: Exited");
        }
    }
}
