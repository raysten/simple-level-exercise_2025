using System.Collections.Generic;
using UnityEngine;

namespace Shooting
{
    public class ProjectileDamage : MonoBehaviour
    {
        private const float CHECK_DISTANCE = 0.5f;
        
        [SerializeField]
        private List<DamageBehaviour> _damageBehaviours = new(); // not instantiated because there is no persistent state needed
        
        [SerializeField]
        private LayerMask _collisionMask;

        [SerializeField]
        private Projectile _projectile;

        private void Reset()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void FixedUpdate()
        {
            CheckForHit();
        }

        private void CheckForHit()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, CHECK_DISTANCE, _collisionMask,
                                QueryTriggerInteraction.Ignore))
            {
                TryDealDamage(hit);
                _projectile.Despawn();
            }
        }

        private void TryDealDamage(RaycastHit hit)
        {
            foreach (var damageBehaviour in _damageBehaviours)
            {
                damageBehaviour.DealDamage(hit);
            }
        }
    }
}
