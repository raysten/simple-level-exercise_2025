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
            var horizontalMovementDelta = CalculateHorizontalMovement();
            var verticalMovementDelta = _playerFacade.PlayerVerticalMovement.VerticalMovement;
            
            _playerFacade.PlayerMovement.Move(horizontalMovementDelta, verticalMovementDelta);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var horizontalInput = _playerFacade.PlayerInput.HorizontalInput;
            var horizontalMoveInput = new Vector3(horizontalInput.x, 0, horizontalInput.y).normalized;
            var speed = _playerFacade.PlayerSettings.HorizontalMoveSpeed;
            // @todo: do these calculations in PlayerMovement
            var horizontalMovementDelta = _playerFacade.transform.TransformDirection(horizontalMoveInput) * (speed * Time.fixedDeltaTime);
            
            return horizontalMovementDelta;
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
