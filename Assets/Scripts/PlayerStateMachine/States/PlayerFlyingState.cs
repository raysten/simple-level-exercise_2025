using Player;
using UnityEngine;

namespace PlayerStateMachine.States
{
    public class PlayerFlyingState : PlayerStateBase
    {
        private readonly IVerticalMovement _verticalMovement;
        private readonly IHorizontalInput _horizontalInput;
        private readonly Transform _transform;
        private readonly IMove _move;
        private readonly IHorizontalSpeed _horizontalSpeed;
        private readonly IFlyingInput _flyingInput;
        public sealed override EPlayerState State => EPlayerState.Flying;

        public PlayerFlyingState(
            IVerticalMovement verticalMovement, IHorizontalInput horizontalInput, Transform transform, IMove move,
            IHorizontalSpeed horizontalSpeed, IFlyingInput flyingInput)
        {
            _verticalMovement = verticalMovement;
            _horizontalInput = horizontalInput;
            _transform = transform;
            _move = move;
            _horizontalSpeed = horizontalSpeed;
            _flyingInput = flyingInput;
        }
        
        public sealed override void StateEntered()
        {
            _verticalMovement.ZeroGravity();
        }

        public sealed override void FixedUpdateState()
        {
            var horizontalMovement = CalculateHorizontalMovement();
            var verticalMovement = CalculateVerticalMovement();
            
            _move.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var playerInput = _horizontalInput.HorizontalInput;
            var horizontalInput = _transform.TransformDirection(playerInput);
            var speed = _horizontalSpeed.CalculateFlyingSpeed();
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        private Vector3 CalculateVerticalMovement()
        {
            var input = _flyingInput.FlyUpDownInput;
            var speed = _horizontalSpeed.CalculateSpeedWhenFalling();
            
            return new Vector3(0f, input * speed * Time.fixedDeltaTime, 0f);
        }

        public sealed override void UpdateState()
        { }

        public sealed override void StateExited()
        {
            _verticalMovement.ResetToDefaultGravity();
        }

        public sealed override string ToString() => nameof(PlayerFlyingState);
    }
}
