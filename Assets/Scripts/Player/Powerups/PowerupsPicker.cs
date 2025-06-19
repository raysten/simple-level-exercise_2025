using Collisions;
using DependencyInjection;
using Framework;
using UnityEngine;

namespace Player.Powerups
{
    public class PowerupsPicker
    {
        private readonly Collider[] _collidersBuffer = new Collider[5];
        
        private readonly Transform _transform;
        private readonly CapsuleCollider _capsuleCollider;
        private readonly IPowerupLayerMask _powerupLayerMask;
        private readonly IAddPowerup _addPowerup;

        public PowerupsPicker(Transform transform, CapsuleCollider capsuleCollider, IPowerupLayerMask powerupLayerMask,
                              IAddPowerup addPowerup, IGameInitializer initializer, IUpdateProvider updateProvider)
        {
            _capsuleCollider = capsuleCollider;
            _transform = transform;
            _powerupLayerMask = powerupLayerMask;
            _addPowerup = addPowerup;
            
            initializer.OnGameInitialized += SubscribeEvents;

            void SubscribeEvents()
            {
                initializer.OnGameInitialized -= SubscribeEvents;
                initializer.OnGameDeinitialized += UnsubscribeEvents;

                updateProvider.OnFixedUpdate += FixedUpdate;
            }

            void UnsubscribeEvents()
            {
                initializer.OnGameDeinitialized -= UnsubscribeEvents;
                
                updateProvider.OnFixedUpdate -= FixedUpdate;
            }
        }

        private void FixedUpdate()
        {
            TryPickPowerups();
        }

        private void TryPickPowerups()
        {
            var (capsuleStart, capsuleEnd) = PhysicsUtility.CalculateCapsulePoints(_transform, _capsuleCollider);
            var collisionMask = _powerupLayerMask.PowerupLayerMask;
            var hitCount = Physics.OverlapCapsuleNonAlloc(capsuleStart, capsuleEnd, _capsuleCollider.radius,
                                                          _collidersBuffer, collisionMask);
            
            for (var i = 0; i < hitCount; i++)
            {
                var hitCollider = _collidersBuffer[i];

                if (hitCollider.TryGetComponent<PowerupPickable>(out var powerupPickable))
                {
                    foreach (var powerupConfig in powerupPickable.PowerupConfigs)
                    {
                        _addPowerup.Add(powerupConfig);
                    }
                    
                    powerupPickable.Destroy();
                }
            }
        }
    }
}
