using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerFallingState : PlayerStateBase
    {
        public PlayerFallingState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override EPlayerState State => EPlayerState.Falling;

        public override void StateEntered()
        {
            Debug.LogError("PlayerFallingState: Entered");
            _playerFacade.PlayerVerticalMovement.ActivateGravity();
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
            var speed = _playerFacade.PlayerSettings.HorizontalSpeedWhenFalling;
            var horizontalMovementDelta = _playerFacade.transform.TransformDirection(horizontalMoveInput) * (speed * Time.fixedDeltaTime);
            
            return horizontalMovementDelta;
        }

        public override void UpdateState()
        {
            
        }

        public override void StateExited()
        {
            Debug.LogError("PlayerFallingState: Exited");
        }
    }
}
