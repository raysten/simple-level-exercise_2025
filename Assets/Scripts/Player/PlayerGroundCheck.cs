using DependencyInjection;
using Framework;
using UnityEngine;

namespace Player
{
    public class PlayerGroundCheck : IGroundedStatus
    {
        private float _checkRadius;
        
        private IGroundCheckConfig _config;
        private CapsuleCollider _capsuleCollider;
        private Transform _transform;

        public bool IsGrounded { get; private set; }

        public PlayerGroundCheck(
            IGameInitializer initializer, IUpdateProvider updateProvider, IGroundCheckConfig config,
            CapsuleCollider capsuleCollider, Transform transform)
        {
            _capsuleCollider = capsuleCollider;
            _config = config;
            _transform = transform;
            
            initializer.OnGameInitialized += Initialize;

            void Initialize()
            {
                _checkRadius = _capsuleCollider.radius - _config.GroundCheckOffsetIntoCapsule;
                
                initializer.OnGameInitialized -= Initialize;
                initializer.OnGameDeinitialized += Deinitialize;

                updateProvider.OnFixedUpdate += FixedUpdate;
            }

            void Deinitialize()
            {
                initializer.OnGameDeinitialized -= Deinitialize;
                
                updateProvider.OnFixedUpdate -= FixedUpdate;
            }
        }

        private void FixedUpdate()
        {
            IsGrounded = CheckIsGrounded();
        }

        private bool CheckIsGrounded()
        {
            var groundDistance = _config.GroundDistance;
            var origin = _transform.position + Vector3.down * (_capsuleCollider.height / 2f - _checkRadius + groundDistance);
            var collisionMask = _config.GroundCheckLayerMask;
            
            return Physics.CheckSphere(origin, _checkRadius, collisionMask, QueryTriggerInteraction.Ignore);
        }
    }
}