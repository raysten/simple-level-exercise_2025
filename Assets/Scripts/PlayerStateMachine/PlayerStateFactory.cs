using PlayerStateMachine.States;
using Zenject;

namespace PlayerStateMachine
{
    public class PlayerStateFactory
    {
        private readonly DiContainer _container;

        public PlayerStateFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>() where T : PlayerStateBase
        {
            return _container.Instantiate<T>();
        }
    }
}
