using Player;
using UnityEngine;

namespace PlayerStateMachine.States
{
    public class PlayerFallingState : PlayerStateBase
    {
        private readonly IVerticalMovement _verticalMovement;
        private readonly IMove _movement;
        private readonly IHorizontalInput _horizontalInput;
        private readonly Transform _transform;
        private readonly IHorizontalSpeed _playerHorizontalSpeed;

        public override EPlayerState State => EPlayerState.Falling;
        
        public PlayerFallingState(
            IVerticalMovement verticalMovement, IMove movement, IHorizontalInput horizontalInput, Transform  transform,
            IHorizontalSpeed playerHorizontalSpeed)
        {
            _verticalMovement = verticalMovement;
            _movement = movement;
            _horizontalInput = horizontalInput;
            _transform = transform;
            _playerHorizontalSpeed = playerHorizontalSpeed;
        }

        public override void StateEntered()
        { }

        public override void FixedUpdateState()
        {
            _verticalMovement.ApplyGravity();
            
            var horizontalMovement = CalculateHorizontalMovement();
            var verticalMovement = _verticalMovement.VerticalMovementDelta;
            
            _movement.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var playerInput = _horizontalInput.HorizontalInput;
            var horizontalInput = _transform.TransformDirection(playerInput);
            var speed = _playerHorizontalSpeed.CalculateSpeedWhenFalling();
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        public override void UpdateState()
        { }

        public override void StateExited()
        { }

        public override string ToString() => nameof(PlayerFallingState);
    }
}
