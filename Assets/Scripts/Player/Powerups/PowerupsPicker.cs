using System;
using Collisions;
using UnityEngine;

namespace Player.Powerups
{
    public class PowerupsPicker : MonoBehaviour
    {
        [SerializeField]
        private CapsuleCollider _capsuleCollider;

        [SerializeField]
        private LayerMask _collisionMask;
        
        [SerializeField]
        private PowerupsController _powerupsController;
        
        private readonly Collider[] _collidersBuffer = new Collider[5];

        private void Reset()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _powerupsController = GetComponent<PowerupsController>();
        }

        private void FixedUpdate()
        {
            TryPickPowerups();
        }

        private void TryPickPowerups()
        {
            var (capsuleStart, capsuleEnd) = PhysicsUtility.CalculateCapsulePoints(transform, _capsuleCollider);

            var hitCount = Physics.OverlapCapsuleNonAlloc(capsuleStart, capsuleEnd, _capsuleCollider.radius,
                                                          _collidersBuffer, _collisionMask);

            for (var i = 0; i < hitCount; i++)
            {
                var hitCollider = _collidersBuffer[i];

                if (hitCollider.TryGetComponent<PowerupPickable>(out var powerupPickable))
                {
                    foreach (var powerupConfig in powerupPickable.PowerupConfigs)
                    {
                        _powerupsController.Add(powerupConfig);
                    }
                    
                    powerupPickable.Destroy();
                }
            }
        }
    }
}
