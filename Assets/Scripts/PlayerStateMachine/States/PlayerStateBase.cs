namespace PlayerStateMachine.States
{
    public abstract class PlayerStateBase
    {
        public abstract EPlayerState State { get; }

        public abstract void StateEntered();

        public abstract void FixedUpdateState();

        public abstract void UpdateState();

        public abstract void StateExited();
    }
}
