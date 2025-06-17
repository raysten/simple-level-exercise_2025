using System.Collections.Generic;
using PlayerStateMachine.States;
using PlayerStateMachine.Transitions;
using UnityEngine;

namespace PlayerStateMachine
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField]
        private PlayerFacade _playerFacade;
        
        [SerializeReference]
        private List<PlayerStateTransition> _transitions = new();

        private PlayerStateBase _currentState;

        private void Reset()
        {
            _playerFacade = GetComponent<PlayerFacade>();
        }

        private void Awake()
        {
            _currentState = new PlayerGroundedState(_playerFacade);
        }

        private void FixedUpdate()
        {
            _currentState.FixedUpdateState();
        }

        private void Update()
        {
            CheckTransitions();
            
            _currentState.UpdateState();
        }

        public void CheckTransitions()
        {
            foreach (var transition in _transitions)
            {
                var transitionEvaluation = transition.CanChangeState(_currentState, _playerFacade);
                
                if (transitionEvaluation.canChange)
                {
                    ChangeState(transitionEvaluation.newState);
                    break;
                }
            }
        }

        private void ChangeState(PlayerStateBase newState)
        {
            _currentState.StateExited();
            
            _currentState = newState;
            
            _currentState.StateEntered();
        }
    }
}