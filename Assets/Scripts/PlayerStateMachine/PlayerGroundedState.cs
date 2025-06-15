using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerGroundedState : PlayerStateBase
    {
        public PlayerGroundedState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override EPlayerState State => EPlayerState.Grounded;

        public override void StateEntered()
        {
            Debug.LogError("PlayerGroundedState: Entered");
            _playerFacade.PlayerVerticalMovement.DeactivateGravity();
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
