﻿using Player;
using PlayerStateMachine.States;

namespace PlayerStateMachine.Transitions
{
    public sealed class TransitionFromFallingToGrounded : PlayerStateTransition
    {
        private readonly IGroundedStatus _groundedStatus;
        protected override EPlayerState From => EPlayerState.Falling;
        
        public TransitionFromFallingToGrounded(IGroundedStatus groundedStatus, PlayerStateFactory stateFactory)
            : base(stateFactory)
        {
            _groundedStatus = groundedStatus;
        }

        public override (bool canChange, PlayerStateBase newState) CanChangeState(PlayerStateBase currentState)
        {
            var canChange = false;
            var newState = currentState;

            if (currentState.State == From)
            {
                canChange = _groundedStatus.IsGrounded;

                if (canChange)
                {
                    newState = _stateFactory.Create<PlayerGroundedState>();
                }
            }

            return (canChange, newState);
        }
    }
}
