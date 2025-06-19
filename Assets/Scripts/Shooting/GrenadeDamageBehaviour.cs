using Damageables;
using UnityEngine;

namespace Shooting
{
    [CreateAssetMenu(fileName = nameof(GrenadeDamageBehaviour),
                     menuName = "Shooting/DamageBehaviours/" + nameof(GrenadeDamageBehaviour))]
    public class GrenadeDamageBehaviour : DamageBehaviour
    {
        private const int MAX_AFFECTED_TARGETS = 10;
        
        [SerializeField]
        private float _damageRadius = 3f;
        
        [SerializeField]
        private LayerMask _affectedLayers;
        
        private readonly Collider[] _hitBuffer = new Collider[MAX_AFFECTED_TARGETS];
        
        public override void DealDamage(RaycastHit hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(hit.point, _damageRadius, _hitBuffer, _affectedLayers);

            for (var i = 0; i < hitCount; i++)
            {
                var collider = _hitBuffer[i];

                if (collider.TryGetComponent(out Damageable damageable))
                {
                    damageable.TakeDamage(_damage);
                }
            }
        }
    }
}
