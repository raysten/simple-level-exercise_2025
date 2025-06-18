using Damageables;
using UnityEngine;

namespace Shooting
{
    public class ProjectileDamage : MonoBehaviour
    {
        private const float CHECK_DISTANCE = 0.5f;
        
        [SerializeField]
        private DamageBehaviour _damageBehaviour;
        
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
                var damageable = hit.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(_damage);
                    _projectile.Despawn();
                }
            }
        }
    }
}
