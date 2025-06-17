namespace PlayerStateMachine
{
    public abstract class PlayerStateBase
    {
        protected PlayerFacade _playerFacade;

        protected PlayerStateBase(PlayerFacade playerFacade)
        {
            _playerFacade = playerFacade;
        }

        public abstract EPlayerState State { get; }
        
        public abstract void StateEntered();

        public abstract void FixedUpdateState();
    
        public abstract void UpdateState();
    
        public abstract void StateExited();
    }
}