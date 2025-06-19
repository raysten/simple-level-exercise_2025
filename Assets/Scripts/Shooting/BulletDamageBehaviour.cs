using Damageables;
using UnityEngine;

namespace Shooting
{
    [CreateAssetMenu(fileName = nameof(BulletDamageBehaviour),
                     menuName = "Shooting/DamageBehaviours/" + nameof(BulletDamageBehaviour))]
    public class BulletDamageBehaviour : DamageBehaviour
    {
        public override void DealDamage(RaycastHit hit, float damageMultiplier)
        {
            if (hit.transform.TryGetComponent(out Damageable damageable))
            {
                damageable.TakeDamage(_damage * damageMultiplier);
            }
        }
    }
}
