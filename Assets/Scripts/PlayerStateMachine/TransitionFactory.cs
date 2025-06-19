using PlayerStateMachine.Transitions;
using Zenject;

namespace PlayerStateMachine
{
    public class TransitionFactory
    {
        private readonly DiContainer _container;

        public TransitionFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : PlayerStateTransition
        {
            return _container.Instantiate<T>();
        }
    }
}
