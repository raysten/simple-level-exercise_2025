using Player;
using UnityEngine;

namespace PlayerStateMachine.States
{
    public class PlayerFallingState : PlayerStateBase
    {
        public override EPlayerState State => EPlayerState.Falling;
        
        public PlayerFallingState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override void StateEntered()
        {
            Debug.LogError("PlayerFallingState: Entered");
            _playerFacade.PlayerVerticalMovement.ActivateGravity();
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
            var speed = _playerFacade.PlayerSettings.HorizontalSpeedWhenFalling;
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
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
