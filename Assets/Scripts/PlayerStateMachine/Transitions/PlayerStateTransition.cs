using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public abstract class PlayerStateTransition
    {
        protected abstract EPlayerState From { get; }

        public abstract (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState, PlayerStateFactory stateFactory);
    }
}
