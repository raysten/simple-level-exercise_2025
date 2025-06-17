using PlayerStateMachine.States;
using UnityEngine;

namespace PlayerStateMachine.Transitions
{
    public abstract class PlayerStateTransition : ScriptableObject
    {
        protected abstract EPlayerState From { get; }

        public abstract (bool canChange, PlayerStateBase newState) CanChangeState(
            PlayerStateBase currentState, PlayerFacade playerFacade);
    }
}
