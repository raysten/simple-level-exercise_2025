using System.Collections.Generic;
using Framework;
using Player;
using PlayerStateMachine.States;
using PlayerStateMachine.Transitions;

namespace PlayerStateMachine
{
    public class PlayerStateController
    {
        private readonly List<PlayerStateTransition> _transitions = new();

        private PlayerStateBase _currentState;
        
        private readonly PlayerStateFactory _stateFactory;
        private readonly TransitionFactory _transitionFactory;
        private readonly IPlayerEvents _playerEvents;

        public PlayerStateController(
            PlayerStateFactory stateFactory, TransitionFactory transitionFactory, IGameInitializer initializer,
            IUpdateProvider updateProvider, IPlayerEvents playerEvents)
        {
            _stateFactory = stateFactory;
            _transitionFactory = transitionFactory;
            _playerEvents = playerEvents;
            
            initializer.OnGameInitialized += Initialize;

            void Initialize()
            {
                InitializeFirstState();
                InstantiateTransitions();
                
                initializer.OnGameInitialized -= Initialize;
                initializer.OnGameDeinitialized += Deinitialize;

                updateProvider.OnFixedUpdate += FixedUpdate;
                updateProvider.OnUpdate += Update;
            }

            void Deinitialize()
            {
                initializer.OnGameDeinitialized -= Deinitialize;
                
                updateProvider.OnFixedUpdate -= FixedUpdate;
                updateProvider.OnUpdate -= Update;
            }
        }

        private void InitializeFirstState()
        {
            _currentState = _stateFactory.Create<PlayerGroundedState>();
            _currentState.StateEntered();
        }

        private void InstantiateTransitions()
        {
            _transitions.Add(_transitionFactory.Create<TransitionFromGroundedToFalling>());
            _transitions.Add(_transitionFactory.Create<TransitionFromFallingToGrounded>());
            _transitions.Add(_transitionFactory.Create<TransitionFromAnyToFlying>());
            _transitions.Add(_transitionFactory.Create<TransitionFromFlyingToFalling>());
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

        private void CheckTransitions()
        {
            foreach (var transition in _transitions)
            {
                var transitionEvaluation = transition.CanChangeState(_currentState);
                
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
            
            _playerEvents.InvokePlayerStateChanged(_currentState.ToString());
        }
    }
}