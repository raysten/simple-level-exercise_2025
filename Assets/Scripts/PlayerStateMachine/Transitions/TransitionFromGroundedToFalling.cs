using Player;
using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public class TransitionFromGroundedToFalling : PlayerStateTransition
    {
        private readonly IGroundedStatus _groundedStatus;
        protected sealed override EPlayerState From => EPlayerState.Grounded;

        public TransitionFromGroundedToFalling(IGroundedStatus groundedStatus, PlayerStateFactory stateFactory)
            : base(stateFactory)
        {
            _groundedStatus = groundedStatus;
        }

        public sealed override (bool canChange, PlayerStateBase newState) CanChangeState(PlayerStateBase currentState)
        {
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = _groundedStatus.IsGrounded == false;

                if (canChange)
                {
                    newState = _stateFactory.Create<PlayerFallingState>();
                }
            }

            return (canChange, newState);
        }
    }
}
