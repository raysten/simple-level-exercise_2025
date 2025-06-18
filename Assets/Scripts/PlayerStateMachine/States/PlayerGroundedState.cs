using Platforms;
using Player;
using UnityEngine;
using Utilities;

namespace PlayerStateMachine.States
{
    public class PlayerGroundedState : PlayerStateBase
    {
        public override EPlayerState State => EPlayerState.Grounded;

        private PlayerSettings Settings => _playerFacade.PlayerSettings;
        
        public PlayerGroundedState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override void StateEntered()
        {
            _playerFacade.DebugDisplay.ShowMessage(nameof(PlayerGroundedState));
            _playerFacade.PlayerVerticalMovement.ResetGravity();
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
            var isAccelerate = _playerFacade.PlayerInput.IsSprintPressed;
            var speed = isAccelerate ? Settings.SprintSpeed : Settings.HorizontalMoveSpeed;
            
            return horizontalInput * (speed * Time.fixedDeltaTime);
        }

        private Vector3 FindMovingPlatformsVelocity()
        {
            var velocity = Vector3.zero;
            
            var transform = _playerFacade.transform;
            var capsule = _playerFacade.CapsuleCollider;
            var radius = capsule.radius;
            var castDistance = capsule.height / 2f - radius + 0.1f;
            var collisionMask = Settings.MovingPlatformLayerMask;
            
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
