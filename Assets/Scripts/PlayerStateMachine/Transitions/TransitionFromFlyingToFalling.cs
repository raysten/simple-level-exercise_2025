using Player;
using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public class TransitionFromFlyingToFalling : PlayerStateTransition
    {
        private readonly IFlyingStatus _flyingStatus;

        protected sealed override EPlayerState From => EPlayerState.Flying;
        
        public TransitionFromFlyingToFalling(PlayerStateFactory stateFactory, IFlyingStatus flyingStatus)
            : base(stateFactory)
        {
            _flyingStatus = flyingStatus;
        }

        public sealed override (bool canChange, PlayerStateBase newState) CanChangeState(PlayerStateBase currentState)
        { 
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = _flyingStatus.IsFlying == false;

                if (canChange)
                {
                    newState = _stateFactory.Create<PlayerFallingState>();
                }
            }

            return (canChange, newState);
        }
    }
}
