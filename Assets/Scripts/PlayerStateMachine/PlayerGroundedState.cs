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
        }

        public override void UpdateState()
        { }

        public override void StateExited()
        {
            Debug.LogError("PlayerGroundedState: Exited");
        }
    }
}
