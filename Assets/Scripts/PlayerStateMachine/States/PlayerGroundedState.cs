using Framework;
using Platforms;
using Player;
using Settings;
using UnityEngine;
using Utilities;

namespace PlayerStateMachine.States
{
    public class PlayerGroundedState : PlayerStateBase
    {
        private bool _isOnPlatform;
        
        private readonly IMovementLayers _movementLayers;
        private readonly IVerticalMovement _verticalMovement;
        private readonly IMove _move;
        private readonly Transform _transform;
        private readonly IHorizontalInput _horizontalInput;
        private readonly IHorizontalSpeed _horizontalSpeed;
        private readonly CapsuleCollider _capsuleCollider;
        private readonly IJumpInput _jumpInput;

        public override EPlayerState State => EPlayerState.Grounded;

        public PlayerGroundedState(IMovementLayers movementLayers, IVerticalMovement verticalMovement, IMove move,
                                   Transform transform, IHorizontalInput horizontalInput,
                                   IHorizontalSpeed horizontalSpeed, CapsuleCollider capsuleCollider,
                                   IJumpInput jumpInput)
        {
            _movementLayers = movementLayers;
            _verticalMovement = verticalMovement;
            _move = move;
            _transform = transform;
            _horizontalInput = horizontalInput;
            _horizontalSpeed = horizontalSpeed;
            _capsuleCollider = capsuleCollider;
            _jumpInput = jumpInput;
        }

        public override void StateEntered()
        {
            // _playerFacade.DebugDisplay.ShowMessage(nameof(PlayerGroundedState));
            _verticalMovement.ResetToDefaultGravity();
        }

        public override void FixedUpdateState()
        {
            var platformVelocity = FindMovingPlatformsVelocity();
            var horizontalMovement = CalculateHorizontalMovement() + platformVelocity.Horizontal();
            
            var verticalMovement = _verticalMovement.VerticalMovementDelta + platformVelocity.Vertical();
            
            _move.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var horizontalInput = _transform.TransformDirection(_horizontalInput.HorizontalInput);
            var speed = _horizontalSpeed.CalculateGroundedSpeed();
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        private Vector3 FindMovingPlatformsVelocity()
        {
            var velocity = Vector3.zero;

            var radius = _capsuleCollider.radius;
            var castDistance = _capsuleCollider.height / 2f - radius + 0.1f;
            var collisionMask = _movementLayers.MovingPlatformLayerMask;
            
            if (Physics.SphereCast(_transform.position, radius, Vector3.down, out var hit, castDistance, collisionMask))
            {
                var movingPlatform = hit.collider.GetComponent<IMovingPlatform>();

                if (movingPlatform != null)
                {
                    velocity = movingPlatform.Velocity;
                    EnterPlatform();
                }
                else
                {
                    LeavePlatform();
                }
            }

            return velocity;
        }

        private void EnterPlatform()
        {
            if (_isOnPlatform == false)
            {
                _verticalMovement.ZeroGravity();
            }

            _isOnPlatform = true;
        }

        private void LeavePlatform()
        {
            _isOnPlatform = false;
            _verticalMovement.ResetToDefaultGravity();
        }

        public override void UpdateState()
        {
            if (_jumpInput.IsJumpPressed)
            {
                _verticalMovement.Jump();
            }
        }

        public override void StateExited()
        { }
    }
}
