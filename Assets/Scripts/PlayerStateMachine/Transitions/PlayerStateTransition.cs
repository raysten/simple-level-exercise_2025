using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public abstract class PlayerStateTransition
    {
        protected readonly PlayerStateFactory _stateFactory;

        protected PlayerStateTransition(PlayerStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        protected abstract EPlayerState From { get; }

        public abstract (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState);
    }
}
