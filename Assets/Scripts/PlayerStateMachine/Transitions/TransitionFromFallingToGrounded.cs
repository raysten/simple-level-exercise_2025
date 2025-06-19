using Player;
using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public class TransitionFromFallingToGrounded : PlayerStateTransition
    {
        private IGroundedStatus _groundedStatus;
        protected override EPlayerState From => EPlayerState.Falling;
        
        public TransitionFromFallingToGrounded(IGroundedStatus groundedStatus)
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
                canChange = _groundedStatus.IsGrounded;

                if (canChange)
                {
                    newState = stateFactory.Create<PlayerGroundedState>();
                }
            }

            return (canChange, newState);
        }
    }
}
