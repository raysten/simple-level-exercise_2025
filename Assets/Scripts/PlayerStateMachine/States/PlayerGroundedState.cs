using Platforms;
using Player;
using UnityEngine;
using Utilities;

namespace PlayerStateMachine.States
{
    public class PlayerGroundedState : PlayerStateBase
    {
        public override EPlayerState State => EPlayerState.Grounded;
        
        public PlayerGroundedState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override void StateEntered()
        {
            _playerFacade.DebugDisplay.ShowMessage(nameof(PlayerGroundedState));
            _playerFacade.PlayerVerticalMovement.DeactivateGravity();
        }

        public override void FixedUpdateState()
        {
            var platformVelocity = FindMovingPlatformsVelocity();
            var horizontalMovement = CalculateHorizontalMovement() + platformVelocity.Horizontal();
            
            var verticalMovement = _playerFacade.PlayerVerticalMovement.VerticalMovement + platformVelocity.Vertical();
            
            _playerFacade.PlayerMovement.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var horizontalInput = _playerFacade.transform.TransformDirection(_playerFacade.PlayerInput.HorizontalInput);
            var speed = _playerFacade.PlayerSettings.HorizontalMoveSpeed;
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        private Vector3 FindMovingPlatformsVelocity()
        {
            var velocity = Vector3.zero;
            
            var transform = _playerFacade.transform;
            var capsule = _playerFacade.CapsuleCollider;
            var radius = capsule.radius;
            var castDistance = capsule.height / 2f - radius + 0.1f;
            var collisionMask = _playerFacade.PlayerSettings.MovingPlatformLayerMask;
            
            if (Physics.SphereCast(transform.position, radius, Vector3.down, out var hit, castDistance, collisionMask))
            {
                var movingPlatform = hit.collider.GetComponent<IMovingPlatform>();

                if (movingPlatform != null)
                {
                    velocity = movingPlatform.Velocity;
                }
            }

            return velocity;
        }

        public override void UpdateState()
        {
            if (_playerFacade.PlayerInput.IsJumpPressed)
            {
                _playerFacade.PlayerVerticalMovement.Jump();
            }
        }

        public override void StateExited()
        { }
    }
}
