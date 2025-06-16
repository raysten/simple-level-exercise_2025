using UnityEngine;

namespace PlayerStateMachine
{
    [CreateAssetMenu(fileName = nameof(TransitionFromGroundedToFalling),
                     menuName = "Player/StateTransitions/" + nameof(TransitionFromGroundedToFalling))]
    public class TransitionFromGroundedToFalling : PlayerStateTransition
    {
        protected override EPlayerState From => EPlayerState.Grounded;

        public override (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState, PlayerFacade playerFacade)
        {
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = playerFacade.PlayerGrounded.IsGrounded == false;

                if (canChange)
                {
                    newState = new PlayerFallingState(playerFacade);
                }
            }

            return (canChange, newState);
        }
    }
}
