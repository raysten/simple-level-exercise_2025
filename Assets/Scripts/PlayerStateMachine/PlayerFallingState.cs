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

        public override void UpdateState()
        {
            
        }

        public override void StateExited()
        {
            Debug.LogError("PlayerFallingState: Exited");
        }
    }
}
