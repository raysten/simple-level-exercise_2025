﻿using Player;
using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public class TransitionFromAnyToFlying : PlayerStateTransition
    {
        private IFlyingStatus _flyingStatus;
        
        protected sealed override EPlayerState From => EPlayerState.Any;
        
        public TransitionFromAnyToFlying(PlayerStateFactory stateFactory, IFlyingStatus flyingStatus) : base(stateFactory)
        {
            _flyingStatus = flyingStatus;
        }

        public sealed override (bool canChange, PlayerStateBase newState) CanChangeState(PlayerStateBase currentState)
        {
            var newState = currentState;
            var isFlying = _flyingStatus.IsFlying;

            if (isFlying)
            {
                newState = _stateFactory.Create<PlayerFlyingState>();
            }
            
            return (_flyingStatus.IsFlying, newState);
        }
    }
}
