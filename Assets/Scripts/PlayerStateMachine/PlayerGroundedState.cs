namespace PlayerStateMachine
{
    public class PlayerGroundedState : PlayerStateBase
    {
        public override EPlayerState State => EPlayerState.Grounded;

        public override void StateEntered()
        { }

        public override void UpdateState()
        { }

        public override void StateExited()
        { }
    }
}
