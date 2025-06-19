using Player;
using PlayerStateMachine.States;
using UnityEngine;

namespace PlayerStateMachine.Transitions
{
    public class TransitionFromGroundedToFalling : PlayerStateTransition
    {
        private IGroundedStatus _groundedStatus;
        protected override EPlayerState From => EPlayerState.Grounded;

        public TransitionFromGroundedToFalling(IGroundedStatus groundedStatus)
        {
            _groundedStatus = groundedStatus;
        }

        public override (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState, PlayerStateFactory stateFactory)
        {
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = _groundedStatus.IsGrounded == false;

                if (canChange)
                {
                    newState = stateFactory.Create<PlayerFallingState>();
                }
            }

            return (canChange, newState);
        }
    }
}
