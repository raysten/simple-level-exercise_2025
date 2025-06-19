using Platforms;
using Player;
using UnityEngine;
using Utilities;

namespace PlayerStateMachine.States
{
    public class PlayerGroundedState : PlayerStateBase
    {
        private bool _isOnPlatform;
        
        public override EPlayerState State => EPlayerState.Grounded;

        private PlayerSettings Settings => _playerFacade.PlayerSettings;
        
        public PlayerGroundedState(PlayerFacade playerFacade) : base(playerFacade)
        { }

        public override void StateEntered()
        {
            _playerFacade.DebugDisplay.ShowMessage(nameof(PlayerGroundedState));
            _playerFacade.PlayerVerticalMovement.ResetToDefaultGravity();
        }

        public override void FixedUpdateState()
        {
            var platformVelocity = FindMovingPlatformsVelocity();
            var horizontalMovement = CalculateHorizontalMovement() + platformVelocity.Horizontal();
            
            var verticalMovement = _playerFacade.PlayerVerticalMovement.VerticalMovementDelta + platformVelocity.Vertical();
            
            _playerFacade.PlayerMovement.Move(horizontalMovement, verticalMovement);
        }

        private Vector3 CalculateHorizontalMovement()
        {
            var horizontalInput = _playerFacade.transform.TransformDirection(_playerFacade.PlayerInput.HorizontalInput);
            var speed = _playerFacade.PlayerHorizontalSpeed.CalculateGroundedSpeed();
            
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
                _playerFacade.PlayerVerticalMovement.ZeroGravity();
            }

            _isOnPlatform = true;
        }

        private void LeavePlatform()
        {
            _isOnPlatform = false;
            _playerFacade.PlayerVerticalMovement.ResetToDefaultGravity();
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
