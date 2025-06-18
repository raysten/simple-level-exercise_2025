using Player;
using PlayerStateMachine.States;
using UnityEngine;

namespace PlayerStateMachine.Transitions
{
    [CreateAssetMenu(fileName = nameof(TransitionFromFallingToGrounded),
                     menuName = "Player/StateTransitions/" + nameof(TransitionFromFallingToGrounded))]
    public class TransitionFromFallingToGrounded : PlayerStateTransition
    {
        protected override EPlayerState From => EPlayerState.Falling;

        public override (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState, PlayerFacade playerFacade)
        {
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = playerFacade.PlayerGroundCheck.IsGrounded;

                if (canChange)
                {
                    newState = new PlayerGroundedState(playerFacade);
                }
            }

            return (canChange, newState);
        }
    }
}
